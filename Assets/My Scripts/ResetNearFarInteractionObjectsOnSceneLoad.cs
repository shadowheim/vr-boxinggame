using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetNearFarInteractorObjectsOnSceneLoad : MonoBehaviour
{
    [SerializeField] private GameObject[] nearFarInteractorObjects;
    [SerializeField] private float resetDelay = 0.15f;

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

        if (nearFarInteractorObjects == null)
            yield break;

        foreach (var obj in nearFarInteractorObjects)
        {
            if (obj != null)
                obj.SetActive(false);
        }

        yield return null;
        yield return null;

        foreach (var obj in nearFarInteractorObjects)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}