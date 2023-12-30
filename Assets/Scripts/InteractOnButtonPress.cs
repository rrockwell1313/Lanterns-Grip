using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractOnButtonPress : MonoBehaviour
{
    bool interactPressed;
    bool canInteract;
    public bool playerInteracting;
    GameObject interactable;
    public Camera mainCamera;
    public float rayLength = 100f;
    public string targetTag = "Interactable";
    public Image reticle;
    public float reticleStartAlpha;
    public float reticleEndAlpha;
    public Image interactButtonOnImage;
    public Image interactButtonOffImage;
    public float interactButtonStartAlpha;
    public float interactButtonEndAlpha;
    public float interactImageFadeInTime;
    public float interactImageOnOffTime;
    float onOffTime;
    float currentAlpha;
    bool imageFading;


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
        if (other.gameObject.CompareTag("Interactable")) 
        {
            interactable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Interactable"))
        {
            interactable = null;
        }
    }

    private void Update()
    {
        ReticleRayCast();
        if (canInteract)
        {
            if (!imageFading)
            {
                InteractButtonAnimate();
            }
        }
        if (interactPressed && canInteract) 
        {
            Interact();
        }

        if (imageFading && canInteract)
        {
            InteractButtonFadeIn();
        }
        else if (imageFading &! canInteract)
        {
            InteractButtonFadeOut();
        }
    }
    void InteractButtonAnimate()
    {
        if (onOffTime < interactImageOnOffTime)
        {
            onOffTime += Time.deltaTime + 1f;
        }
        else
        {
            if (interactButtonOnImage.enabled)
            {
                interactButtonOnImage.enabled = false;
                onOffTime = 0f;
            }
            else
            {
                interactButtonOnImage.enabled = true;
                onOffTime = 0f;
            }
        }
    }
    void InteractButtonFadeIn()
    {
        //the image should fade in and then in the animate method it should switch between the two images
        //interactButtonOnImage.color = new Color(interactButtonOnImage.color.r, interactButtonOnImage.color.g, interactButtonOnImage.color.b, (interactButtonEndAlpha + currentAlpha) / 255);
        interactButtonOffImage.color = new Color(interactButtonOnImage.color.r, interactButtonOnImage.color.g, interactButtonOnImage.color.b, (interactButtonEndAlpha + currentAlpha) / 255);
        interactButtonOnImage.color = new Color(interactButtonOnImage.color.r, interactButtonOnImage.color.g, interactButtonOnImage.color.b, (interactButtonEndAlpha + currentAlpha) / 255);

        if (interactButtonEndAlpha + currentAlpha < interactButtonStartAlpha)
        {
            currentAlpha += (1 / interactImageFadeInTime);
        }
        else
        {
            imageFading = false;
        }

    }
    void InteractButtonFadeOut()
    {
        //image should fade out
        interactButtonOffImage.enabled = true; //use .enabled for images!!!
        interactButtonOffImage.color = new Color(interactButtonOnImage.color.r, interactButtonOnImage.color.g, interactButtonOnImage.color.b, (interactButtonEndAlpha + currentAlpha) / 255);
        if (interactButtonStartAlpha + currentAlpha > interactButtonEndAlpha)
        {
            currentAlpha -= (1 / interactImageFadeInTime);
        }
        else
        {
            imageFading = false;
        }
    }

    void ReticleRayCast()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        float startAlpha = reticleStartAlpha / 255f;
        float endAlpha = reticleEndAlpha / 255f;
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                reticle.color = new Color(reticle.color.r, reticle.color.g, reticle.color.b, startAlpha);
                if (!canInteract)
                {
                    canInteract = true;
                    imageFading = true;
                    interactButtonOnImage.enabled = true; //use .enabled for images!!!
                    interactButtonOffImage.enabled = true; //use .enabled for images!!!
                    currentAlpha = 0f;
                    InteractButtonFadeIn();
                }
                //could add a display interact button here
            }
            else
            {
                reticle.color = new Color(reticle.color.r, reticle.color.g, reticle.color.b, endAlpha);
            }
        }
        else
        {
            reticle.color = new Color(reticle.color.r, reticle.color.g, reticle.color.b, endAlpha);
            if (canInteract)
            {
                canInteract = false;
                imageFading = true;
                interactButtonOnImage.enabled = false; //use .enabled for images!!!
                interactButtonOffImage.enabled = true; //use .enabled for images!!!
                interactButtonOffImage.color = new Color(interactButtonOnImage.color.r, interactButtonOnImage.color.g, interactButtonOnImage.color.b, interactButtonStartAlpha / 255);
                currentAlpha = interactButtonStartAlpha;
                //InteractButtonFadeOut();
            }
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
