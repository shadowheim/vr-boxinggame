using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetControllerInteractorsOnSceneLoad : MonoBehaviour
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
        StartCoroutine(ResetRoutine());
    }

    private IEnumerator ResetRoutine()
    {
        yield return null;

        if (resetDelay > 0f)
            yield return new WaitForSeconds(resetDelay);

        if (interactorsToReset == null)
            yield break;

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