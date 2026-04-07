using UnityEngine;

public class XRPlayerAnimatorDriver : MonoBehaviour
{
    [SerializeField]
    private Transform xrRigRoot;
    [SerializeField]
    private Animator animator;
    private Vector3 lastPosition;
    private void Start()
    {
        if(xrRigRoot == null)
        xrRigRoot = transform.root;

        lastPosition = xrRigRoot.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 delta = xrRigRoot.position - lastPosition;
        delta.y = 0f;

        float speed = delta.magnitude / Time.deltaTime;

        animator.SetFloat("Movement", speed);

        lastPosition = xrRigRoot.position;
    }
}
