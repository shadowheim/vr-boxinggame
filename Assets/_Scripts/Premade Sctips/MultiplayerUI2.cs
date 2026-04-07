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

    private void Start()
    {
        XR_hostButton.onClick.AddListener(() => OnStartHost?.Invoke());
        XR_clientButton.onClick.AddListener(() => OnStartClient?.Invoke());
        XR_clientDisconnect.onClick.AddListener(() => OnDisconnectClient?.Invoke());

        EnableButtons();
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
