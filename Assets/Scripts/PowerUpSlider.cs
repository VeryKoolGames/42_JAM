using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpSlider : MonoBehaviour
{
    public FloatVariable powerUpTimeLeft;
    public FloatVariable powerUpDuration;
    public Slider slider;
    public Image image;
    public Sprite newImage;
    public TMP_Text text;
    public string newText;

    public Animator animator;

    

    void Start(){
        slider.maxValue = powerUpDuration.value;
        text.text = newText;
        image.sprite = newImage;

    }
    
    void Update()
    {
        slider.value = powerUpTimeLeft.value;

        if(slider.value <= 0){
            animator.SetTrigger("leave");
        }
    }
}
