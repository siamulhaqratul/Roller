using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    #region Private Serializable Fields
    [SerializeField]
    private byte maxPlayersPerRoom = 10;
    bool isConnecting;

    #endregion

    #region Private Fields
    string gameVersion = "1";
    public bool isFirstPlayer = true;

    #endregion

    #region MonoBehaviour CallBacks


    void Awake()
    {

        PhotonNetwork.AutomaticallySyncScene = true;

    }

    private void Start()
    {
        StartCoroutine(Connect());
        PhotonNetwork.SendRate = 60;
        PhotonNetwork.SerializationRate = 60;
    }



    #endregion


    #region Public Methods


    public IEnumerator Connect()
    {
        yield return new WaitForSeconds(.10f);
        if (PhotonNetwork.IsConnected)
        {

            PhotonNetwork.JoinRandomRoom();

        }
        else
        {

            PhotonNetwork.GameVersion = gameVersion;
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    }

    #endregion

    #region MonoBehaviourPunCallbacks Callbacks

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected To Master");

        PhotonNetwork.JoinLobby();

        PhotonNetwork.AutomaticallySyncScene = true;



    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause);
    }
    public override void OnJoinedLobby()
    {
        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }

    }
    public override void OnCreatedRoom()
    {

        PhotonNetwork.LoadLevel(1);

    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayersPerRoom;

        PhotonNetwork.CreateRoom(null, roomOptions);
    }

   


  




    #endregion


}
