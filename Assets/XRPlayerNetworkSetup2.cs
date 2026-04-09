using Unity.Netcode;
using UnityEngine;

public class XRPlayerNetworkSetup2 : NetworkBehaviour
{
    [SerializeField] private GameObject mainCameraObject;
    [SerializeField] private GameObject locomotionObject;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private MonoBehaviour[] ownerOnlyScripts;

    public override void OnNetworkSpawn()
    {
        Debug.Log(
            $"{name} | OwnerClientId={OwnerClientId} | LocalClientId={NetworkManager.Singleton.LocalClientId} | IsOwner={IsOwner}"
        );

        ApplyOwnerState(IsOwner);
    }

    private void ApplyOwnerState(bool isOwner)
    {
        if (mainCameraObject != null)
            mainCameraObject.SetActive(isOwner);

        if (locomotionObject != null)
            locomotionObject.SetActive(isOwner);

        if (characterController != null)
            characterController.enabled = isOwner;

        if (ownerOnlyScripts != null)
        {
            foreach (var script in ownerOnlyScripts)
            {
                if (script != null)
                    script.enabled = isOwner;
            }
        }
    }
}