using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Animator timerAnim;
    [SerializeField] private Animator timerBetweenAnim;
    [SerializeField] private float velocidadAparicion;

    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI timerTextBetween;

    void Start(){
        timerAnim.SetBool("needToShow",false);
        timerBetweenAnim.SetBool("needToShow",false);
    }

    // Función que muestra el tiempo de ronda en pantalla
    public void timerUIMag(bool value){
        timerAnim.SetBool("needToShow",value);
    }

    // Función que muestra el tiempo entre ronda en pantalla
    public void timerBetweenUIMag(bool value){
        timerBetweenAnim.SetBool("needToShow",value);
    }

    // Función que actualiza el contenido de tiempo de ronda
    public void updateTimerUI(float tiempo){
        TimeSpan tiempoFormat = TimeSpan.FromSeconds(tiempo);
        timerText.text = tiempoFormat.ToString("hh':'mm':'ss'.'ff");
    }

    // Función que actualiza el contenido de tiempo entre ronda
    public void updateTimerBetweenUI(float tiempo){
        TimeSpan tiempoFormat = TimeSpan.FromSeconds(tiempo);
        timerTextBetween.text = tiempoFormat.ToString("ss'.'ff");
    }

}
