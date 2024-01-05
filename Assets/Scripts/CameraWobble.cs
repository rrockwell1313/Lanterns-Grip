using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraWobble : MonoBehaviour
{
    public PlayerController controller;
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    public float dutchMax, sprintDutchMax, oscillationSpeed, sprintOscillationSpeed;
    private float time = 0f;
    private Vector2 movementInput;
    private float currentDutch;
    bool sprinting;


    void Update()
    {
        cinemachineVirtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        movementInput = controller.movementInput; //get this value from the controller script

        float inputMagnitude = movementInput.magnitude;

        if (inputMagnitude > 0.01f) // Threshold to check for movement
        {
            float currentOscillationSpeed = sprinting ? sprintOscillationSpeed : oscillationSpeed; //YOU ARE HERE
            float maxDutch = sprinting ? sprintDutchMax : dutchMax;

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
    public void EnableSprinting()
    {
        sprinting = true;
    }
    public void DisableSprinting()
    {
        sprinting = false;
    }
}
