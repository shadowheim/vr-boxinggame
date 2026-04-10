using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RefreshXRUIOnSceneLoadAndResume : MonoBehaviour
{
    [SerializeField] private Behaviour[] interactorsToReset;
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
        StartCoroutine(RefreshRoutine());
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            StartCoroutine(RefreshRoutine());
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            StartCoroutine(RefreshRoutine());
        }
    }

    private IEnumerator RefreshRoutine()
    {
        yield return null;

        if (resetDelay > 0f)
            yield return new WaitForSeconds(resetDelay);

        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null)
        {
            eventSystem.SetSelectedGameObject(null);

            var modules = eventSystem.GetComponents<BaseInputModule>();
            foreach (var module in modules)
            {
                if (module != null)
                    module.enabled = false;
            }

            yield return null;

            foreach (var module in modules)
            {
                if (module != null)
                    module.enabled = true;
            }
        }

        if (interactorsToReset != null)
        {
            foreach (var interactor in interactorsToReset)
            {
                if (interactor != null)
                    interactor.enabled = false;
            }

            yield return null;

            foreach (var interactor in interactorsToReset)
            {
                if (interactor != null)
                    interactor.enabled = true;
            }
        }
    }
}