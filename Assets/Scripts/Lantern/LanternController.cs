using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanternController : MonoBehaviour
{
    [Tooltip("Amount of time in seconds between updates of the brightness amount.")]
    public float adjustmentInterval;
    public float maxFlameParticleSize, minFlameParticleSize;
    public float flameParticleSizeDifferential;
    public float maxLightRadius, minLightRadius, currentLightRadius, lightDelay;
    [HideInInspector] public float  brightness; //percentage
    [HideInInspector] public bool increaseLantern, decreaseLantern;
    private Coroutine brightnessCoroutine;
    private bool isCoroutineRunning;

    private ParticleSystem particleSystem;
    private Light areaLight;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null) //using this can allow us to attach things to the objects that use them, eventually, though, if you have more than one of something, it could be an issue
        {
            particleSystem = GetComponentInChildren<ParticleSystem>();
        }
        areaLight = GetComponent<Light>();
        if (areaLight == null)
        {
            areaLight = GetComponentInChildren<Light>();
        }
        brightness = 50f;
        AdjustParticleSize();
        AdjustLightRadius();
    }

    private void Update()
    {
        BrightnessCoroutine();

    }

    void BrightnessCoroutine()
    {
        if (!isCoroutineRunning)
        {
            if (increaseLantern || decreaseLantern)
            {
                isCoroutineRunning = true;
                brightnessCoroutine = StartCoroutine(AdjustBrightnessCoroutine());
            }
        }
        else if (isCoroutineRunning)
        {
            if (!increaseLantern && !decreaseLantern)
            {
                isCoroutineRunning = false;
                StopCoroutine(brightnessCoroutine);
            }
        }
    }

    IEnumerator AdjustBrightnessCoroutine()
    {
        while (true) // Infinite loop
        {
            if (increaseLantern && brightness < 100)
            {
                brightness++;
            }
            else if (decreaseLantern && brightness > 0)
            {
                brightness--;
            }

            // Update particle size and light radius here

            AdjustParticleSize();
            AdjustLightRadius();
            yield return new WaitForSeconds(adjustmentInterval); // Wait for specified interval

        }
    }

    public void AdjustParticleSize()
    {
        //grow the particle system size = size based on brightness
        float percentage = brightness / 100;                //return the brightness as a percentage
        float newSize = maxFlameParticleSize * percentage;  //new size is set based on a percent of the max size of particle entered in the inspector
        var mainModule = particleSystem.main;               //access the main module

        if (percentage == 0f)                               //if the percentage is 0, the light is off, so turn off the particle effect
        {
            particleSystem.Stop(true);
        }
        else if (percentage > 0f)
        {
            if (particleSystem.isStopped)                   //if the effect is off, turn it on, since the light is back on
            {
                particleSystem.Play(true);
            }
            mainModule.startSize = new ParticleSystem.MinMaxCurve(newSize - flameParticleSizeDifferential, newSize + flameParticleSizeDifferential); //using the differential to add visual flair
        }
    }
    public void AdjustLightRadius()
    {
        //grow the light radius size = size based on brightness
        float percentage = brightness / 100;                                //return the brightness as a percentage
        float newSize = ((maxLightRadius-minLightRadius) * percentage);     //new size is set based on a percent of the max radius - min radius entered in the inspector
        currentLightRadius = newSize + minLightRadius; //using lerp to keep the light behind the particles visually

        if (percentage == 0f)                                               //if the percentage is 0, the light is off, so turn off the particle effect
        {
            areaLight.range = minLightRadius;
        }
        else if (percentage > 0f)
        {
            areaLight.range = currentLightRadius;
        }
    }
}
