using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraWobble : MonoBehaviour
{
    public PlayerController controller;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public float dutchMax, sprintDutchMax, oscillationSpeed, sprintOscillationSpeed;
    private float time = 0f;
    private Vector2 movementInput;
    private float currentDutch;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        float inputMagnitude = movementInput.magnitude;

        if (inputMagnitude > 0.01f) // Threshold to check for movement
        {
            float currentOscillationSpeed = controller.isSprinting ? sprintOscillationSpeed : oscillationSpeed;
            float maxDutch = controller.isSprinting ? sprintDutchMax : dutchMax;

            // Scale the oscillation speed based on the joystick input magnitude
            currentOscillationSpeed *= inputMagnitude;

            time += Time.deltaTime * currentOscillationSpeed;
            currentDutch = maxDutch * Mathf.Sin(time);

            // Smoothly interpolate to the new Dutch angle
            cinemachineVirtualCamera.m_Lens.Dutch = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.Dutch, currentDutch, Time.deltaTime * currentOscillationSpeed);
        }
        else
        {
            // Gradually return to 0 when there's no movement
            cinemachineVirtualCamera.m_Lens.Dutch = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.Dutch, 0, Time.deltaTime * oscillationSpeed);
            time = 0f; // Reset the time to ensure smooth restart of oscillation
        }
    }
}
