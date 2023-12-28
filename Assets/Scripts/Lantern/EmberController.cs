using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmberController : MonoBehaviour
{
    // Start is called before the first frame update
    public float adjustmentAmount;
    public float adjustmentMaximum;

    private float rangeMinimum;
    private float rangeMaximum;

    private float maxRangeMinimum;
    private float maxRangeMaximum;
    private ParticleSystem lanternFlame;


    void Start()
    {
        lanternFlame = GetComponent<ParticleSystem>();
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
            // Access the main module
            var mainModule = lanternFlame.main;

            // Create a new MinMaxCurve with the adjusted size
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin + adjustmentAmount, mainModule.startSize.constantMax + adjustmentAmount);

        }
        else if (Input.GetKey(KeyCode.Q))
        {
            // Similar structure as above for decreasing the size
            var mainModule = lanternFlame.main;
            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin - adjustmentAmount, mainModule.startSize.constantMax - adjustmentAmount);

        }
    }

}
