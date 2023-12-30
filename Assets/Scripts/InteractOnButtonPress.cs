using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractOnButtonPress : MonoBehaviour
{
    bool interactPressed;
    bool canInteract;
    public bool playerInteracting;
    GameObject interactable;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            interactPressed = true;
        }
        else if (context.canceled)
        {
            interactPressed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable")) // Ensure your ground has the "Ground" tag
        {
            interactable = other.gameObject;
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactable = null;
            canInteract = false;
        }
    }

    private void Update()
    {
        if (!canInteract)
        {
            interactable = null;
        }
        if (interactPressed && canInteract) 
        {
            Interact();
        }
    }

    void Interact()
    {
        canInteract = false;
        playerInteracting = true;
        Debug.Log("Interacting");
        interactable.tag = "Untagged";
    }
}
