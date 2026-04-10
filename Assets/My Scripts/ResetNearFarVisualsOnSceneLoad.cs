using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetNearFarVisualsOnSceneLoad : MonoBehaviour
{
    [SerializeField] private Behaviour[] visualControllers;
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

        if (visualControllers == null)
            yield break;

        foreach (var controller in visualControllers)
        {
            if (controller != null)
                controller.enabled = false;
        }

        yield return null;

        foreach (var controller in visualControllers)
        {
            if (controller != null)
                controller.enabled = true;
        }
    }
}