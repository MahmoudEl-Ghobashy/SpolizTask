using Photon.Pun;
using Unity.Cinemachine;
using UnityEngine;

public class PlayersSpawner : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CinemachineCamera mainCamera;
    [SerializeField] private ReconnectionHandler reconnectionHandler;

    public override void OnJoinedRoom()
    {
        Debug.Log("joined room");
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("CarPosition"))
        {
            RespawnSavedPosition();
            return;
        }

        var playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPoint = spawnPoints[playerIndex % spawnPoints.Length];

        var playerCar = PhotonNetwork.Instantiate(carPrefab.name, spawnPoint.position, spawnPoint.rotation);
        reconnectionHandler.SetMyCar(playerCar.transform);
        Debug.Log("Joined room spawn player");
        mainCamera.Follow = playerCar.transform;
        //ToDo Assign player name and change color
    }

    private void RespawnSavedPosition()
    {
        var posData = (float[])PhotonNetwork.LocalPlayer.CustomProperties["CarPosition"];
        var rotData = (float[])PhotonNetwork.LocalPlayer.CustomProperties["CarRotation"];

        var savedPosition = new Vector3(posData[0], posData[1], posData[2]);
        var savedRotation = Quaternion.Euler(rotData[0], rotData[1], rotData[2]);


        Debug.Log("Spwan to position " + savedPosition);
        var playerCar = PhotonNetwork.Instantiate(carPrefab.name, savedPosition, savedRotation);
        reconnectionHandler.SetMyCar(playerCar.transform);
        mainCamera.Follow = playerCar.transform;
    }
}