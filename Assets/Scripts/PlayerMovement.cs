using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerController controller;
    bool crouching, sprinting;
    float moveSpeed, walkSpeed, crouchSpeed, sprintSpeed;
    Vector2 movementInput;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void MovePlayer()
    {
        if (crouching)
        {
            moveSpeed = crouchSpeed;
        }
        else if (sprinting)
        {
            moveSpeed += sprintSpeed;
        }
        else 
        {
            moveSpeed = walkSpeed;
        }
        
        movementInput = controller.movementInput;
        transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime); //actual code that does the movement
    }
    public void EnableSprint()
    {
        sprinting = true;
    }
    public void DisableSprint()
    {
        sprinting = false;
    }

}
