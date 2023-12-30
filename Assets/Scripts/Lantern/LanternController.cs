using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanternController : MonoBehaviour
{
    public float adjustmentAmount;
    public float bloomAdjustment;
    public float adjustmentMaximum;
    public float lanternSizePercentage;
    public EmberController emberController;

    private float rangeMinimum;
    private float rangeMaximum;
    private float maxRangeMinimum;
    private float maxRangeMaximum;
    private AreaLightRangeSmoothing areaLight;


    bool increaseLanternPressed, decreaseLanternPressed;

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
    // Start is called before the first frame update
    void Start()
    {   

        if (areaLight == null)
        {
            areaLight = GetComponent<AreaLightRangeSmoothing>();
            //Set the current range to return too as the minimum.
            rangeMaximum = areaLight.maxRange;
            rangeMinimum = areaLight.minRange;

            maxRangeMaximum = areaLight.maxRange + adjustmentMaximum;
            maxRangeMinimum = areaLight.minRange + adjustmentMaximum;
            Debug.Log("areaLight is null");
        }
        else if (areaLight != null)
        {
            Debug.Log("areaLight is not null");
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (increaseLanternPressed || decreaseLanternPressed)
        {
            AdjustLantern();
        }
    }

    public void AdjustLantern()
    {
        float flameSizePercentage = emberController.flameSizePercentage;
        lanternSizePercentage = flameSizePercentage;
        areaLight.maxRange = adjustmentMaximum * flameSizePercentage;
        areaLight.minRange = (adjustmentMaximum * flameSizePercentage) - 1;
        Debug.Log("areaLight.maxRange: " + areaLight.maxRange);
    }
    //public void AdjustLantern()
    //{
    //    if (increaseLanternPressed)
    //    {
    //        //Check if they are within the maxRange before applying more.
    //        if (areaLight.maxRange - adjustmentAmount <= maxRangeMaximum)
    //        {
    //            areaLight.maxRange += adjustmentAmount;
    //            areaLight.minRange += adjustmentAmount;
    //        }else
    //        {
    //            Debug.Log("Lantern Max Range");
    //            return;
    //        }

    //    }else if (decreaseLanternPressed)
    //    {
    //        if (areaLight.maxRange - adjustmentAmount >= rangeMaximum)
    //        {
    //            areaLight.maxRange -= adjustmentAmount;
    //            areaLight.minRange -= adjustmentAmount;
    //        }
    //        else
    //        {
    //            Debug.Log("Lantern Minimum Range");
    //            return;
    //        }
    //    }
    //}
}
