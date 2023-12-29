using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 movementInput;
    private Vector2 rotationInput;
    public float movementSpeed = 0.0f;

    public void OnMovement(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();     
    }

    public void OnRotation(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    void Update()
    {
        if (movementInput != Vector2.zero) 
        {
            MovePlayer();
        }
    }

    public void MovePlayer()
    {
        //Debug.Log("Movement input: " + movementInput);
        transform.Translate(new Vector3(movementInput.x, 0f , movementInput.y) * movementSpeed * Time.deltaTime);
    }
}
