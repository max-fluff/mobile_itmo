using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Lean.Gui;
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

        [SerializeField] public LeanWindow waitingWindow;
        [SerializeField] public LeanWindow winWindow;
        [SerializeField] public LeanWindow lostWindow;

        [SerializeField] public LeanButton quitButton;

        private readonly Dictionary<Player, GameObject> _players = new();

        private void Awake()
        {
            PhotonNetwork.IsMessageQueueRunning = true;

            PhotonNetwork.EnableCloseConnection = true;
            roomNumber.SetText(PhotonNetwork.CurrentRoom.Name);

            var skinId = PlayerCustomPropertiesUtility.GetSkinId(PhotonNetwork.LocalPlayer);
            localPlayer.GetComponent<PlayerSkinController>().SetSkin(skinId);

            if (PhotonNetwork.IsMasterClient)
            {
                localPlayer.SetActive(true);
                _players.Add(PhotonNetwork.LocalPlayer, localPlayer);
            }
            else
                RaiseDataRequestEvent();

            quitButton.OnClick.AddListener(OnQuitClicked);
        }

        private void OnQuitClicked() => PhotonNetwork.LeaveRoom();

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            waitingWindow.TurnOff();
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

            playerGO.GetComponentInChildren<PlayerName>().SetName(player.NickName);

            _players.Add(player, playerGO);

            playerGO.GetComponent<CharacterControls>().enabled = false;

            playerGO.SetActive(true);
        }

        private void RaiseInstantiationEvent(Player player, Player targetPlayer)
        {
            var content = new object[]
            {
                player.ActorNumber,
                _players[player].GetComponent<PhotonView>().ViewID
            };

            var raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.DoNotCache,
                TargetActors = new[] { targetPlayer.ActorNumber }
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.InstantiatePlayer, content, raiseEventOptions,
                sendOptions);
        }

        private void RaiseInstantiationEvent()
        {
            var content = new object[]
            {
                PhotonNetwork.LocalPlayer.ActorNumber,
                localPlayer.GetComponent<PhotonView>().ViewID
            };

            var raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.DoNotCache,
                Receivers = ReceiverGroup.Others
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.InstantiatePlayer, content, raiseEventOptions,
                sendOptions);
        }

        private void RaiseDataRequestEvent()
        {
            var raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.DoNotCache,
                Receivers = ReceiverGroup.MasterClient
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.RequestInitData, null, raiseEventOptions,
                sendOptions);
        }

        public void Win()
        {
            winWindow.TurnOn();

            var raiseEventOptions = new RaiseEventOptions
            {
                CachingOption = EventCaching.DoNotCache,
                Receivers = ReceiverGroup.Others
            };

            var sendOptions = new SendOptions
            {
                Reliability = true
            };

            localPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
            localPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            localPlayer.GetComponent<CharacterControls>().enabled = false;

            PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.PlayerWon, null, raiseEventOptions,
                sendOptions);
        }

        public void OnEvent(EventData photonEvent)
        {
            switch (photonEvent.Code)
            {
                case (byte)PhotonCustomEvents.InstantiatePlayer:
                    var data = (object[])photonEvent.CustomData;
                    SpawnPlayer(GetPlayerByActorNumber((int)data[0]), (int)data[1]);
                    break;
                case (byte)PhotonCustomEvents.InstantiatingDone:
                    PhotonNetwork.AllocateViewID(localPlayer.GetComponent<PhotonView>());
                    localPlayer.SetActive(true);
                    RaiseInstantiationEvent();
                    waitingWindow.TurnOff();
                    break;
                case (byte)PhotonCustomEvents.RequestInitData:
                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (PhotonNetwork.PlayerList.Length > 7)
                            PhotonNetwork.CurrentRoom.IsVisible = false;

                        foreach (var player in _players)
                            RaiseInstantiationEvent(player.Key, GetPlayerByActorNumber(photonEvent.Sender));

                        var raiseEventOptions = new RaiseEventOptions
                        {
                            CachingOption = EventCaching.DoNotCache,
                            TargetActors = new[] { photonEvent.Sender }
                        };

                        var sendOptions = new SendOptions
                        {
                            Reliability = true
                        };

                        PhotonNetwork.RaiseEvent((byte)PhotonCustomEvents.InstantiatingDone, null, raiseEventOptions,
                            sendOptions);
                    }

                    break;

                case (byte)PhotonCustomEvents.PlayerWon:

                    lostWindow.TurnOn();
                    localPlayer.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    localPlayer.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    localPlayer.GetComponent<CharacterControls>().enabled = false;
                    break;
            }
        }

        private Player GetPlayerByActorNumber(int actorNumber) =>
            PhotonNetwork.PlayerList.First(p => p.ActorNumber == actorNumber);
    }
}