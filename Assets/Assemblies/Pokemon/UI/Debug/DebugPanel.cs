using System;
using System.Collections.Generic;
using Nico;
using OneButtonGame;
using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    //基于UITookit实现的调试面板
    public class DebugPanel : UIPanel
    {
        [SerializeField] private List<GameObject> debugPrefabs = new List<GameObject>();

        [SerializeField] private Button closeButton;
        [SerializeField] private RectTransform moveParamsContainer;
        private PlayerModel playerModel => ModelManager.Get<PlayerModel>();

        private void Awake()
        {
            foreach (var prefab in debugPrefabs)
            {
                PoolGameObjectManager.Instance.Register(prefab);
            }
        }

        public override void OnCreate()
        {
            closeButton.onClick.AddListener(UIManager.Instance.CloseUI<DebugPanel>);
            //通过反射拿到所有celesteMoveParams的成员变量
            foreach (var fieldInfo in typeof(CelesteMoveParams).GetFields())
            {
                if (fieldInfo.FieldType == typeof(float))
                {
                    //判断类型 如果是float类型的 则 slider
                    DebugSlider slider = PoolGameObjectManager.Instance.Get<DebugSlider>();
                    slider.text = fieldInfo.Name + ":" + fieldInfo.GetValue(playerModel.celesteMoveParams);
                    slider.SetValue((float)fieldInfo.GetValue(playerModel.celesteMoveParams));
                    slider.AddListener(value =>
                    {
                        fieldInfo.SetValue(playerModel.celesteMoveParams, value);
                        slider.text = fieldInfo.Name + ":" + value;
                    });


                    slider.transform.SetParent(moveParamsContainer);
                    slider.transform.localScale = Vector3.one;
                    slider.rectTransform.localPosition = Vector3.zero;
                    continue;
                }

                //bool 则 toggle
                if (fieldInfo.FieldType == typeof(bool))
                {
                    DebugToggle toggle = PoolGameObjectManager.Instance.Get<DebugToggle>();
                    toggle.tmpText.text = fieldInfo.Name;
                    toggle.toggle.isOn = (bool)fieldInfo.GetValue(playerModel.celesteMoveParams);
                    toggle.toggle.onValueChanged.AddListener(value =>
                    {
                        fieldInfo.SetValue(playerModel.celesteMoveParams, value);
                        toggle.tmpText.text = fieldInfo.Name + ":" + value;
                    });

                    toggle.transform.SetParent(moveParamsContainer);
                    toggle.transform.localScale = Vector3.one;
                    toggle.transform.localPosition = Vector3.zero;
                }

                //Vector2 则两个slider
                if (fieldInfo.FieldType == typeof(Vector2))
                {
                    DebugSlider x = PoolGameObjectManager.Instance.Get<DebugSlider>();
                    DebugSlider y = PoolGameObjectManager.Instance.Get<DebugSlider>();
                    x.text = fieldInfo.Name + ".x" + fieldInfo.GetValue(playerModel.celesteMoveParams);
                    y.text = fieldInfo.Name + ".y" + fieldInfo.GetValue(playerModel.celesteMoveParams);
                    Vector2 vector2 = (Vector2)fieldInfo.GetValue(playerModel.celesteMoveParams);
                    x.SetValue(vector2.x);
                    y.SetValue(vector2.y);
                    x.AddListener(value =>
                    {
                        vector2.x = value;
                        fieldInfo.SetValue(playerModel.celesteMoveParams, vector2);
                        x.text = fieldInfo.Name + ".x:" + value;
                    });
                    y.AddListener(value =>
                    {
                        vector2.y = value;
                        fieldInfo.SetValue(playerModel.celesteMoveParams, vector2);
                        y.text = fieldInfo.Name + ".y:" + value;
                    });


                    x.transform.SetParent(moveParamsContainer);
                    y.transform.SetParent(moveParamsContainer);
                    x.transform.localScale = Vector3.one;
                    x.transform.localPosition = Vector3.zero;
                    y.transform.localScale = Vector3.one;
                    y.transform.localPosition = Vector3.zero;
                }
            }
            //再绘制一下玩家无限跳跃和无限冲刺的开关
            DebugToggle infiniteJumpToggle = PoolGameObjectManager.Instance.Get<DebugToggle>();
            infiniteJumpToggle.tmpText.text = "InfiniteJump";
            infiniteJumpToggle.toggle.isOn = playerModel.infiniteJump;
            infiniteJumpToggle.toggle.onValueChanged.AddListener(value =>
            {
                playerModel.infiniteJump = value;
            });
            infiniteJumpToggle.transform.SetParent(moveParamsContainer);
            infiniteJumpToggle.transform.localScale = Vector3.one;
            infiniteJumpToggle.transform.localPosition = Vector3.zero;
            
            DebugToggle infiniteDashToggle = PoolGameObjectManager.Instance.Get<DebugToggle>();
            infiniteDashToggle.tmpText.text = "InfiniteDash";
            infiniteDashToggle.toggle.isOn = playerModel.infiniteDash;
            infiniteDashToggle.toggle.onValueChanged.AddListener(value =>
            {
                playerModel.infiniteDash = value;
            });
            infiniteDashToggle.transform.SetParent(moveParamsContainer);
            infiniteDashToggle.transform.localScale = Vector3.one;
            infiniteDashToggle.transform.localPosition = Vector3.zero;
        }

        public override UILayer Layer()
        {
            return UILayer.Top;
        }
    }
}