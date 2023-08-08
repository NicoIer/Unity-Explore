using OneButtonGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    public class DebugToggle: MonoBehaviour,IPoolGameObject
    {

        public Toggle toggle;
        public TextMeshProUGUI tmpText;
        
        PoolObjectState IPoolObject.state { get; set; }

        public void OnSpawn()
        {

        }

        public void OnRecycle()
        {

        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}