using Unity.Netcode;
using UnityEngine;

public class IgnoreOwnRigGloveCollisions : NetworkBehaviour
{
    [SerializeField] private Collider leftGloveCollider;
    [SerializeField] private Collider rightGloveCollider;

    // Update is called once per frame
    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;

        ApplyIgnore();
    }

    private void ApplyIgnore()
    {
        if (PersistentXRRigReferences.Instance == null)
        {
            Debug.LogWarning("PersistentXRRigReferences.Instance not found.", this);
            return;
        }

        CharacterController ownCharacterController = PersistentXRRigReferences.Instance.CharacterController;
        if (ownCharacterController == null)
        {
            Debug.LogWarning("Own CharacterController not found", this);
            return;
        }

        if (leftGloveCollider != null)
            Physics.IgnoreCollision(leftGloveCollider, ownCharacterController, true);

        if (rightGloveCollider != null)
            Physics.IgnoreCollision(rightGloveCollider, ownCharacterController, true);
    }
}
