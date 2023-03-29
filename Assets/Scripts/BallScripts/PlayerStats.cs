using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private static float Force;
    private static float minForce = 0;
    private static float maxForce = 100;
    //private static float minForce = -100f;
    public bool isIncreasing;
    private float direction = 1f;
    public float forceToAdd;
    public GameObject ForceBar;

    
    

    public static PlayerStats playerStatsInstance;

    public Slider ForceSlider;

    public void Awake()
    {
        if (playerStatsInstance != null) return;
        playerStatsInstance = this;
    }
    void Start()
    {
        ForceSlider.minValue = 0;
        ForceSlider.maxValue = 100f;

    }

    void Update()
    {
        
        if (PlayerMovement.playerInstance.playerHasBall)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isIncreasing = true;
                PlayerMovement.playerInstance.startDirect = true;
                ForceBar.SetActive(true);
            }
            
            
            if (Input.GetMouseButtonUp(0))
            {
                isIncreasing = false;
                ForceBar.SetActive(false);
            }

            if (isIncreasing)
            {
                ForceSlider.value += Time.deltaTime * 50f * direction;
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
                

                forceToAdd =  ForceSlider.value;

            }

        }
        else
        {
            forceToAdd = 0f;
        }

    }
    
}


