using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private ParticleSystem particulas;

    [SerializeField] private AudioSource audioLluvia;

    [SerializeField] private Camera camFollow;

    [SerializeField] private Vector3 offset;

    private float velocidad = 0.01f;

    void Start(){
        audioLluvia.volume = 0;
        particulas.Stop();
    }

    void Update(){
        transform.position = camFollow.transform.position + offset;
    }

    // Función que actualiza los valores de la lluvia
    public void updateRainParams(float emissionCtda, float initVelocidad, float simVeloc, float volLluvia){
        
        ParticleSystem.EmissionModule emitModule = particulas.emission;
        ParticleSystem.MinMaxCurve ctdaEnTiempo = emitModule.rateOverTime;
        
        ctdaEnTiempo.constant = emissionCtda;
        emitModule.rateOverTime = ctdaEnTiempo;

        ParticleSystem.MainModule mainConfig = particulas.main;
        ParticleSystem.MinMaxCurve velocidad = mainConfig.startSpeed;

        mainConfig.simulationSpeed = simVeloc;

        velocidad.constant = initVelocidad;
        mainConfig.startSpeed = velocidad;

        StopAllCoroutines();
        StartCoroutine(changeVolumenAudio(volLluvia));
    }

    // Corrutina encargada de cambiar el volumen del sonido de la lluvia
    IEnumerator changeVolumenAudio(float value){
        float contador = 0f;
        float timeLimit = 1f;
        float porcentaje = 0f;
        float initValue = audioLluvia.volume;
        while(contador<timeLimit){
            audioLluvia.volume = Mathf.Lerp(initValue,value,porcentaje);
            contador = contador + Time.deltaTime;
            porcentaje += velocidad;
            yield return new WaitForSeconds(0.05f);
        }
        audioLluvia.volume = value;
    }

    // Función que activa o desactiva el sistema de lluvia a petición
    public void setRainSystem(bool value){
        particulas.Stop();
        if(value){
            particulas.Play();
        }else{
            StopAllCoroutines();
            audioLluvia.volume = 0f;
        }

    }
}
