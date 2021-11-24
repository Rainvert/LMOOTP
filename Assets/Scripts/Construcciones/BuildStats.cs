using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildStats: MonoBehaviour {

    // Variables necesarias
    [SerializeField] private string nameEdif;
    [SerializeField] private float maxVida;
    [SerializeField] private float vida;
    [SerializeField] private GameObject deathInstance;
    [SerializeField] private GameObject soundHit;
    [SerializeField] private GameObject soundDeath;

    // Funcion get y set para:
    // Nombre
    public void setNameEdif(string value){
        nameEdif = value;
    }

    public string getNameEdif(){
        return nameEdif;
    }

    // MaxVida
    public void setMaxVida(float value){
        maxVida = value;
    }

    public float getMaxVida(){
        return maxVida;
    }

    // Vida 
    public void setVida(float value){
        vida = value;
    }

    public float getVida(){
        return vida;
    }

    // Función encargada de dañar la vida del objetivo
    public void damageHealth(float value){
        vida -= value;

        GameObject hitSound = Instantiate(soundHit,transform.position,transform.rotation);
        hitSound.GetComponent<AudioSource>().Play();
        Destroy(hitSound,1f);

        if(!isAlive()){
            Debug.Log("Murió "+nameEdif);
            showDeathInstance();
            
        } 
    }

    // Función que verifica si el objetivo sigue vivo
    private bool isAlive(){
        if(vida>0){
            return true;
        } else{
            return false;
        }
    }

    // Función encargada de destruir el objeto actual y mostrar la instancia de muerte
    private void showDeathInstance(){

        GameObject deathSound = Instantiate(soundDeath,transform.position,transform.rotation);
        deathSound.GetComponent<AudioSource>().Play();
        Destroy(deathSound,1f);

        GameObject deathIns = Instantiate(deathInstance,transform.position,transform.rotation);
        Destroy(deathIns,10f);
        Destroy(transform.parent.gameObject);
    }

    // Función virtual encargada de implementarse para cada edificación con su propia instancia de muerte
    protected virtual void inDeathState(){
        Debug.Log("Construcción "+nameEdif+" Destruida");
    }

}
