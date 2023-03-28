using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static float Force;
    private static float maxForce = 100;
    private static float minForce = -100f;
    private bool isIncreasing;
    private float direction = 1f;

    public Slider ForceSlider;
    
    void Start()
    {
        ForceSlider.minValue = -100;
        ForceSlider.maxValue = 100f;
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isIncreasing = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isIncreasing = false;
            
        }

        if (isIncreasing)
        {
            ForceSlider.value += Time.deltaTime * 100f * direction;
            if (ForceSlider.value >= maxForce)
            {
                ForceSlider.value = maxForce;
                direction *= -1f;
            }
            else if (ForceSlider.value <= minForce)
            {
                ForceSlider.value = minForce;
                direction *= -1f;
            }
        }
    }

    
    /*
    void IncreaseForce()
    {
        if (Input.GetMouseButton(0))
        {
            Force += 3f; 
            ForceSlider.value = Force;
            
            if (ForceSlider.value == maxForce)
            {
                Force -= 3f;
                
            }
            
            else if (ForceSlider.value == minForce)
            {
                Force += 3f;
            }
        }
        
    }
    */
    
}


