using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractOnButtonPress : MonoBehaviour
{
    bool interactPressed;       // Indicates if the interaction button is currently pressed
    bool canInteract;           // Determines if the player can interact with something
    public bool playerInteracting; // Public flag to show if the player is currently interacting

    GameObject interactable;    // Stores the interactable object
    public Camera mainCamera;   // Reference to the main camera for raycasting
    public float rayLength = 100f; // Length of the raycast
    public string targetTag = "Interactable"; // Tag that identifies interactable objects, may be easier to see if we can check a component for a specific script with an interactable bool


    public Image reticle;       // Reticle image for the UI
    public float reticleStartAlpha; // Start alpha value for the reticle
    public float reticleEndAlpha;   // End alpha value for the reticle
    public Image interactButtonOnImage; // Image shown when interaction is possible
    public Image interactButtonOffImage; // Image shown when interaction is not possible
    public float interactButtonStartAlpha; // Start alpha for the interact button image
    public float interactButtonEndAlpha;   // End alpha for the interact button image
    public float interactImageFadeInTime;  // Time it takes for the interact image to fade in
    public float interactImageOnOffTime;   // Time for toggling the interact on/off image
    float onOffTime;              // Timer for interact image on/off toggle
    float currentAlpha;           // Current alpha value for the fading effect
    bool imageFading;             // Flag to determine if the image is currently fading

    
    private void Update()
        //consolidate update function to simply be a list of methods. Keep ifs in their methods, update being cluttered is bad practice and should only
        //be a place where you check the hierachy of processing each method where possible.
    {
        ReticleRayCast();
        ManageInteraction();//Moved if statements to this method.

    }

    private void ManageInteraction()
    {
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
        else if (imageFading & !canInteract)
        {
            InteractButtonFadeOut();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    // Called when the interact button is pressed
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

    //private void OnTriggerEnter(Collider other)
    //// Called when the player collides with an interactable object. Checking if object is interactable. More definition later?
    ////Tags may become more defined in future, instead object pooling for script components possibly. Not sure yet.
    //{
    //    if (other.gameObject.CompareTag("Interactable"))
    //    {
    //        interactable = other.gameObject;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //// Called when the player exits the collision with an interactable object
    ////Required to note the return of the previous reticle. May not be needed, on enter only happens when inters, can just say else
    //{
    //    if (other.gameObject.CompareTag("Interactable"))
    //    {
    //        interactable = null;
    //    }
    //}
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
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)); //create a Ray named ray and cast it forward from the center of the camera
        float startAlpha = reticleStartAlpha / 255f; //initial alpha of the reticle
        float endAlpha = reticleEndAlpha / 255f; //end alpha of the reticle
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            //Debug.DrawLine(ray.origin, hit.point, Color.green, 5f);
            //Debug.Log("Hit: " + hit.collider.name);


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
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.red);
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
