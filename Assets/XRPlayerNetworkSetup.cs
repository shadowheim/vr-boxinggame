using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.SocialPlatforms;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class XRPlayerNetworkSetup : NetworkBehaviour
{
    [Header("Local-only objects/components")]
    [SerializeField] private Camera playerCamera;
    [SerializeField] private AudioListener audioListener;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
            SetLocalPlayerState(true);
        else
            SetLocalPlayerState(false);
    }
    private void SetLocalPlayerState(bool isLocalPlayer)
    {
        if (playerCamera != null)
            playerCamera.enabled = isLocalPlayer;

        if (audioListener != null)
            audioListener.enabled = isLocalPlayer;

        if (characterController != null)
            characterController.enabled = isLocalPlayer;

        if (dynamicMoveProvider != null)
            dynamicMoveProvider.enabled = isLocalPlayer;
    }
}
