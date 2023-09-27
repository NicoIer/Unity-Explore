using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TowerDefence
{
    public class LobbyMainPanelPun : MonoBehaviourPunCallbacks
    {
        [Button]
        public void Connect()
        {
            PhotonNetwork.GameVersion = SettingManager.Singleton.Get<GameSettings>().gameVersion;
            PhotonNetwork.NickName = "Player" + Random.value;
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            Log.Info($"Connected to master:{PhotonNetwork.LocalPlayer.NickName}");
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            Log.Info($"Disconnected: {cause}");
        }

        [Button]
        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom("Room" + Random.value);
        }
        
        [Button]
        public void JoinRoom()
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            // PhotonNetwork.JoinRoom()
        }
        public override void OnCreatedRoom()
        {
            
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            
        }
    }
}