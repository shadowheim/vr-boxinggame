using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetRayVisualsOnSceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] lineVisualObjects;
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
        StartCoroutine(ResetVisualsRoutine());
    }

    private IEnumerator ResetVisualsRoutine()
    {
        yield return null;

        if (resetDelay > 0f)
            yield return new WaitForSeconds(resetDelay);

        if (lineVisualObjects == null)
            yield break;

        foreach (var obj in lineVisualObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        yield return null;

        foreach (var obj in lineVisualObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}