using QFSW.QC;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityToolkit;

namespace TowerDefence
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public ModelCenter modelCenter { get; private set; } = new ModelCenter();
        public EventRepository eventRepository { get; private set; } = new EventRepository();
        
        public override bool dontDestroyOnLoad => true;
        public string gameScene;
        public string lobbyScne;

        [Button]
        public void OpenConsole()
        {
            QuantumConsole.Instance.Activate();
        }
        
        [Button]
        public void CloseConsole()
        {
            QuantumConsole.Instance.Deactivate();
        }

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Tab))
            {
                if (QuantumConsole.Instance.IsActive)
                {
                    QuantumConsole.Instance.Deactivate();
                }
                else
                {
                    QuantumConsole.Instance.Activate();
                }
            }
        }

        protected override void OnInit()
        {
            base.OnInit();
        }

        private void Start()
        {
            // UIRoot.Singleton.OpenPanel<TowerDefenseLobbyMainPanel>();
        }

        public void EnterGamePaly()
        {
        }

        public void BackToLobby()
        {
        }
    }
}