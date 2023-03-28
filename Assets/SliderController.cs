using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float minValue = -100f;
    public float maxValue = 100f;
    public float increment = 6f;


    private bool isDragging = false;

    private float range;
    private float turnValue;

    void Start()
    {
        slider.value = 0f;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            float currentValue = slider.value;
            currentValue += Input.GetAxis("Mouse X") * increment;
            currentValue = Mathf.Clamp(currentValue, minValue, maxValue);
            slider.value = currentValue;
        }
    }
}