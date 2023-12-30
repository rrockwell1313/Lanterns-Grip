using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveLanternTestScript : MonoBehaviour
{
    public GameObject level1, level2, level3, light1;
    public InteractOnButtonPress interactOnButtonPress;
    private bool interacting;
    private bool lightOn;

    // Update is called once per frame
    void Update()
    {
        if (interactOnButtonPress.playerInteracting &! interacting)
        {     
            Interact();
        }

    }

    void Interact()
    {
        interacting = true;
        if (!lightOn)
        {
            Debug.Log("Lantern Lit");
            lightOn = true;
            level1.SetActive(true);
            level2.SetActive(true);
            level3.SetActive(true);
            light1.SetActive(true);
        }
        else if (lightOn)
        {
            Debug.Log("Lantern Off");
            lightOn = false;
            level1.SetActive(false);
            level2.SetActive(false);
            level3.SetActive(false);
            light1.SetActive(false);
        }
        gameObject.tag = "Interactable";
    }
}
