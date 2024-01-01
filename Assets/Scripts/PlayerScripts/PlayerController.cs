using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Vector2 movementInput;
    private Vector2 rotationInput;
    public float horizontalRotationSpeed = 150.0f;
    public float verticalRotationSpeed = 100f;
    bool sprintPressed, crouchPressed, jumpPressed; 
    private PlayerInteract interactScript;
    private PlayerMovement movementScript;
    private LanternController lanternScript;
    private PlayerJump jumpScript;


    private void Start()
    {
        interactScript = GetComponent<PlayerInteract>();
        movementScript = GetComponent<PlayerMovement>();
        lanternScript  = GetComponent<LanternController>();
        jumpScript = GetComponent<PlayerJump>();

    }

    void Update()
    {

    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        movementScript.EnableMovement();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpScript.CheckGround();
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
            if (crouchPressed)
            {
                movementScript.EnableCrouch();
            }
            else if (!crouchPressed)
            {
                movementScript.DisableCrouch();
            }
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
            lanternScript.increaseLantern = true;
        }
        else if (context.canceled)
        {
            lanternScript.increaseLantern = false;
        }
    }
    public void OnLanternDecrease(InputAction.CallbackContext context)
    // Called when the interact button is pressed
    {
        if (context.started)
        {
            lanternScript.decreaseLantern = true;
        }
        else if (context.canceled)
        {
            lanternScript.decreaseLantern = false;
        }
    }


}
