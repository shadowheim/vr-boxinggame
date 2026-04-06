using UnityEngine;

public class XR_Rig_Camera_Yaw_Character_rotation : MonoBehaviour
{
    [SerializeField] private Transform xrCamera;
    [SerializeField] private bool smoothRotation = true;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Vector3 modelForwardOffsetEuler;

    private void LateUpdate()
    {
        if (xrCamera == null) return;

        Vector3 forward = xrCamera.forward;
        forward.y = 0f;

        if (forward.sqrMagnitude < 0.0001f) return;

        Quaternion targetRotation = Quaternion.LookRotation(forward.normalized, Vector3.up);
        targetRotation *= Quaternion.Euler(modelForwardOffsetEuler);

        if (smoothRotation)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
        else
        {
            transform.rotation = targetRotation;
        }
    }
}