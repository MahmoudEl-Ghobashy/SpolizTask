using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviourPun
{
    public GameObject mainMenuPanel;
    public GameObject reconnectPanel;
    public Button playButton;
    public Button cancelButton;
    public TextMeshProUGUI reconnectText;

    public GameObject statusPanel;
    public TextMeshProUGUI statusText;


    private void Start()
    {
        mainMenuPanel.SetActive(true);
        reconnectPanel.SetActive(false);
        statusPanel.SetActive(false);

        playButton.onClick.AddListener(OnPlayClicked);
        cancelButton.onClick.AddListener(OnCancelClicked);
    }

    private void OnPlayClicked()
    {
        mainMenuPanel.SetActive(false);
        statusPanel.SetActive(true);

        SetStatusText("Connecting to Photon...");
        PhotonNetwork.ConnectUsingSettings();
    }

    private void OnCancelClicked()
    {
        ShowMenuPanel();

        if (PhotonNetwork.IsConnected) PhotonNetwork.Disconnect();
    }

    public void ShowReconnectPanel()
    {
        reconnectPanel.SetActive(true);
    }

    public void HideReconnectPanel()
    {
        reconnectPanel.SetActive(false);
    }

    public void ShowMenuPanel()
    {
        mainMenuPanel.SetActive(true);
        reconnectPanel.SetActive(false);
        statusPanel.SetActive(false);
    }

    public void SetReconnectionText(int currentAttempt)
    {
        reconnectText.text = "Reconnecting... (" + currentAttempt + ")";
    }


    public void SetStatusText(string message)
    {
        statusText.text = message;
    }
}