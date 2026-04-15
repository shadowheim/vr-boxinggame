using Unity.Netcode;
using UnityEngine;

public class IgnoreOwnGloveCollisions : NetworkBehaviour
{
    [SerializeField] private Collider leftGloveCollider;
    [SerializeField] private Collider rightGloveCollider;
    [SerializeField] private CharacterController ownCharacterController;

    public override void OnNetworkSpawn()
    {
        ApplyIgnore();
    }
    private void ApplyIgnore()
    {
        if (ownCharacterController == null)
        {
            Debug.LogWarning("IgnoreOwnCollisions: ownerCharacterController is not assigned.");
            return;
        }

        Collider bodyCollider = ownCharacterController;

        if (leftGloveCollider != null)
        {
            Physics.IgnoreCollision(leftGloveCollider, bodyCollider, true);
        }

        if (rightGloveCollider != null)
        {
            Physics.IgnoreCollision(rightGloveCollider, bodyCollider, true);
        }
    }


}
