using Unity.Netcode;
using UnityEngine;

public class NetworkDebugCallbacks : MonoBehaviour
{
    private void Start()
    {
        if (NetworkManager.Singleton == null)
            return;

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton == null)
            return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"[NET] Client connected: {clientId}");
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log($"[NET] Client disconnected: {clientId}");
    }
}