using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgresoUI : MonoBehaviour
{

    // Variables necesarias
    [SerializeField] private Slider progressSlide;
    [SerializeField] private Text progressText;
    
    // Función que aumenta el máximo de progreso
    public void setMaxProgress(float value){
        progressSlide.maxValue = value;
        progressSlide.value = 0;
        progressText.text = progressSlide.value+"/"+progressSlide.maxValue;
    }

    // Función que cambia el valor actual del progreso
    public void setProgress(float value){
        progressSlide.value = value;
        progressText.text = progressSlide.value+"/"+progressSlide.maxValue;
    }



}
