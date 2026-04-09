using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private MultiplayerUI2 m_multiplayerUI2;
    [SerializeField]
    private MultiplayerUI m_multiplayerUI;

    private void Start()
    {
        if (m_multiplayerUI2 != null)
        {
            m_multiplayerUI2.OnStartHost += StartHost2;
            m_multiplayerUI2.OnStartClient += StartClient2;
            m_multiplayerUI2.OnDisconnectClient += DisconnectClient2;
        }
        else
        {
            Debug.LogError("MultiplayerUI2 is not assigned", this);
        }

        if (m_multiplayerUI != null)
        {
            m_multiplayerUI.OnStartHost += StartHost;
            m_multiplayerUI.OnStartClient += StartClient;
            m_multiplayerUI.OnDisconnectClient += DisconnectClient;
        }
        else
        {
            Debug.LogError("MultiplayerUI is not assigned", this);
        }
    }

    private void OnDestroy()
    {
        if (m_multiplayerUI2 != null)
        {
            m_multiplayerUI2.OnStartHost -= StartHost2;
            m_multiplayerUI2.OnStartClient -= StartClient2;
            m_multiplayerUI2.OnDisconnectClient -= DisconnectClient2;
        }

        if (m_multiplayerUI != null)
        {
            m_multiplayerUI.OnStartHost -= StartHost;
            m_multiplayerUI.OnStartClient -= StartClient;
            m_multiplayerUI.OnDisconnectClient -= DisconnectClient;
        }
    }

    private void StartHost2()
    {
        Debug.Log("StartHost2 clicked");
        m_multiplayerUI2.DisableButtons();
        NetworkManager.Singleton.StartHost();
    }

    private void StartClient2()
    {
        m_multiplayerUI2.DisableButtons();
        NetworkManager.Singleton.StartClient();
    }

    private void DisconnectClient2()
    {
        m_multiplayerUI2.EnableButtons();
        NetworkManager.Singleton.Shutdown();
    }

    private void StartHost()
    {
        m_multiplayerUI.DisableButtons();
        NetworkManager.Singleton.StartHost();
    }

    private void StartClient()
    {
        m_multiplayerUI.DisableButtons();
        NetworkManager.Singleton.StartClient();
    }

    private void DisconnectClient()
    {
        m_multiplayerUI.EnableButtons();
        NetworkManager.Singleton.Shutdown();
    }
}