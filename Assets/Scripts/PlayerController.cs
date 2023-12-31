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
    public float walkSpeed, sprintSpeed;
    public bool sprintPressed, isMoving, isSprinting;

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



    public void MovePlayer()
    {
        //Debug.Log("Movement input: " + movementInput);
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
            transform.Translate(new Vector3(movementInput.x, 0f, movementInput.y) * moveSpeed * Time.deltaTime);
        }

    }
}
