using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmberController : MonoBehaviour
{
    // Start is called before the first frame update
    public float adjustmentAmount;
    public float adjustmentMaximum;

    //added
    public float maxParticleSize, minParticleSize, flameSizePercentage;
    private float currentParticleSize;
    bool increaseLanternPressed, decreaseLanternPressed;

    private float rangeMinimum;
    private float rangeMaximum;

    private float maxRangeMinimum;
    private float maxRangeMaximum;
    public ParticleSystem lanternFlame;


    void Start()
    {
        //lanternFlame = GetComponent<ParticleSystem>();
        currentParticleSize = lanternFlame.main.startSize.constantMin;
        AdjustLantern();


    }

    // Update is called once per frame
    void Update()
    {
        if (increaseLanternPressed || decreaseLanternPressed)
        {
            AdjustLantern();
        }
    }
    public void OnIncreaseLantern(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            increaseLanternPressed = true;
        }
        else if (context.canceled)
        {
            increaseLanternPressed = false;
        }
    }
    public void OnDecreaseLantern(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            decreaseLanternPressed = true;
        }
        else if (context.canceled)
        {
            decreaseLanternPressed = false;
        }
    }

    public void AdjustLantern()
    {
        // Access the main module
        var mainModule = lanternFlame.main;
        if (increaseLanternPressed)
        {
            if (currentParticleSize < maxParticleSize)
            {
                mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin + adjustmentAmount, mainModule.startSize.constantMax + adjustmentAmount);
                currentParticleSize += adjustmentAmount;
            }
        }
        else if (decreaseLanternPressed)
        {
            if (currentParticleSize > minParticleSize)
            {
                mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin - adjustmentAmount, mainModule.startSize.constantMax - adjustmentAmount);
                currentParticleSize -= adjustmentAmount;
            }
        }

        flameSizePercentage = currentParticleSize / maxParticleSize - minParticleSize;
    }

    //public void AdjustLantern()
    //{
    //    if (Input.GetKey(KeyCode.E))
    //    {
    //        // Access the main module
    //        var mainModule = lanternFlame.main;

    //        // Create a new MinMaxCurve with the adjusted size
    //        mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin + adjustmentAmount, mainModule.startSize.constantMax + adjustmentAmount);

    //    }
    //    else if (Input.GetKey(KeyCode.Q))
    //    {
            
    //        // Similar structure as above for decreasing the size
    //        var mainModule = lanternFlame.main;

    //        mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin - adjustmentAmount, mainModule.startSize.constantMax - adjustmentAmount);

    //    }
    //}

}
