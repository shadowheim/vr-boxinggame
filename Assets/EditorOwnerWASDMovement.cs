using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class EditorOwnerWASDMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotationSpeed = 120f;

    private float yaw;

    public override void OnNetworkSpawn()
    {
#if UNITY_EDITOR
        if (!IsOwner)
        {
            enabled = false;
            return;
        }

        yaw = transform.eulerAngles.y;
        Debug.Log($"{name} EditorOwnerWASDMovement enabled for owner {OwnerClientId}");
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (characterController == null || Keyboard.current == null)
            return;

        HandleRotation();
        HandleMovement();
#endif
    }

    private void HandleRotation()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.rightButton.isPressed)
        {
            float mouseX = Mouse.current.delta.ReadValue().x;
            yaw += mouseX * rotationSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }

    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current.wKey.isPressed) input.y += 1f;
        if (Keyboard.current.sKey.isPressed) input.y -= 1f;
        if (Keyboard.current.dKey.isPressed) input.x += 1f;
        if (Keyboard.current.aKey.isPressed) input.x -= 1f;

        Vector3 forward = cameraTransform != null ? cameraTransform.forward : transform.forward;
        Vector3 right = cameraTransform != null ? cameraTransform.right : transform.right;

        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        Vector3 move = forward * input.y + right * input.x;
        move = Vector3.ClampMagnitude(move, 1f);

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }
}