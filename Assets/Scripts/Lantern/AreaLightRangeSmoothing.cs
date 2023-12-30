using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing; // Post-processing stack reference.

// Class to create a flickering light effect, resembling a flame or unstable light source.
#region NOTES ON FLICKER
/*
 * This version of the flicker uses perlin noise, and math to scale UP and DOWN in increments over time following a curve to simulate the flame flicker.
 * We can adjust this pretty heavily to get it right based on the lantern, and we can even create profiles for different types of lanterns for more stability
 * or maybe this one has glass panes protecting the flame so its less. Or maybe we increase the flickering based on movement to simulate wind hitting it.
 * Or maybe a gust of wind comes to blow the wick out etc.
 */
#endregion
public class AreaLightRangeSmoothing : MonoBehaviour
{
    public Light areaLight;
    public AnimationCurve flickerCurve; // A curve to control the flicker intensity
    public float minRange;
    public float maxRange;
    public float flickerDuration; // Duration of one flicker to reach the target intensity
    public float flickerIntensity; // The maximum deviation added to the light's range due to flicker
    public PostProcessVolume volume; // Reference to the post-process volume for potential effects 
    private Bloom bloom;               // Reference to a Bloom effect if needed 
    public float bloomOffset;


    private float targetRange;//target range may not be relevant
    private Coroutine flickerCoroutine;//error listed is false, its used in the start.

    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.HasSettings<Bloom>())
        {
            //Debug.Log("bloom exists");
            bloom = volume.profile.GetSetting<Bloom>();
        }
        // Set initial range
        areaLight.range = minRange;
        // Start the flickering effect
        flickerCoroutine = StartCoroutine(FlickerLight());
    }

    IEnumerator FlickerLight()
    {
        // The time at which the flicker started.
        float startTime = Time.time;

        while (true)
        {
            // Calculate the time elapsed since the flicker started.
            float timeElapsed = Time.time - startTime;

            // Use a sine wave to oscillate the range value smoothly over time.
            // Mathf.PingPong will smoothly transition the value between the minRange and maxRange.
            float pingPong = Mathf.PingPong(timeElapsed * flickerDuration, maxRange - minRange);

            // Apply a random factor to the ping pong value for variability, simulating a flame.
            // Perlin noise can be used here for smoother random transitions.
            float noise = Mathf.PerlinNoise(timeElapsed, 0f) * flickerIntensity;

            // Set the new light range, combining the ping pong and noise values.
            // Adjust the minRange accordingly to ensure it stays within the desired limits.
            areaLight.range = minRange + pingPong + noise;
            if (bloom)
            {
                bloom.intensity.value = (minRange + bloomOffset) + pingPong + noise;
            }



            // Wait until the next frame to update again.
            yield return null;
        }
    }

}
