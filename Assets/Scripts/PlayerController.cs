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
    
    private InteractOnButtonPress interactScript;
    private PlayerMovement movementScript;
    private LanternController lanternScript;


    private void Start()
    {
        interactScript = GetComponent<InteractOnButtonPress>();
        movementScript = GetComponent<PlayerMovement>();
        lanternScript  = GetComponent<LanternController>();
    }

    void Update()
    {
        
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        if (movementInput  != Vector2.zero)
        {
            movementScript.MovePlayer();
        }
        else
        {
            return;
        }
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementScript.EnableSprint();
        }
        else if (context.canceled)
        {
            movementScript.DisableSprint();
        }
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            crouchPressed = !crouchPressed; //toggle crouch instead of hold for right now
        }

    }
    public void OnInteract(InputAction.CallbackContext context)
    // Called when the interact button is pressed
    {
        if (context.started)
        {
            interactScript.AttemptInteraction();
        }
 
    }
    public void OnLanternIncrease(InputAction.CallbackContext context)
    // Called when the interact button is pressed
    {
        if (context.started)
        {
            lanternScript.AdjustLantern();
        }

    }
    public void OnLanternDecrease(InputAction.CallbackContext context)
    // Called when the interact button is pressed
    {
        if (context.started)
        {
            lanternScript.AdjustLantern();
        }

    }


}
