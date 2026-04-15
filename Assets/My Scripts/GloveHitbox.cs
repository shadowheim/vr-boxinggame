using Unity.Netcode;
using UnityEngine;

public class GloveHitbox : NetworkBehaviour
{
    [SerializeField] private float knockbackStrength = 10f;
    [SerializeField] private float minHitSpeed = 0f;
    [SerializeField] private float hitCooldown = 0.3f;

    private Vector3 m_lastPosition;
    private float m_cooldownTimer;

    private void Start()
    {
        Debug.Log("GloveHitbox script is alive!");
        m_lastPosition = transform.position;
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if (m_cooldownTimer > 0f)
            m_cooldownTimer -= Time.deltaTime;
    }

    private void LateUpdate()
    {
        m_lastPosition = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsOwner)
            return;

        if (m_cooldownTimer > 0f)
            return;

        PlayerHurtBox hurtBox = other.GetComponentInParent<PlayerHurtBox>();
        if (hurtBox == null)
            return;

        NetworkObject targetNetworkObject = hurtBox.GetComponentInParent<NetworkObject>();
        if (targetNetworkObject == null)
            return;

        ulong targetClientId = targetNetworkObject.OwnerClientId;

        Debug.Log($"Glove owner: {OwnerClientId}, target owner: {targetClientId}, hit object: {other.name}");

        if (targetClientId == OwnerClientId)
        {
            Debug.Log("Ignoring self-hit");
            return;
        }

        float handSpeed = ((transform.position - m_lastPosition) / Mathf.Max(Time.deltaTime, 0.0001f)).magnitude;
        Debug.Log("Hand speed: " + handSpeed);

        if (handSpeed < minHitSpeed)
        {
            Debug.Log("Rejected hit because speed was too low");
            return;
        }

        Vector3 direction = (hurtBox.transform.position - transform.position).normalized;
        direction.y = 0f;

        if (direction.sqrMagnitude < 0.0001f)
            direction = transform.forward;

        Vector3 knockbackVelocity = direction.normalized * knockbackStrength;

        Debug.Log("Sending knockback ServerRpc to target client: " + targetClientId);
        RequestKnockbackServerRpc(targetClientId, knockbackVelocity);

        m_cooldownTimer = hitCooldown;
    }

    [ServerRpc]
    private void RequestKnockbackServerRpc(ulong targetClientId, Vector3 knockbackVelocity)
    {
        Debug.Log("[SERVER] RequestKnockbackServerRpc received. Target client: " + targetClientId);

        ClientRpcParams rpcParams = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new[] { targetClientId }
            }
        };

        ApplyKnockbackClientRpc(knockbackVelocity, rpcParams);
    }

    [ClientRpc]
    private void ApplyKnockbackClientRpc(Vector3 knockbackVelocity, ClientRpcParams clientRpcParams = default)
    {
        Debug.Log("[CLIENT] ApplyKnockbackClientRpc received on local client: " + NetworkManager.Singleton.LocalClientId);

        if (PersistentXRRigReferences.Instance == null)
        {
            Debug.Log("[CLIENT] PersistentXRRigReferences.Instance is null");
            return;
        }

        LocalRigKnockbackMotor motor = PersistentXRRigReferences.Instance.KnockbackMotor;
        if (motor == null)
        {
            Debug.Log("[CLIENT] KnockbackMotor is null");
            return;
        }

        Debug.Log("[CLIENT] Applying knockback to persistent local rig");
        motor.ApplyKnockback(knockbackVelocity);
    }
}