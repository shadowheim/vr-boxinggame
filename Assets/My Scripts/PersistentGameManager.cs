using UnityEngine;

public class PersistentGameManager : MonoBehaviour
{
    private static PersistentGameManager s_instance;

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