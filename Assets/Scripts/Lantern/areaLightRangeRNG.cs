using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting; 
using UnityEditor;           
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing; // Post-processing stack reference.

// Class to create a flickering light effect, resembling a flame or unstable light source.
#region NOTES ON FLICKER
/*
 * One of the issues we may run into with this script, is how post processing effects come from other light sources. For example
 * we may have another torch, with its own flicker settings. Perhaps we remove the bloom flicker and post processing effect if there
 * is any kind of issues with this, or find a way to create a list of light objects within range that may effect the bloom sensitivity as this is
 * a camera effect, and not really a on scene effect like the lighting itself is. Something worth testing to see if this is the case.
 */
#endregion
public class AreaLightRangeRNG : MonoBehaviour
{
    public Light areaLight;    // Reference to the Light component intended to flicker.
    public float range;        // Current range of the area light.
    public float startingRange; // Starting range of the area light, to reset or use as a base for calculations.
    float oldRange;            // Previous range value for comparison or other logic (unused in current script for some reason? Need to debug.).
    public float frequency;    // Number of frames between light range updates.
    float frequencyReset;      // The initial value of frequency to reset after each update.
    public float time;         // Time between adjustments (unused in current script. I dont forsee this being an issue to use.).
    public float flicker;      // Amount of range variation to simulate flickering.
    public Camera mainCamera;  // Reference to the main camera (unused in current script. Will not need as we have access to the volume).
    public PostProcessVolume volume; // Reference to the post-process volume for potential effects 
    private Bloom bloom;               // Reference to a Bloom effect if needed 
    public float bloomMinimum;

    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.HasSettings<Bloom>())
        {
            Debug.Log("bloom exists");
            bloom = volume.profile.GetSetting<Bloom>();
        }
        startingRange = range;
        frequencyReset = frequency;
        SetNewLightRange();//immediatley set new range on start.
    }

    void Update()
    {
        if (frequency < frequencyReset) { frequency++; }
        // If frequency counter has reached the threshold, set a new light range.
        if (frequency >= frequencyReset) { SetNewLightRange(); }
    }

    // Sets the light's range to a new random value within the flicker boundaries and resets the frequency counter.
    void SetNewLightRange()
    {
        oldRange = range;//referenced in variables, but not effecting. Weird.
        float newBloomIntensity = Random.Range((startingRange + bloomMinimum) - flicker, startingRange + flicker);
        if (bloom != null)
        {
            bloom.intensity.value = newBloomIntensity;
        }        
        range = Random.Range(startingRange - flicker, startingRange + flicker);
        areaLight.range = range;
        frequency = 0;
    }
}



