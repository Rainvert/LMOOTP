using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SunLightControl : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    // Variables necesarias
    private bool notPlayers = false;

    private float finalValue = 0.2f;
    private float velocidad = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(checkPlayersPresence());
        GetComponent<Light2D>().intensity = 0f;
    }

    // Corrutina encargada de revisar la presencia de players 
    IEnumerator checkPlayersPresence(){
        while(!notPlayers){
            if(playerManager.getPlayers().Count != 0){
                notPlayers = true;
            }
            yield return null;
        }
        StartCoroutine(changeIlumination(finalValue));
    }

    // Corrutina encargada de cambiar la iluminación progresivamente
    IEnumerator changeIlumination(float value){
        float contador = 0f;
        float timeLimit = 1f;
        float porcentaje = 0f;
        float initValue = GetComponent<Light2D>().intensity;
        while(contador<timeLimit){
            GetComponent<Light2D>().intensity = Mathf.Lerp(initValue,value,porcentaje);
            contador = contador + Time.deltaTime;
            porcentaje += velocidad;
            yield return new WaitForSeconds(0.01f);
        }
        GetComponent<Light2D>().intensity = value;
    }

    // Función que permite cambiar la iluminación a petición 
    public void changeCustomIlumination(float value){
        StopAllCoroutines();
        StartCoroutine(changeIlumination(value));
    }
}
