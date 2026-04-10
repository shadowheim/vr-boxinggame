using UnityEngine;

public class PersistentEventSystem : MonoBehaviour
{
    private static PersistentEventSystem s_instance;

    private void Awake()
    {
        if (s_instance != null && s_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        s_instance = this;
        DontDestroyOnLoad(gameObject);
    }
}