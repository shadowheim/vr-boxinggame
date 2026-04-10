using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistAcrossScenes : MonoBehaviour
{
    private static PersistAcrossScenes s_instance;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this;
        DontDestroyOnLoad(gameObject);

        Debug.Log("Persistent XR rig initialised");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Persistent XR rig survived into scene: " + scene.name);
    }
}