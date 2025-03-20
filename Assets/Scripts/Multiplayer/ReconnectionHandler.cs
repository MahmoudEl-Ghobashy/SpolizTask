using System.Collections;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class ReconnectionHandler : MonoBehaviourPunCallbacks
{
    [SerializeField] private bool isReconnecting;
    [SerializeField] private int numberOfAttempts = 5;
    [SerializeField] private int reconnectionTimeout = 3;
    private bool _isJoined;
    private Transform _myCarTransform;
    private string _roomName;
    private UIHandler _uiHandler;

    private void Start()
    {
        _uiHandler = FindAnyObjectByType<UIHandler>();
    }

    [ContextMenu("Force disconnect")]
    private void ForcedDisconnect()
    {
        PhotonNetwork.Disconnect();
    }

    [ContextMenu("Force delete last position")]
    private void DeleteStoredPosition()
    {
        PlayerPrefs.DeleteAll();
    }

    public override void OnJoinedRoom()
    {
        if (string.IsNullOrEmpty(_roomName))
            _roomName = PhotonNetwork.CurrentRoom.Name;

        _isJoined = true;
        _uiHandler.HideReconnectPanel();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // Save the player's car position & rotation
        var pos = _myCarTransform.transform.position;
        var rot = _myCarTransform.transform.rotation;
        var props = new Hashtable
        {
            { "CarPosition", new[] { pos.x, pos.y, pos.z } },
            { "CarRotation", new[] { rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z } }
        };
        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
        _isJoined = false;
        _uiHandler.ShowReconnectPanel();
        _uiHandler.SetStatusText("Player disconnected");
        AttemptReconnection();
    }

    [ContextMenu("Force reconnect")]
    private void AttemptReconnection()
    {
        if (isReconnecting) return;
        isReconnecting = true;
        _uiHandler.SetStatusText("reconnecting");

        StartCoroutine(ReconnectionAttemptCoroutine());
    }

    private IEnumerator ReconnectionAttemptCoroutine()
    {
        for (var i = 0; i < numberOfAttempts; i++)
        {
            _uiHandler.SetReconnectionText(i);
            var reconnect = PhotonNetwork.ReconnectAndRejoin();
            if (reconnect)
            {
                _uiHandler.SetStatusText("Reconnected successfully");
                isReconnecting = false;
                yield break;
            }

            yield return new WaitForSeconds(reconnectionTimeout);
        }

        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
        _uiHandler.SetStatusText("reconnection failed going back to menu");
        _uiHandler.ShowMenuPanel();
        isReconnecting = false;
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        _uiHandler.SetStatusText("failed to rejoin room " + message);
        _uiHandler.ShowMenuPanel();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        //Handle the other player continue.
        //Todo replace the disconnected with AI 
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
    }

    public void SetMyCar(Transform car)
    {
        _myCarTransform = car;
    }
}