using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    PlayerController controller;
    PlayerMovement movement;
    private CapsuleCollider playerCollider;
    private float originalHeight;
    public float crouchHeight = 0.5f; // Desired height when crouching

    // Start is called before the first frame update
    void Start()
    {
        PlayerController controller = GetComponent<PlayerController>();
        playerCollider = GetComponent<CapsuleCollider>();
        originalHeight = playerCollider.height;
    }
    public void Crouch()
    {
        playerCollider.height = crouchHeight;
    }

    public void StandUp()
    {
        playerCollider.height = originalHeight;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
