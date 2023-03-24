using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static float Force;
    private static float maxForce = 100;

    public Slider ForceSlider;
    
    void Start()
    {
        ForceSlider.minValue = 0;
        ForceSlider.maxValue = maxForce;
        Force = maxForce;
    }

    
    void Update()
    {
        ForceSlider.value = Force;
        Damage();
    }

    void Damage()
    {
        Force = PlayerMovement.playerInstance.durationTime * 20f;
    }
}
