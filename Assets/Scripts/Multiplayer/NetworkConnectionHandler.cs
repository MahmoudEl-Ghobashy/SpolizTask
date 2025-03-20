using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

public class NetworkConnectionHandler : MonoBehaviourPunCallbacks
{
    private UIHandler _uiHandler;

    private void Start()
    {
        _uiHandler = FindAnyObjectByType<UIHandler>();
    }

    public override void OnConnectedToMaster()
    {
        _uiHandler.SetStatusText("Connected to master");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        _uiHandler.SetStatusText("Joined lobby");
        if (!PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("CarPosition"))
            PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        _uiHandler.SetStatusText("Cannot join random room " + message);
        TryCreateRoom();
    }

    private void TryCreateRoom()
    {
        var options = new RoomOptions
        {
            MaxPlayers = 2,
            PlayerTtl = 60000
        };
        var rand = Random.Range(0, 1000);
        PhotonNetwork.CreateRoom("SpoilzRace" + rand, options);
    }

    public override void OnCreatedRoom()
    {
        _uiHandler.SetStatusText("Room created " + PhotonNetwork.CurrentRoom.Name);
        //ToDo wait for 2nd player
    }

    public override void OnJoinedRoom()
    {
        _uiHandler.SetStatusText("Room joined " + PhotonNetwork.CurrentRoom.Name);
        //ToDo check for players count and start game event
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        _uiHandler.SetStatusText("Cannot create room " + message);
        TryCreateRoom();
    }
}