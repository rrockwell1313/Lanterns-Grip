using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour, IInteractable
{

    bool isHit;
    public void OnRayCastHit()
    {
        isHit = true;
    }
    public void Interact()
    {
        Debug.Log("Torch Interacted");
    }

    void Update()
    {
        if (isHit)
        {
            Interact();
            isHit = false;
        }
    }
}
