using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;

public class PlayerInteract : MonoBehaviour
{
    public Camera mainCamera;   // Reference to the main camera for raycasting
    public float rayLength = 1f; // Length of the raycast

    public void AttemptInteraction()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayLength))
        {
            Debug.Log(hit.collider.name);
            Debug.DrawLine(ray.origin, hit.point, Color.green);
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();
            if (interactable != null)
            {
                interactable.OnRayCastHit();
            }
            else if (interactable == null)
            {
                Debug.Log("No Hit");
            }
        }
    }

}
