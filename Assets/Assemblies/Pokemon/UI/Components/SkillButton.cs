using System;
using Nico;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Pokemon
{
    [Serializable]
    public struct SkillConfig
    {
        public const int Dash = 0;
        public const int WallGrab = 1;
        
        public int id;
        public string name;
    }
    public struct SkillButtonClickedEvent : IEvent
    {
        public SkillConfig config;
    }
    public class SkillButton: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public SkillConfig config;
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(config.name);
            EventManager.Send<SkillButtonClickedEvent>(new SkillButtonClickedEvent()
            {
                config = config
            });
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // throw new NotImplementedException();
        }
    }
}