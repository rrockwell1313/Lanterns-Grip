using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class areaLightRangeRNG : MonoBehaviour
{
    public Light areaLight;
    public float range;
    public float startingRange;
    float oldRange;
    public float frequency; //number of frames between updates
    float frequencyReset;
    public float time;      //time between adjustments
    public float flicker;

    // Start is called before the first frame update
    void Start()
    {
        range = areaLight.range;
        startingRange = range;
        frequencyReset = frequency;
        SetNewLightRange();
    }

    // Update is called once per frame
    void Update()
    {
        if (frequency < frequencyReset) { frequency++; }
        if (frequency >= frequencyReset) { SetNewLightRange();}
    }

    void SetNewLightRange()
    {
        oldRange = range;
        range = Random.Range(startingRange - flicker, startingRange + flicker);
        areaLight.range = range;
        frequency = 0;
    }
}
