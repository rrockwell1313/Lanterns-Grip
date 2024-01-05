using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    bool touchingGround;
    bool onGround;
    bool playerJumping;
    private Rigidbody rb;
    public float jumpForce = 5f;
    public float groundCheckDistance;
    public float bottomOffset;

    RequireComponent Rigidbody;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void CheckGround()
    {
        //get point at the bottom of the collider
        Collider collider = GetComponent<Collider>(); //access the collider
        Vector3 bottomCenter = collider.bounds.center; //refer to the collider at the center, then set the y to the bottom
        bottomCenter.y -= collider.bounds.extents.y;
        bottomCenter.y += bottomOffset;


        touchingGround = Physics.Raycast(bottomCenter, Vector3.down, groundCheckDistance);
        if (touchingGround)
        {
            Debug.Log("Object is onGround");
            Jump();
        }
        else if (!touchingGround)
        {
            Debug.Log("Object is NOT onGround");
        }
    }
    void Jump()
    {
        //do things here for the jump
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
