using System;
using Lean.Gui;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VirtualConferences.CommonPlatform;
using Random = UnityEngine.Random;

namespace Source
{
    public class MenuPanel : MonoBehaviourPunCallbacks
    {
        [SerializeField] private LeanButton connectButton;
        [SerializeField] private TMP_InputField roomId;

        [SerializeField] private LeanButton connectToRandom;
        [SerializeField] private LeanButton createButton;

        [SerializeField] private TMP_InputField playerName;

        [SerializeField] private LeanButton nextSkin;
        [SerializeField] private LeanButton prevSkin;

        [SerializeField] private Renderer playerRenderer;
        [SerializeField] private PlayerSkins playerSkins;

        [SerializeField] private LeanWindow loadingWindow;

        [SerializeField] private TextMeshProUGUI errText;

        private int _skinNumber;

        private void Start()
        {
            ConnectToMaster();

            createButton.OnClick.AddListener(CreateRoom);
            connectButton.OnClick.AddListener(ConnectToRoom);
            connectToRandom.OnClick.AddListener(JoinRandomRoom);

            nextSkin.OnClick.AddListener(NextSkin);
            prevSkin.OnClick.AddListener(PreviousSkin);

            playerName.SetTextWithoutNotify(PlayerPrefs.GetString("name", "Player"));

            playerName.onEndEdit.AddListener(SaveNameToPlayerPrefs);

            _skinNumber = PlayerPrefs.GetInt(nameof(_skinNumber), 0);

            ApplySkin();
        }

        private void NextSkin()
        {
            _skinNumber = (++_skinNumber) % playerSkins.Skins.Count;
            ApplySkin();
        }

        private void PreviousSkin()
        {
            _skinNumber = (--_skinNumber + playerSkins.Skins.Count) % playerSkins.Skins.Count;
            ApplySkin();
        }

        private void ApplySkin()
        {
            PlayerPrefs.SetInt(nameof(_skinNumber), _skinNumber);
            playerRenderer.material = playerSkins.Skins[_skinNumber];
        }

        private void SaveNameToPlayerPrefs(string name) => PlayerPrefs.SetString("name", name);

        private static void ConnectToMaster()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private void CreateRoom()
        {
            ClearErrMessage();

            SetupLocalPlayerCustomProperties();

            var roomOptions = new RoomOptions()
                { PlayerTtl = 10000, MaxPlayers = 8 };

            var roomName = Random.Range(0, 99999).ToString("00000");

            PhotonNetwork.CreateRoom(roomName);

            AnalyticsEventSender.CreateRoom(roomName);
        }

        public void JoinRandomRoom()
        {
            ClearErrMessage();

            SetupLocalPlayerCustomProperties();

            PhotonNetwork.JoinRandomRoom();
        }

        private void ConnectToRoom() => JoinRoom(roomId.text);

        private void JoinRoom(string roomName)
        {
            ClearErrMessage();

            SetupLocalPlayerCustomProperties();

            var roomNumberFormatted = roomName.Replace("-", string.Empty);
            if (string.IsNullOrWhiteSpace(roomNumberFormatted))
                errText.SetText("Room number is empty");
            else
                PhotonNetwork.JoinRoom(roomNumberFormatted);
        }

        private void ClearErrMessage() => errText.SetText(string.Empty);

        public override void OnJoinedRoom()
        {
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadSceneAsync("LevelScene");

            AnalyticsEventSender.JoinRoom(DateTime.Now, PhotonNetwork.CurrentRoom.Name);
        }

        private void SetupLocalPlayerCustomProperties()
        {
            var player = PhotonNetwork.LocalPlayer;

            player.NickName = playerName.text;
            PlayerCustomPropertiesUtility.SetSkinId(player, _skinNumber);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            errText.SetText("Cannot create room");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            errText.SetText("There are no rooms to connect");
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            errText.SetText("Cannot join room \n Room might be closed");
        }

        public override void OnConnectedToMaster()
        {
            ClearErrMessage();
            loadingWindow.TurnOff();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            errText.SetText($"Disconnected\n {cause.ToString()}");
            loadingWindow.TurnOn();
            ConnectToMaster();
        }
    }
}