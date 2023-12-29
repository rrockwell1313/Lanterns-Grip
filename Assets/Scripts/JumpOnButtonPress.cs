using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JumpOnButtonPress : MonoBehaviour
{
    bool jumpPressed;
    bool touchingGround;
    bool onGround;
    bool playerJumping;
    [SerializeField] private Rigidbody rb;
    public float jumpForce = 5f;
    GameObject floor;

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            jumpPressed = true;
            Jump();
            Debug.Log("Touching Ground?: " + touchingGround);
        }
        else if (context.canceled)
        {
            jumpPressed = false;
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with the ground
        if (collision.gameObject.CompareTag("Ground")) // Ensure your ground has the "Ground" tag
        {
            touchingGround = true;
            if (playerJumping)
            {
                playerJumping = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Check if the box is no longer colliding with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            touchingGround = false;
        }
    }

    void Jump()
    {
        //do things here for the jump

        if (!playerJumping && touchingGround)
        {         
            playerJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump mah boy");
        }


    }
}
