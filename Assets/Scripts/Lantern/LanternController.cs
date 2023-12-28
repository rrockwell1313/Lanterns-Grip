using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanternController : MonoBehaviour
{
    public float adjustmentAmount;
    public float bloomAdjustment;
    public float adjustmentMaximum;
    
    private float rangeMinimum;
    private float rangeMaximum;
    private float maxRangeMinimum;
    private float maxRangeMaximum;
    private AreaLightRangeSmoothing areaLight;

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
        AdjustLantern();
    }

    public void AdjustLantern()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //Check if they are within the maxRange before applying more.
            if (areaLight.maxRange - adjustmentAmount <= maxRangeMaximum)
            {
                areaLight.maxRange += adjustmentAmount;
                areaLight.minRange += adjustmentAmount;
            }else
            {
                Debug.Log("Lantern Max Range");
                return;
            }

        }else if (Input.GetKey(KeyCode.Q))
        {
            if (areaLight.maxRange - adjustmentAmount >= rangeMaximum)
            {
                areaLight.maxRange -= adjustmentAmount;
                areaLight.minRange -= adjustmentAmount;
            }
            else
            {
                Debug.Log("Lantern Minimum Range");
                return;
            }
        }
    }
}
