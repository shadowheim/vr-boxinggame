using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.NativeTypes;
public class MovementTestForWASD : MonoBehaviour
{
    private void Update()
    {
        Vector2 movementInput = Vector2.zero;
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.IsPressed())
                movementInput.x = -1f;
            else if (Keyboard.current.dKey.IsPressed())
                movementInput.x = 1f;

            if (Keyboard.current.wKey.IsPressed())
                movementInput.y = 1f;
            else if (Keyboard.current.sKey.IsPressed())
                movementInput.y = -1f;
        }
        Move(movementInput);
    
    }
    [SerializeField]
    private CharacterController XR_WASD_Controller;
    [SerializeField]
    private Transform xrCamera;

    [SerializeField]
    private float XR_movementSpeed = 4f;
    [SerializeField]
    private float XR_rotationSpeed = 200f;

    public void Move(Vector2 movementInput)
    {
        // rotate left/right
        transform.Rotate(Vector3.up, movementInput.x * XR_rotationSpeed * Time.deltaTime);

        // move forward/backward
        Vector3 direction = transform.forward;
        direction.y = 0f;
        direction.Normalize();
        XR_WASD_Controller.Move(movementInput.y * Time.deltaTime * XR_movementSpeed * direction);
        
    }
}
