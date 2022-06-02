using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : Singleton<LobbyController>
{
    [SerializeField] private GameObject lobbyCanvas;
    [SerializeField] private Button enterButton;
    private NetworkHandler _networkHandler;
    
    void Start()
    {
        _networkHandler = NetworkHandler.Instance;
        enterButton.onClick.AddListener(OnClickEnterGame);    
    }

    private void OnClickEnterGame()
    {
        lobbyCanvas.SetActive(false);

        _networkHandler.SpawnLocalPlayer();
    }

    public void ReturnToLobby()
    {
        lobbyCanvas.SetActive(true);
    }
}
