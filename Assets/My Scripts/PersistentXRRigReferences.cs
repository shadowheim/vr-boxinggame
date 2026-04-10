using UnityEngine;

public class PersistentXRRigReferences : MonoBehaviour
{
    public static PersistentXRRigReferences Instance { get; private set; }

    [SerializeField] private Transform head;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    public Transform Head => head;
    public Transform LeftHand => leftHand;
    public Transform RightHand => rightHand;

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