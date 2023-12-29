using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerRotatesTowardCameraFace : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera cinemachineVirtualCamera;


    void Update()
    {
        // Get the camera's rotation
        Quaternion cameraRotation = mainCamera.transform.rotation;

        // Create a new rotation that matches the camera's Y rotation
        Quaternion newRotation = Quaternion.Euler(transform.eulerAngles.x, cameraRotation.eulerAngles.y, transform.eulerAngles.z);

        // Set the player's rotation
        transform.rotation = newRotation;
    }
}
