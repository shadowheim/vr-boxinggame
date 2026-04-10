using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ResetXRUIOnSceneLoad : MonoBehaviour
{
    [SerializeField] private float resetDelay = 0.05f;

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
        StartCoroutine(ResetUIRoutine());
    }

    private IEnumerator ResetUIRoutine()
    {
        yield return null;

        if (resetDelay > 0f)
            yield return new WaitForSeconds(resetDelay);

        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null)
            yield break;

        eventSystem.SetSelectedGameObject(null);

        var modules = eventSystem.GetComponents<BaseInputModule>();
        foreach (var module in modules)
        {
            if (module == null)
                continue;

            module.enabled = false;
        }

        yield return null;

        foreach (var module in modules)
        {
            if (module == null)
                continue;

            module.enabled = true;
        }
    }
}