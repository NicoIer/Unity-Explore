using System;
using UnityToolkit;

namespace TowerDefence
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public ToolKitCenter toolKitCenter { get; private set; } = new ToolKitCenter();
        public override bool dontDestroyOnLoad => true;
        public string gameScene;
        public string lobbyScne;
        protected override void OnInit()
        {
            base.OnInit();
        }

        private void Start()
        {
            UIRoot.Singleton.OpenPanel<LobbyMainPanel>();
        }

        public void EnterGamePaly()
        {
        }

        public void BackToLobby()
        {
        }
    }
}