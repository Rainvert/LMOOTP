using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMMusic : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private AudioSource musica;
    [SerializeField] private BGMType tipo;
    [SerializeField] private float volMin,volMax;


    // Función encargada de subir el volumen de manera progresiva
    public void subirVolumen(){
        StopAllCoroutines();
        StartCoroutine(changeVolumeBGM(musica.volume,volMax));
    }

    // Función encargada de bajar el volumen de manera progresiva
    public void bajarVolumen(){
        StopAllCoroutines();
        StartCoroutine(changeVolumeBGM(musica.volume,volMin));
    }

    // Función encargada de bajar el volumen de manera brusca
    public void bajarVolumenNow(){
        musica.volume = volMin;
    }

    // Función que permite obtener el tipo de música que se está manejando
    public BGMType getTypeBGM(){
        return tipo;
    }

    // Corrutina encargada de cambiar el volumen de manera progresiva
    IEnumerator changeVolumeBGM(float initValue, float endValue){
        float porcentaje = 0f;
        float error = 0.0001f;
        while(true){
            musica.volume = Mathf.Lerp(initValue,endValue,porcentaje);
            porcentaje += 0.01f;

            if(Mathf.Abs(musica.volume - endValue) <= error){
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
        musica.volume = endValue;
        yield break;
    }

    // Función que reproduce una vez el audio
    public void playOnce(){
        musica.Play();
    }

}

// Enum encargado de manejar los tipos de BGM
public enum BGMType {Batalla,Normal,Boss,Muerte}