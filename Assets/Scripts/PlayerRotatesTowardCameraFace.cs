using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRotatesTowardCameraFace : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    public PlayerController playerController;
    [HideInInspector] public float hRotation, vRotation;
    float oldH, oldV;

    private void Start()
    {
        UpdateRotationSpeed();
    }
    void UpdateRotationSpeed()
    {

        hRotation = playerController.horizontalRotationSpeed; //update the camera rotation speed from the settings in the player controller script
        vRotation = playerController.verticalRotationSpeed;
        if ((oldH != hRotation) || (oldV != vRotation)) //gets the values from the player controller and updates the camera if they have been altered
        {
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = hRotation;
            cinemachineVirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = vRotation;
            oldH = hRotation;
            oldV = vRotation;
        }

    }

    void Update()
    {
        UpdateRotationSpeed();

        // Get the camera's rotation
        Quaternion cameraRotation = mainCamera.transform.rotation;

        // Create a new rotation that matches the camera's Y rotation
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, cameraRotation.eulerAngles.y, transform.eulerAngles.z);

        // Set the player's rotation
        transform.rotation = newRotation;
    }
}
