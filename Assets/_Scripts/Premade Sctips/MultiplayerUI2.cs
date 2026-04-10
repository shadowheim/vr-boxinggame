using System;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayerUI2 : MonoBehaviour
{
    [SerializeField] private Button XR_hostButton;
    [SerializeField] private Button XR_clientButton;
    [SerializeField] private Button XR_clientDisconnect;

    public event Action OnStartHost;
    public event Action OnStartClient;
    public event Action OnDisconnectClient;

    private void OnEnable()
    {
        XR_hostButton.onClick.AddListener(HandleHostClicked);
        XR_clientButton.onClick.AddListener(HandleClientClicked);
        XR_clientDisconnect.onClick.AddListener(HandleDisconnectClicked);
    }

    private void OnDisable()
    {
        XR_hostButton.onClick.RemoveListener(HandleHostClicked);
        XR_clientButton.onClick.RemoveListener(HandleClientClicked);
        XR_clientDisconnect.onClick.RemoveListener(HandleDisconnectClicked);
    }

    private void HandleHostClicked()
    {
        OnStartHost?.Invoke();
    }

    private void HandleClientClicked()
    {
        OnStartClient?.Invoke();
    }

    private void HandleDisconnectClicked()
    {
        OnDisconnectClient?.Invoke();
    }

    public void DisableButtons()
    {
        XR_hostButton.interactable = false;
        XR_clientButton.interactable = false;
        XR_clientDisconnect.interactable = true;
    }

    public void EnableButtons()
    {
        XR_hostButton.interactable = true;
        XR_clientButton.interactable = true;
        XR_clientDisconnect.interactable = false;
    }
}