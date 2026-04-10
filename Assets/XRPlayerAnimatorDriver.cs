using UnityEngine;

public class XRPlayerAnimatorDriver : MonoBehaviour
{
    [SerializeField] private Transform movementSource;
    [SerializeField] private Animator animator;

    private Vector3 lastPosition;

    private void Start()
    {
        if (movementSource == null)
        {
            Debug.LogWarning("XRPlayerAnimatorDriver: movementSource is not assigned.", this);
            enabled = false;
            return;
        }

        lastPosition = movementSource.position;
    }

    private void Update()
    {
        Vector3 delta = movementSource.position - lastPosition;
        delta.y = 0f;

        float speed = delta.magnitude / Time.deltaTime;
        animator.SetFloat("Movement", speed);

        lastPosition = movementSource.position;
    }
}