using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VidaUI : MonoBehaviour
{

    // Variables necesarias
    [SerializeField] private Slider vidaSlide;
    
    // Función que aumenta el máximo de Vida
    public void setMaxVida(float value){
        vidaSlide.maxValue = value;
        vidaSlide.value = value;
    }

    // Función que cambia el valor actual de la vida
    public void setVida(float value){
        vidaSlide.value = value;
    }

}
