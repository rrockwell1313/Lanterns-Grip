using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Vector2 movementInput;
    private Vector2 rotationInput;
    private float moveSpeed = 0.0f;
    public float horizontalRotationSpeed = 150.0f;
    public float verticalRotationSpeed = 100f;
    public float walkSpeed, sprintSpeed, crouchSpeed;
    bool sprintPressed, crouchPressed;
    public bool isMoving, isSprinting; //accessible for other states?

    void Update()
    {
        MovePlayer();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();     
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            sprintPressed = true;
        }
        else if (context.canceled)
        {
            sprintPressed = false;
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            crouchPressed = !crouchPressed; //toggle crouch instead of hold for right now
        }

    }



    public void MovePlayer()
    {
        if (movementInput != Vector2.zero)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
            isSprinting = false;
        }
        
        if (isMoving)
        {
            if (crouchPressed)
            {
                moveSpeed = crouchSpeed;
            }
            else if (!crouchPressed)
            {
                if (sprintPressed)
                {
                    moveSpeed = sprintSpeed;
                    isSprinting = true;
                }
                else if (!sprintPressed)
                {
                    moveSpeed = walkSpeed;
                    isSprinting = false;
                }
            }
            
            transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime); //actual code that does the movement
        }

    }
}
