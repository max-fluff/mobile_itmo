using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source
{
    public class RoomCallbacks : MonoBehaviourPunCallbacks, IOnEventCallback
    {
        [SerializeField] public GameObject playerPrefab;
        [SerializeField] public GameObject localPlayer;

        [SerializeField] public TextMeshProUGUI roomNumber;

        private void Awake()
        {
            PhotonNetwork.EnableCloseConnection = true;
            roomNumber.SetText(PhotonNetwork.CurrentRoom.Name);

            RaiseInstantiationEvent();

            var skinId = PlayerCustomPropertiesUtility.GetSkinId(PhotonNetwork.LocalPlayer);
            localPlayer.GetComponent<PlayerSkinController>().SetSkin(skinId);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.CurrentRoom.IsVisible = false;
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            SceneManager.LoadSceneAsync("MenuScene");
        }

        private void SpawnPlayer(Player player, int viewId)
        {
            var skinId = PlayerCustomPropertiesUtility.GetSkinId(player);

            var playerGO = Instantiate(playerPrefab);
            playerGO.GetComponent<PlayerSkinController>().SetSkin(skinId);
            playerGO.GetComponent<PhotonView>().ViewID = viewId;

            playerGO.GetComponent<CharacterControls>().enabled = false;
        }

        private void RaiseInstantiationEvent()
        {
            var content = new object[]
            {
                localPlayer.GetComponent<PhotonView>().ViewID
            };

            var raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.Others,
                CachingOption = EventCaching.AddToRoomCache
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.InstantiatePlayer, content, raiseEventOptions,
                sendOptions);
        }

        public void OnEvent(EventData photonEvent)
        {
            var data = (object[])photonEvent.CustomData;

            if (photonEvent.Code == (byte)PhotonCustomEvents.InstantiatePlayer)
            {
                SpawnPlayer(PhotonNetwork.PlayerList[photonEvent.Sender], (int)data[0]);
            }
        }
    }
}