using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private string m_gameSceneName = "VRBoxingScene";
    [SerializeField] private string m_startSceneName = "StartScene";

    private MultiplayerUI2 m_multiplayerUI2;
    private MultiplayerUI m_multiplayerUI;
    private LanConnectionConfig m_lanConfig;

    private static GameManager s_instance;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this;
        DontDestroyOnLoad(gameObject);

        m_lanConfig = FindAnyObjectByType<LanConnectionConfig>();

        if (m_lanConfig == null)
            Debug.LogError("LanConnectionConfig was not found in the scene.");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        m_lanConfig = GetComponent<LanConnectionConfig>();
        if (m_lanConfig == null)
            Debug.Log("LanConnectionConfig is not attached to GameManager");

        HookupSceneUI();
        SyncUIState();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HookupSceneUI();
        SyncUIState();
    }

    private void HookupSceneUI()
    {
        UnhookSceneUI();

        m_multiplayerUI2 = FindFirstObjectByType<MultiplayerUI2>();
        m_multiplayerUI = FindFirstObjectByType<MultiplayerUI>();

        if (m_multiplayerUI2 != null)
        {
            m_multiplayerUI2.OnStartHost += StartHost2;
            m_multiplayerUI2.OnStartClient += StartClient2;
            m_multiplayerUI2.OnDisconnectClient += DisconnectClient2;
        }

        if (m_multiplayerUI != null)
        {
            m_multiplayerUI.OnStartHost += StartHost;
            m_multiplayerUI.OnStartClient += StartClient;
            m_multiplayerUI.OnDisconnectClient += DisconnectClient;
        }
    }

    private void UnhookSceneUI()
    {
        if (m_multiplayerUI2 != null)
        {
            m_multiplayerUI2.OnStartHost -= StartHost2;
            m_multiplayerUI2.OnStartClient -= StartClient2;
            m_multiplayerUI2.OnDisconnectClient -= DisconnectClient2;
            m_multiplayerUI2 = null;
        }

        if (m_multiplayerUI != null)
        {
            m_multiplayerUI.OnStartHost -= StartHost;
            m_multiplayerUI.OnStartClient -= StartClient;
            m_multiplayerUI.OnDisconnectClient -= DisconnectClient;
            m_multiplayerUI = null;
        }
    }

    private void SyncUIState()
    {
        bool connected = NetworkManager.Singleton != null &&
                         (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient);

        if (connected)
        {
            m_multiplayerUI2?.DisableButtons();
            m_multiplayerUI?.DisableButtons();
        }
        else
        {
            m_multiplayerUI2?.EnableButtons();
            m_multiplayerUI?.EnableButtons();
        }
    }

    private void StartHost2()
    {
        if (NetworkManager.Singleton == null)
            return;

        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
        {
            SyncUIState();
            return;
        }

        m_lanConfig?.ConfigureForHost();

        bool started = NetworkManager.Singleton.StartHost();

        if (started)
        {
            SyncUIState();
            NetworkManager.Singleton.SceneManager.LoadScene(m_gameSceneName, LoadSceneMode.Single);
        }
    }

private void StartClient2()
{
    if (NetworkManager.Singleton == null)
        return;

    if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
    {
        SyncUIState();
        return;
    }

    m_lanConfig?.ConfigureForClient();

    bool started = NetworkManager.Singleton.StartClient();

    if (started)
    {
        SyncUIState();
    }
}

    private void DisconnectClient2()
    {
        DisconnectInternal();
    }

    private void StartHost()
    {
        if (NetworkManager.Singleton == null)
            return;

        if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
        {
            SyncUIState();
            return;
        }

        m_lanConfig?.ConfigureForHost();

        bool started = NetworkManager.Singleton.StartHost();

        if (started)
        {
            SyncUIState();
            NetworkManager.Singleton.SceneManager.LoadScene(m_gameSceneName, LoadSceneMode.Single);
        }
    }

    private void StartClient()
{
    if (NetworkManager.Singleton == null)
        return;

    if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
    {
        SyncUIState();
        return;
    }

    m_lanConfig?.ConfigureForClient();

    bool started = NetworkManager.Singleton.StartClient();

    if (started)
    {
        SyncUIState();
    }
}

    private void DisconnectClient()
    {
        DisconnectInternal();
    }

    private void DisconnectInternal()
    {
        if (NetworkManager.Singleton != null &&
            (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient))
        {
            NetworkManager.Singleton.Shutdown();
        }

        SyncUIState();

        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name != m_startSceneName)
        {
            SceneManager.LoadScene(m_startSceneName, LoadSceneMode.Single);
        }
    }
}