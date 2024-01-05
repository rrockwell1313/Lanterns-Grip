using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCameraManager : MonoBehaviour
{
    GameObject follower;
    GameObject lookAt;
    Cinemachine.CinemachineVirtualCamera virtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<Cinemachine.CinemachineVirtualCamera>();
        follower = GameObject.Find("Player");
        lookAt = GameObject.Find("Player");

        //Set the cinemachine Lookat and follow to follower and lookAt
        virtualCamera.Follow = follower.transform;
        virtualCamera.LookAt = lookAt.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
