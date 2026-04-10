using System;
using UnityEngine;
using UnityEngine.UIElements;

public class MultiplayerUI : MonoBehaviour
{
    [SerializeField] private UIDocument m_uiDocument;

    private Button m_hostButton;
    private Button m_clientButton;
    private Button m_clientDisconnect;

    public event Action OnStartHost;
    public event Action OnStartClient;
    public event Action OnDisconnectClient;

    private void Awake()
    {
        m_hostButton = m_uiDocument.rootVisualElement.Q<Button>("ButtonHost");
        m_clientButton = m_uiDocument.rootVisualElement.Q<Button>("ButtonClient");
        m_clientDisconnect = m_uiDocument.rootVisualElement.Q<Button>("ButtonDisconnect");
    }

    private void OnEnable()
    {
        m_hostButton.clicked += HandleHostClicked;
        m_clientButton.clicked += HandleClientClicked;
        m_clientDisconnect.clicked += HandleDisconnectClicked;
    }

    private void OnDisable()
    {
        m_hostButton.clicked -= HandleHostClicked;
        m_clientButton.clicked -= HandleClientClicked;
        m_clientDisconnect.clicked -= HandleDisconnectClicked;
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
        m_hostButton.SetEnabled(false);
        m_clientButton.SetEnabled(false);
        m_clientDisconnect.SetEnabled(true);
    }

    public void EnableButtons()
    {
        m_hostButton.SetEnabled(true);
        m_clientButton.SetEnabled(true);
        m_clientDisconnect.SetEnabled(false);
    }
}