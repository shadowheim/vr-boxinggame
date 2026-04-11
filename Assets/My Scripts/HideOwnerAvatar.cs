using Unity.Netcode;
using UnityEngine;

public class HideOwnerAvatar : NetworkBehaviour
{
    [SerializeField] private Renderer[] renderersToHide;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
            return;

        foreach (var rend in renderersToHide)
        {
            if (rend != null)
                rend.enabled = false;
        }
    }
}