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
    private PlayerJump jumpScript;
    private PlayerCrouch crouchScript;


    private void Start()
    {

        interactScript = GetComponent<PlayerInteract>();
        movementScript = GetComponent<PlayerMovement>();
        jumpScript = GetComponent<PlayerJump>();
        crouchScript = GetComponent<PlayerCrouch>();

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
                crouchScript.Crouch();
            }
            else if (!crouchPressed)
            {
                movementScript.DisableCrouch();
                crouchScript.StandUp();
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
}
