using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowBar : MonoBehaviour
{
    public Slider slider;

    public void SetThrowForce(float throwForce)
    {
        slider.value = throwForce;
    }

    public void SetMaxForce(float throwForce)
    {
        slider.maxValue = throwForce;
        slider.value = throwForce;

    }
}
