using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Mirror.Examples.LagCompensationDemo
{
    public class ServerCube : MonoBehaviour
    {
        [Header("Components")] public ClientCube client; //客户端的cube的引用，这里拿到是为了调用client的OnMessage方法，用于模拟网络
        public BoxCollider col;

        [Header("Movement")] public float distance = 10;
        public float speed = 3;
        Vector3 start;

        [Header("Snapshot Interpolation")] [Tooltip("Send N snapshots per second. Multiples of frame rate make sense.")]
        public int sendRate = 30; // 消息发送频率 每秒30次

        public float sendInterval => 1f / sendRate; //单个消息的发送间隔时间
        float lastSendTime; //上次发送消息的时间

        [Header("Lag Compensation")] public LagCompensationSettings
            lagCompensationSettings = new LagCompensationSettings(); //延迟补偿设置 包含两个成员变量 历史记录长度和采样间隔

        double lastCaptureTime; //上一次记录快照的时间

        // lag compensation history of <timestamp, capture>
        Queue<KeyValuePair<double, Capture2D>> history = new Queue<KeyValuePair<double, Capture2D>>();

        public Color historyColor = Color.white;

        // store latest lag compensation result to show a visual indicator
        [Header("Debug")] public double resultDuration = 0.5;
        double resultTime;
        Capture2D resultBefore;
        Capture2D resultAfter;
        Capture2D resultInterpolated;

        //延迟模拟的参数信息
        [Header("Latency Simulation")] [Tooltip("Latency in seconds")]
        public float latency = 0.05f; // 50 ms 延迟

        [Tooltip("Latency jitter, randomly added to latency.")] [Range(0, 1)]
        public float jitter = 0.05f; //延迟抖动

        [Tooltip("Packet loss in %")] [Range(0, 1)]
        public float loss = 0.1f; //丢包率

        [Tooltip("Scramble % of unreliable messages, just like over the real network. Mirror unreliable is unordered.")]
        [Range(0, 1)]
        public float scramble = 0.1f; //无序消息占总消息的百分比

        System.Random random = new System.Random(); //随机数

        // hold on to snapshots for a little while before delivering
        // <deliveryTime, snapshot>
        List<(double, Snapshot3D)> queue = new List<(double, Snapshot3D)>();


        float SimulateLatency() => latency + Random.value * jitter; //模拟出的延迟 rtt + 随机数 * 抖动


        float AverageLatency() => latency + 0.5f * jitter; //平均延迟 因为随机数的期望是0.5 而不包含rtt是固定的

        void Start()
        {
            start = transform.position;
        }

        void Update()
        {
            //在服务器上移动Cube
            float x = Mathf.PingPong(Time.time * speed, distance);
            transform.position = new Vector3(start.x + x, start.y, start.z);

            //如果达到了发送间隔 就发送当前的位置信息给客户端
            if (Time.time >= lastSendTime + sendInterval)
            {
                Send(transform.position);
                lastSendTime = Time.time;
            }

            Flush();

            // 如果达到了采样间隔 就记录当前的位置信息
            if (NetworkTime.localTime >= lastCaptureTime + lagCompensationSettings.captureInterval)
            {
                lastCaptureTime = NetworkTime.localTime;
                Capture();
            }
        }

        void Send(Vector3 position)
        {
            //创建一个快照
            Snapshot3D snap = new Snapshot3D(NetworkTime.localTime, 0, position);

            // 模拟丢包
            if (random.NextDouble() < loss) return;
            // 模拟无序信息
            int index = queue.Count;
            if (random.NextDouble() < scramble)
            {
                index = random.Next(0, queue.Count + 1);
            }

            // 模拟延迟
            float simulatedLatency = SimulateLatency();
            double deliveryTime = NetworkTime.localTime + simulatedLatency; //消息达到的时间 = 服务器发送时间 + 延迟
            queue.Insert(index, (deliveryTime, snap));
        }

        void Flush()
        {
            // 将所有快照消息都发给客户端
            for (int i = 0; i < queue.Count; ++i)
            {
                (double deliveryTime, Snapshot3D snap) = queue[i];

                //模拟延迟，因为是本地模拟，调用client的函数是没有延迟的，所以必须要等到消息达到的时间才能调用
                if (NetworkTime.localTime < deliveryTime) continue;
                //模拟客户端收到快照消息
                client.OnMessage(snap);
                //移除已经发送的快照消息
                queue.RemoveAt(i);
                --i; //-- 防止移除后数组越界
            }
        }

        void Capture()
        {
            // 记录当前服务器的状态
            Capture2D capture = new Capture2D(NetworkTime.localTime, transform.position, col.size);

            // 存储到历史记录中
            LagCompensation.Insert(history, lagCompensationSettings.historyLimit, NetworkTime.localTime, capture);
        }

        //客户端通知服务器，我在这个时间，点击了这个位置
        public bool CmdClicked(double timeline, double bufferTime, Vector2 position)
        {
            // 永远不要信任客户端的输入，因为客户端可能会作弊 服务器来估算客户端的消息发送时间
            double rtt = AverageLatency() * 2;
            //估算消息的发送时间 用服务器的时间+rtt+客户端的bufferTime
            double estimatedTime = LagCompensation.EstimateTime(NetworkTime.localTime, rtt, bufferTime);

            // 估算时间和真实时间的差值 仅用于DEBUG
            double error = Math.Abs(estimatedTime - timeline);
            Debug.Log(
                $"CmdClicked: serverTime={NetworkTime.localTime:F3} clientTime={timeline:F3} estimatedTime={estimatedTime:F3} estimationError={error:F3} position={position}");

            // 对服务器的历史快照进行采样，拿到客户端发送点击消息时，服务器的状态信息
            if (LagCompensation.Sample(history, estimatedTime, lagCompensationSettings.captureInterval,
                    out resultBefore, out resultAfter, out double t))
            {
                // 进行插值，拿到对应时间的插值状态信息
                resultInterpolated = Capture2D.Interpolate(resultBefore, resultAfter, t);
                resultTime = NetworkTime.localTime;

                // 检查这个时间，客户端是否能够点击到Cube
                Bounds bounds = new Bounds(resultInterpolated.position, resultInterpolated.size);
                if (bounds.Contains(position))
                {
                    return true;
                }
                Debug.Log($"CmdClicked: interpolated={resultInterpolated} doesn't contain {position}");
            }
            else
            {
                Debug.Log($"CmdClicked: history doesn't contain {estimatedTime:F3}");
            }

            return false;
        }

        void OnDrawGizmos()
        {
            // should we apply special colors to an active result?
            bool showResult = NetworkTime.localTime <= resultTime + resultDuration;

            // draw interpoalted result first.
            // history meshcubes should write over it for better visibility.
            if (showResult)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(resultInterpolated.position, resultInterpolated.size);
            }

            // draw history
            Gizmos.color = historyColor;
            LagCompensation.DrawGizmos(history);

            // draw result samples after. useful to see the selection process.
            if (showResult)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireCube(resultBefore.position, resultBefore.size);
                Gizmos.DrawWireCube(resultAfter.position, resultAfter.size);
            }
        }
    }
}