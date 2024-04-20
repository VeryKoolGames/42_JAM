using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpSlider : MonoBehaviour
{
    public FloatVariable powerUpTimeLeft;
    public Slider slider;
    
    void Update()
    {
        slider.value = powerUpTimeLeft.value;
    }
}
