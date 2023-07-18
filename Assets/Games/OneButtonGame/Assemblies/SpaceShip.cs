using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneButtonGame
{
    public enum SpaceShipState
    {
        Rotate,
        SpeedUp,
        Pause,
    }

    public class SpaceShip : MonoBehaviour, IEventListener<OneButtonDown>, IEventListener<OneButtonUp>
    {
        public float forceRate = 1000;
        public float angelSpeed = 5; //角速度
        public float currentAngel = 0;
        public float radius = 10;
        public SpaceShipState state = SpaceShipState.Rotate;
        public Rigidbody2D rb2D;
        public SpaceRotate spaceRotate;
        public float exp;
        public float pauseTime;
        public int level = 1;

        private void Awake()
        {
            spaceRotate = GetComponentInChildren<SpaceRotate>();
            rb2D = GetComponent<Rigidbody2D>();
            EventManager.Listen<OneButtonDown>(this);
            EventManager.Listen<OneButtonUp>(this);
        }

        // private void OnValidate()
        // {
        //     spaceRotate = GetComponentInChildren<SpaceRotate>();
        //     Vector2 dir = spaceRotate.transform.localPosition.normalized;
        //     spaceRotate.transform.localPosition = dir * radius;
        // }

        private void OnDestroy()
        {
            EventManager.UnListen<OneButtonDown>(this);
            EventManager.UnListen<OneButtonUp>(this);
        }

        private async void Update()
        {
            if (state == SpaceShipState.Rotate)
            {
                //2D XY 平面上 child 围绕 parent 旋转 角速度 angelSpeed 半径 radius
                currentAngel += angelSpeed * Time.deltaTime;
                if (currentAngel > 360)
                {
                    currentAngel -= 360;
                }

                spaceRotate.Rotate(currentAngel, radius);
                return;
            }

            if (state == SpaceShipState.SpeedUp)
            {
                state = SpaceShipState.Rotate;

                //向对应方向施加力
                rb2D.velocity = spaceRotate.GetVelocity(forceRate);
                spaceRotate.Release();
                await ExpUp();
                return;
            }

            if (state == SpaceShipState.Pause)
            {
                return;
            }
        }

        public async UniTask ExpUp()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.3f, 1f)));
            Debug.Log("ExpUp");
            float tmp = exp;
            float get = UnityEngine.Random.Range(30, 600) * (Time.time - pauseTime);
            float levelNeedExp = 1000 * level;
            //生成一个提示信息
            GameObject prompt = ObjectPoolManager.Get(nameof(Prompt));
            prompt.transform.position = spaceRotate.transform.position;
            if (get > levelNeedExp * 0.4)
            {
                prompt.GetComponent<Prompt>().Print(((int)get).ToString(CultureInfo.InvariantCulture), Color.green,2);
            }
            else
            {
                prompt.GetComponent<Prompt>().Print(((int)get).ToString(CultureInfo.InvariantCulture), Color.green);
            }
                

            exp += get;
            EventManager.Send(new ExpChange()
            {
                previousExp = tmp,
                currentExp = exp,
                levelNeedExp = levelNeedExp
            });

            if (exp > levelNeedExp)
            {
                level++;
                exp -= levelNeedExp;
                EventManager.Send(new LevelUp()
                {
                    level = level
                });
                GameObject obj = ObjectPoolManager.Get(nameof(Prompt));
                obj.transform.position = transform.position.RandomXYOffset(4);
                Prompt prompt2 = obj.GetComponent<Prompt>();
                prompt2.Print($"Level Up! {level}", Color.red, 2);
            }
        }


        public void OnReceiveEvent(OneButtonDown e)
        {
            state = SpaceShipState.Pause;
            spaceRotate.Pause();
            pauseTime = Time.time;
        }

        public void OnReceiveEvent(OneButtonUp e)
        {
            state = SpaceShipState.SpeedUp;
            pauseTime = Time.time;
        }
    }
}