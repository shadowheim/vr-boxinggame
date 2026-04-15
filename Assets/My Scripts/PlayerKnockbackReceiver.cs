using Unity.Netcode;
using UnityEngine;

public class PlayerKnockbackReceiver : NetworkBehaviour
{
    [SerializeField] private float knockBackDuration = 0.15f;

    private Vector3 m_knockbackVelocity;
    private float m_knockbackTimer;

    public void ApplyKnockback(Vector3 velocity)
    {
        if (!IsOwner)
        {
            Debug.Log("[KNOCKBACK] Ignored because this is not the owner");
            return;
        }

        Debug.Log("[KNOCKBACK] ApplyKnockback called with velocity: " + velocity);

        m_knockbackVelocity = velocity;
        m_knockbackTimer = knockBackDuration;
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if (m_knockbackTimer <= 0f)
            return;

        if (PersistentXRRigReferences.Instance == null)
        {
            Debug.Log("[KNOCKBACK] PersistentXRRigReferences.Instance is null");
            return;
        }

        Transform rigRoot = PersistentXRRigReferences.Instance.RigRoot;
        if (rigRoot == null)
        {
            Debug.Log("[KNOCKBACK] RigRoot is null");
            return;
        }

        Debug.Log("[KNOCKBACK] Moving rig root with knockback: " + m_knockbackVelocity);

        rigRoot.position += m_knockbackVelocity * Time.deltaTime;
        m_knockbackTimer -= Time.deltaTime;
    }
}