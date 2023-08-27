using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.Examples.LagCompensationDemo
{
    public class ClientCube : MonoBehaviour
    {
        [Header("Components")] public ServerCube server;
        public Renderer render;

        [Header("Toggle")] public bool interpolate = true;

        // 快照插值的参数设置
        [Header("Snapshot Interpolation")] public SnapshotInterpolationSettings snapshotSettings =
            new SnapshotInterpolationSettings();

        // 缓存多久前的快照
        public double bufferTime => server.sendInterval * snapshotSettings.bufferTimeMultiplier;

        // <服务器时间, 消息>
        public SortedList<double, Snapshot3D> snapshots = new SortedList<double, Snapshot3D>();


        internal double localTimeline; //本地时间线（沿着服务器时间差值得到）
        double localTimescale = 1; //本地时间线的速度 落后太多就加速，超前太多就减速

        ExponentialMovingAverage driftEma; //时间差值的EMA
        ExponentialMovingAverage deliveryTimeEma; // 交付时间的EMA

        // debugging ///////////////////////////////////////////////////////////
        [Header("Debug")] public Color hitColor = Color.blue;
        public Color missedColor = Color.magenta;
        public Color originalColor = Color.black;

        [Header("Simulation")] bool lowFpsMode;
        double accumulatedDeltaTime;

        void Awake()
        {
            // defaultColor = render.sharedMaterial.color;

            // initialize EMA with 'emaDuration' seconds worth of history.
            // 1 second holds 'sendRate' worth of values.
            // multiplied by emaDuration gives n-seconds.
            driftEma = new ExponentialMovingAverage(server.sendRate * snapshotSettings.driftEmaDuration);
            deliveryTimeEma = new ExponentialMovingAverage(server.sendRate * snapshotSettings.deliveryTimeEmaDuration);
        }

        // 客户端收到服务器的消息
        public void OnMessage(Snapshot3D snap)
        {
            // 设置快照的时间戳
            snap.localTime = NetworkTime.localTime;

            // 如果动态调整buffer的大小，增加buffer可以抗抖动，但是会增加延迟
            if (snapshotSettings.dynamicAdjustment)
            {
                // 动态调整buffer的大小
                snapshotSettings.bufferTimeMultiplier = SnapshotInterpolation.DynamicAdjustment(
                    server.sendInterval,
                    deliveryTimeEma.StandardDeviation,
                    snapshotSettings.dynamicAdjustmentTolerance
                );
            }

            //把收到的消息插入buffer中，并且更新本地时间线
            SnapshotInterpolation.InsertAndAdjust(
                snapshots,
                snapshotSettings.bufferLimit,
                snap,
                ref localTimeline,
                ref localTimescale, //本地时间线的速度 落后太多就加速，超前太多就减速，默认会距离服务器时间线 sendInterval * bufferTimeMultiplier
                server.sendInterval,
                bufferTime,
                snapshotSettings.catchupSpeed,
                snapshotSettings.slowdownSpeed,
                ref driftEma, //时间差值的EMA
                snapshotSettings.catchupNegativeThreshold,
                snapshotSettings.catchupPositiveThreshold,
                ref deliveryTimeEma); //交付时间的EMA
        }

        void Update()
        {
            // 用于模拟低帧率情况（后台运行） 1fps 
            accumulatedDeltaTime += Time.unscaledDeltaTime;
            if (lowFpsMode && accumulatedDeltaTime < 1) return;

            // 只有当前有数据时才执行
            if (snapshots.Count > 0)
            {
                // 插值
                if (interpolate)
                {
                    // 更新快照
                    SnapshotInterpolation.Step(
                        snapshots, //快照信息
                        accumulatedDeltaTime, //deltaTime 一帧的时间
                        ref localTimeline, //本地时间线
                        localTimescale, //本地时间线的速度
                        out Snapshot3D fromSnapshot, //插值的起始快照
                        out Snapshot3D toSnapshot, //插值的结束快照
                        out double t //插值的比例
                    );

                    // 进行插值
                    Snapshot3D computed = Snapshot3D.Interpolate(fromSnapshot, toSnapshot, t);
                    transform.position = computed.position;
                }
                // 不插值，给的是多少就渲染多少
                else
                {
                    Snapshot3D snap = snapshots.Values[0];
                    transform.position = snap.position;
                    snapshots.RemoveAt(0);
                }
            }

            // 重制
            accumulatedDeltaTime = 0;
        }

        void OnMouseDown()
        {
            // send the command.
            // only x coordinate matters for this simple example.
            if (server.CmdClicked(localTimeline, bufferTime, transform.position))
            {
                Debug.Log($"Click hit!");
                FlashColor(hitColor);
            }
            else
            {
                Debug.Log($"Click missed!");
                FlashColor(missedColor);
            }
        }

        // simple visual indicator for better feedback.
        // changes the cube's color for a short time.
        void FlashColor(Color color) =>
            StartCoroutine(TemporarilyChangeColorToGreen(color));

        IEnumerator TemporarilyChangeColorToGreen(Color color)
        {
            Renderer r = GetComponentInChildren<Renderer>();
            r.material.color = color;
            yield return new WaitForSeconds(0.2f);
            r.material.color = originalColor;
        }

        void OnGUI()
        {
            // display buffer size as number for easier debugging.
            // catchup is displayed as color state in Update() already.
            const int width = 30; // fit 3 digits
            const int height = 20;
            Vector2 screen = Camera.main.WorldToScreenPoint(transform.position);
            string str = $"{snapshots.Count}";
            GUI.Label(new Rect(screen.x - width / 2, screen.y - height / 2, width, height), str);

            // client simulation buttons on the bottom of the screen
            float areaHeight = 150;
            GUILayout.BeginArea(new Rect(0, Screen.height - areaHeight, Screen.width, areaHeight));
            GUILayout.Label("Click the black cube. Lag compensation will correct the latency.");
            GUILayout.BeginHorizontal();
            GUILayout.Label("Client Simulation:");
            if (GUILayout.Button((lowFpsMode ? "Disable" : "Enable") + " 1 FPS"))
            {
                lowFpsMode = !lowFpsMode;
            }

            GUILayout.Label("|");

            if (GUILayout.Button("Timeline 10s behind"))
            {
                localTimeline -= 10.0;
            }

            if (GUILayout.Button("Timeline 1s behind"))
            {
                localTimeline -= 1.0;
            }

            if (GUILayout.Button("Timeline 0.1s behind"))
            {
                localTimeline -= 0.1;
            }

            GUILayout.Label("|");

            if (GUILayout.Button("Timeline 0.1s ahead"))
            {
                localTimeline += 0.1;
            }

            if (GUILayout.Button("Timeline 1s ahead"))
            {
                localTimeline += 1.0;
            }

            if (GUILayout.Button("Timeline 10s ahead"))
            {
                localTimeline += 10.0;
            }

            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        void OnValidate()
        {
            // thresholds need to be <0 and >0
            snapshotSettings.catchupNegativeThreshold = Math.Min(snapshotSettings.catchupNegativeThreshold, 0);
            snapshotSettings.catchupPositiveThreshold = Math.Max(snapshotSettings.catchupPositiveThreshold, 0);
        }
    }
}