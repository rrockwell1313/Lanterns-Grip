using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    private Vector2 movementInput;
    private Vector2 rotationInput;
    [SerializeField] float rotationSpeed = 0.0f;

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
        if (rotationInput != Vector2.zero)
        {
            RotatePlayer();
        }
    }

    public void MovePlayer()
    {
        //Debug.Log("Movement input: " + movementInput);
        transform.Translate(new Vector3(movementInput.x, 0f , movementInput.y) * Time.deltaTime);
    }
    public void RotatePlayer()
    {
        Debug.Log("Rotation input: " + rotationInput);
        float rotationY = rotationInput.x * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotationY, 0f);
    }
}
