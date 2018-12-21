/*
*
* Carlos Adan Cortes De la Fuente
* All rights reserved. Copyright (c)
* Email: krlozadan@gmail.com
*
*/

using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : SingletonBehaviour<NetworkManager>, IConnectionCallbacks, ILobbyCallbacks, IInRoomCallbacks, IMatchmakingCallbacks
{

    private string playerName;
    private string customRoomName = null;
    private const string gameVersion = "1";
    private const byte maxPlayersPerRoom = 2;
    private PhotonView photonView;
    private LoadSceneOnStart sceneLoader;
    
    private List<RoomInfo> avaliableRooms = new List<RoomInfo>();

    protected override void SingletonAwake()
    {
        sceneLoader = GetComponent<LoadSceneOnStart>();
        photonView = GetComponent<PhotonView>();
        PhotonNetwork.AddCallbackTarget(this);
        PhotonNetwork.AutomaticallySyncScene = false;
        PhotonNetwork.GameVersion = gameVersion;
        playerName = "Default Player#" + Random.Range(1000, 9999);
        SetPlayerNickname(playerName);
    }

    #region Custom Matchmaking / Network Logic

    private void SetPlayerNickname(string newNickname) {
        PhotonNetwork.NickName = newNickname;
    }

    // In case the player wants to join before having to wait for the second player
    private void StartGame() {
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.CurrentRoom.IsVisible = false;
        RPC_LoadMainLevel();
    }
    
    private void CreateNewRoom(string roomName = null) {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = maxPlayersPerRoom;
        options.IsVisible = true;
        options.IsOpen = true;
        PhotonNetwork.CreateRoom(roomName, options, TypedLobby.Default);
    }

    public void LetOtherPlayerLoadMainLevel() {
        photonView.RPC("RPC_LoadMainLevel", RpcTarget.Others);
    }

    [PunRPC]
    private void RPC_LoadMainLevel() {
        if (sceneLoader.loadDebugScene == false) {
            PhotonNetwork.LoadLevel(sceneLoader.mainGameScene);
        } else {
            PhotonNetwork.LoadLevel(sceneLoader.debugScene);
        }
    }
    
    #endregion

    #region Connection Callbacks
        
    public void OnConnected()
    {
        // Called when the initial connection got established but before you can use the server
    }

    public void OnConnectedToMaster()
    {
        // Called after the connection to the master is established and authenticated but only when PhotonNetwork.autoJoinLobby is false.
        // Here we can join a default lobby and start creating rooms
        PhotonNetwork.JoinLobby(TypedLobby.Default); // We join the default lobby manually   
    }

    public void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log(cause.ToString());
    }

    public void OnRegionListReceived(RegionHandler regionHandler)
    {

    }

    public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {

    }

    public void OnCustomAuthenticationFailed(string debugMessage)
    {
        
    }

    #endregion
    
    #region Lobby Callbacks
        
    public void OnJoinedLobby()
    {
        // Called on entering a lobby on the Master Server. The actual room-list updates will call OnRoomListUpdate
    }

    public void OnLeftLobby()
    {
        // Called after leaving a lobby
    }

    public void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        avaliableRooms = roomList;
    }

    public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        // Called when the Master Server sent an update for the Lobby Statistics, updating PhotonNetwork.LobbyStatistics
    }

    #endregion

    #region InRoom Callbacks
        
    public void OnPlayerEnteredRoom(Player newPlayer)
    {
    }

    public void OnPlayerLeftRoom(Player otherPlayer)
    {
        if (SceneManager.GetActiveScene().name == sceneLoader.mainGameScene || SceneManager.GetActiveScene().name == sceneLoader.debugScene) {
            PhotonNetwork.LeaveRoom();
            return;
        }
        string isPlayerLocal = otherPlayer.IsLocal ? "IS" : "IS NOT";
        PhotonNetwork.CurrentRoom.IsOpen = true;
        PhotonNetwork.CurrentRoom.IsVisible = true;
    }

    public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
    }

    public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
    }

    public void OnMasterClientSwitched(Player newMasterClient)
    {
    }

    #endregion
    
    #region Matchmaking Callback
        
    public void OnFriendListUpdate(List<FriendInfo> friendList)
    {
    }

    public void OnCreatedRoom()
    {
    }

    public void OnCreateRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinedRoom()
    {
    }

    public void OnJoinRoomFailed(short returnCode, string message)
    {
    }

    public void OnJoinRandomFailed(short returnCode, string message)
    {
    }

    public void OnLeftRoom()
    {
        SceneManager.LoadScene("Menus");
    }
    
    #endregion 

    #region GUI

    private void OnGUI() {
        
        if(SceneManager.GetActiveScene().name == sceneLoader.mainGameScene || SceneManager.GetActiveScene().name == sceneLoader.debugScene) return;

        if (PhotonNetwork.IsConnected == false) {
            GUILayout.BeginVertical();
                GUILayout.Label("Not Connected");
                if (GUILayout.Button("Connect To Network")) PhotonNetwork.ConnectUsingSettings();
            GUILayout.EndVertical();
            return;
        }

        // Network
        GUILayout.BeginVertical();
            GUILayout.Label("Connected to the network");
            GUILayout.Label("Nickname: " + PhotonNetwork.LocalPlayer.NickName);
            playerName = GUILayout.TextField(playerName);
            if (GUILayout.Button("Change Nickname")) SetPlayerNickname(playerName);
            if (GUILayout.Button("Disconnect")) PhotonNetwork.Disconnect();;
        GUILayout.EndVertical();

        // Matchmaking
        if (PhotonNetwork.InRoom == false) {
            if (avaliableRooms.Count > 0) {
                GUILayout.BeginVertical();
                    GUILayout.Label("Available Rooms:");
                    foreach (RoomInfo room in avaliableRooms) {
                        if (room.MaxPlayers == maxPlayersPerRoom) {
                            if (GUILayout.Button(room.ToStringFull())) PhotonNetwork.JoinRoom(room.Name);
                        }
                    }
                GUILayout.EndVertical();    
            }
            GUILayout.BeginVertical();
                customRoomName = GUILayout.TextField(customRoomName);
                if (GUILayout.Button("Create Room")) CreateNewRoom(customRoomName);    
            GUILayout.EndVertical();
        } else {
            // Show leave room button and list of players in room
            GUILayout.BeginVertical();
                if (PhotonNetwork.IsMasterClient) {
                    if (GUILayout.Button("Start Game")) StartGame();
                }
                GUILayout.Label("You're in Room " + PhotonNetwork.CurrentRoom.Name + " with:");
                foreach (var player in PhotonNetwork.CurrentRoom.Players) {
                    GUILayout.Label(player.Value.NickName);
                }
                if (GUILayout.Button("Leave Room")) PhotonNetwork.LeaveRoom();
            GUILayout.EndVertical();
        }
    }
        
    #endregion
}
