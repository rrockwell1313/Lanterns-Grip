using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmberController : MonoBehaviour
{
    //// Start is called before the first frame update
    //public float adjustmentAmount;
    //public float adjustmentMaximum;

    ////added
    //public float maxParticleSize, minParticleSize, flameSizePercentage;
    //private float currentParticleSize;
    //bool increaseLantern, decreaseLantern;

    //private float rangeMinimum;
    //private float rangeMaximum;

    //private float maxRangeMinimum;
    //private float maxRangeMaximum;
    //public ParticleSystem lanternFlame;


    //void Start()
    //{
    //    //lanternFlame = GetComponent<ParticleSystem>();
    //    currentParticleSize = lanternFlame.main.startSize.constantMin;
    //    AdjustLantern();


    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    if (increaseLanternPressed || decreaseLanternPressed)
    //    {
    //        AdjustLantern();
    //    }
    //}

    //public void AdjustLantern()
    //{
    //    // Access the main module
    //    var mainModule = lanternFlame.main;
    //    if (increaseLanternPressed)
    //    {
    //        if (currentParticleSize < maxParticleSize)
    //        {
    //            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin + adjustmentAmount, mainModule.startSize.constantMax + adjustmentAmount);
    //            currentParticleSize += adjustmentAmount;
    //        }
    //    }
    //    else if (decreaseLanternPressed)
    //    {
    //        if (currentParticleSize > minParticleSize)
    //        {
    //            mainModule.startSize = new ParticleSystem.MinMaxCurve(mainModule.startSize.constantMin - adjustmentAmount, mainModule.startSize.constantMax - adjustmentAmount);
    //            currentParticleSize -= adjustmentAmount;
    //        }
    //    }

    //    flameSizePercentage = currentParticleSize / maxParticleSize - minParticleSize;
    //}

}
