using Lean.Gui;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private int _skinNumber;

        private void Start()
        {
            ConnectToMaster();

            createButton.OnClick.AddListener(CreateRoom);
            connectButton.OnClick.AddListener(ConnectToRoom);
            connectToRandom.OnClick.AddListener(JoinRandomRoom);

            nextSkin.OnClick.AddListener(NextSkin);
            prevSkin.OnClick.AddListener(PreviousSkin);

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
            playerRenderer.material = playerSkins.Skins[_skinNumber];
        }

        private static void ConnectToMaster()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        private void CreateRoom()
        {
            SetupLocalPlayerCustomProperties();

            var roomName = Random.Range(0, 99999).ToString("00000");

            PhotonNetwork.CreateRoom(roomName);
        }

        public void JoinRandomRoom()
        {
            SetupLocalPlayerCustomProperties();

            PhotonNetwork.JoinRandomRoom();
        }

        private void ConnectToRoom() => JoinRoom(roomId.text);

        private void JoinRoom(string roomName)
        {
            SetupLocalPlayerCustomProperties();

            PhotonNetwork.JoinRoom(roomName.Replace("-", string.Empty));
        }

        public override void OnJoinedRoom()
        {
            PhotonNetwork.IsMessageQueueRunning = false;
            SceneManager.LoadSceneAsync("LevelScene");
        }

        private void SetupLocalPlayerCustomProperties()
        {
            var player = PhotonNetwork.LocalPlayer;

            player.NickName = playerName.text;
            PlayerCustomPropertiesUtility.SetSkinId(player, _skinNumber);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
        }
    }
}