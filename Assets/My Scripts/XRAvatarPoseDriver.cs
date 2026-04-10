using Unity.Netcode;
using UnityEngine;

public class XRAvatarPoseDriver : NetworkBehaviour
{
    [SerializeField] private Transform headTarget;
    [SerializeField] private Transform leftHandTarget;
    [SerializeField] private Transform rightHandTarget;

    private Transform localHead;
    private Transform localLeftHand;
    private Transform localRightHand;

    public override void OnNetworkSpawn()
    {
        Debug.Log($"{name} XRAvatarPoseDriver OnNetworkSpawn | IsOwner={IsOwner}");

        if (!IsOwner)
            return;

        TryGetRigReferences();
    }

    private void LateUpdate()
    {
        if (!IsOwner)
            return;

        if (localHead == null || localLeftHand == null || localRightHand == null)
        {
            TryGetRigReferences();
            return;
        }

        if (headTarget != null)
        {
            headTarget.position = localHead.position;
            headTarget.rotation = localHead.rotation;
        }

        if (leftHandTarget != null)
        {
            leftHandTarget.position = localLeftHand.position;
            leftHandTarget.rotation = localLeftHand.rotation;
        }

        if (rightHandTarget != null)
        {
            rightHandTarget.position = localRightHand.position;
            rightHandTarget.rotation = localRightHand.rotation;
        }
    }

    private void TryGetRigReferences()
    {
        if (PersistentXRRigReferences.Instance == null)
        {
            Debug.LogWarning("PersistentXRRigReferences.Instance not found yet.");
            return;
        }

        localHead = PersistentXRRigReferences.Instance.Head;
        localLeftHand = PersistentXRRigReferences.Instance.LeftHand;
        localRightHand = PersistentXRRigReferences.Instance.RightHand;

        Debug.Log(
            $"{name} rig refs found | head={(localHead != null)} | left={(localLeftHand != null)} | right={(localRightHand != null)}"
        );
    }
}