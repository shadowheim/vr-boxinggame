using Unity.Netcode;
using UnityEngine;

public class IgnoreOwnHurtboxGloveCollisions : NetworkBehaviour
{
    [SerializeField] private Collider leftGloveCollider;
    [SerializeField] private Collider rightGloveCollider;
    [SerializeField] private Collider ownHurtboxCollider;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;

        if (leftGloveCollider != null && ownHurtboxCollider != null)
            Physics.IgnoreCollision(leftGloveCollider, ownHurtboxCollider, true);

        if (rightGloveCollider != null && ownHurtboxCollider != null)
            Physics.IgnoreCollision(rightGloveCollider, ownHurtboxCollider, true);
    }
}