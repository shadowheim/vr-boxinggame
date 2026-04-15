using UnityEngine;

public class PersistentXRRigReferences : MonoBehaviour
{
    public static PersistentXRRigReferences Instance { get; private set; }

    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private LocalRigKnockbackMotor knockbackMotor;

    public Transform Head => head;
    public Transform LeftHand => leftHand;
    public Transform RightHand => rightHand;
    public CharacterController CharacterController => characterController;
    public Transform RigRoot => transform;
    public LocalRigKnockbackMotor KnockbackMotor => knockbackMotor;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}