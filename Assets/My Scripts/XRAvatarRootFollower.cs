using Unity.Netcode;
using UnityEngine;

public class XRAvatarRootFollower : NetworkBehaviour
{
    [SerializeField] private Transform headTarget;
    [SerializeField] private float floorOffset = -1.7f;

    private void LateUpdate()
    {
        if (!IsOwner)
            return;

        if (headTarget == null)
            return;

        Vector3 targetPosition = headTarget.position;
        targetPosition.y += floorOffset;

        transform.position = targetPosition;
    }
}