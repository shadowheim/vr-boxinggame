using UnityEngine;

public class LocalRigKnockbackMotor : MonoBehaviour
{
    [SerializeField] private float damping = 10f;

    private Vector3 m_velocity;

    public void ApplyKnockback(Vector3 velocity)
    {
        Debug.Log("[KNOCKBACK MOTOR] ApplyKnockback called with velocity: " + velocity);
        m_velocity = velocity;
    }

    private void LateUpdate()
    {
        if (m_velocity.sqrMagnitude < 0.0001f)
            return;

        Debug.Log("[KNOCKBACK MOTOR] Moving rig root with velocity: " + m_velocity);

        transform.position += m_velocity * Time.deltaTime;
        m_velocity = Vector3.Lerp(m_velocity, Vector3.zero, damping * Time.deltaTime);
    }
}