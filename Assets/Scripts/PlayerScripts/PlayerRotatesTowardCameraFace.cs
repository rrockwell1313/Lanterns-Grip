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
    float rotationSpeed = 85f;
    Quaternion newRotation;

    private void Start()
    {
        UpdateRotationSpeed();
    }
    void LateUpdate()
    {
        UpdateRotationSpeed();

        // Get the target rotation which is the camera's Y rotation
        Quaternion targetRotation = Quaternion.Euler(0f, mainCamera.transform.eulerAngles.y, 0f);

        // Smoothly interpolate between the current rotation and the target rotation
        // 'rotationSpeed' is a public float that you can adjust in the inspector to control the speed of rotation
        newRotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Apply the new rotation
        transform.rotation = newRotation;
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


}
