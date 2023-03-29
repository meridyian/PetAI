using System.Collections;
using System.Collections.Generic;
using Cinemachine.Editor;
using Unity.VisualScripting.FullSerializer;
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
    public float durationTime;
    public Transform trackedPosition;
    public Vector3 forwardForce;
    public float forceY;
    public float forceZ;


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
                forwardForce = trackedPosition.forward;
                isIncreasing = false;
                ForceBar.SetActive(false);
            }

            if (isIncreasing)
            {
                durationTime = Time.deltaTime * 10f;
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
                

                forceToAdd = ForceSlider.value;
                Debug.Log(forceToAdd);
            }

        }

    }
    
}


