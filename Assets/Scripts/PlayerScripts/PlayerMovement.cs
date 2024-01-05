using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController controller;
    bool crouching, sprinting, moving;
    public float walkSpeed, crouchSpeed, sprintSpeed;
    float moveSpeed;
    Vector2 movementInput;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (moving)
        {
            MovePlayer();
        }
    }

    public void MovePlayer()
    {
        if (crouching)
        {
            moveSpeed = crouchSpeed;
        }
        else if (sprinting)
        {
            moveSpeed = sprintSpeed;
        }
        else 
        {
            moveSpeed = walkSpeed;
        }
        
        movementInput = controller.movementInput;
        transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime); //actual code that does the movement
    }

    //receive all of the below from the PlayerController (or elsewhere)
    public void EnableMovement()
    {
        moving = true;
    }
    public void DisableMovement()
    {
        moving = false;
    }
    public void EnableSprint()
    {
        sprinting = true;
    }
    public void DisableSprint()
    {
        sprinting = false;
    }
    public void EnableCrouch()
    {
        crouching = true;
    }
    public void DisableCrouch()
    {
        crouching = false;
    }
}
