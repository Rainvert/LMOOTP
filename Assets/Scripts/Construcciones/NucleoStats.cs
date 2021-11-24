using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NucleoStats: MonoBehaviour
{   
    // Variables necesarias
    [SerializeField] private float edifMaxVida;
    [SerializeField] private float edifVida;
    [SerializeField] private float edifDamage;
    [SerializeField] private float progressTerraformMax;
    [SerializeField] private float progressTerraform;
    [SerializeField] private float progressTime;

    [SerializeField] private GameObject soundActivate;
    [SerializeField] private GameObject soundHit;
    [SerializeField] private GameObject soundDeath;
    [SerializeField] private GameObject pulseLight;
    [SerializeField] private GameObject pulseLightSound;

    private bool destroyed;
    private bool activated;

    private bool isEnded;

    [SerializeField] private GameObject lifeSlide;
    [SerializeField] private GameObject progressSlide;

    void Start(){
        if(lifeSlide!=null){
            lifeSlide.GetComponent<VidaUI>().setMaxVida(edifMaxVida);
        }
        if(progressSlide!=null){
            progressSlide.GetComponent<ProgresoUI>().setMaxProgress(progressTerraformMax);
        }
        destroyed = false;
        activated = false;
        isEnded = false;

        if(soundActivate!=null){
            soundActivate.GetComponent<AudioSource>().volume = 0f;
            StartCoroutine(soundProgress());
        }

        StartCoroutine(progressActivation());
    }

    // Corrutina encargada de verificar si está activo o no el progreso para hacer fade in o fade out del audio
    IEnumerator soundProgress(){
        float pctje = 0f;
        float finalValue = 0.2f;
        bool volumenChanged = false;
        AudioSource activeSound = soundActivate.GetComponent<AudioSource>();
        while(true){
            if(activated && !volumenChanged){
                if(activeSound.volume < finalValue){
                    activeSound.volume = Mathf.Lerp(0f,finalValue,pctje);
                    pctje += 0.1f;
                } else{
                    activeSound.volume = finalValue;
                    volumenChanged = true;
                    pctje = 0f;
                }
            } else{
                if(!activated && volumenChanged){
                    if(activeSound.volume > 0f){
                        activeSound.volume = Mathf.Lerp(finalValue,0,pctje);
                        pctje += 0.05f;
                    } else{
                        activeSound.volume = 0f;
                        volumenChanged = false;
                        pctje = 0f;
                    }  
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Función encargada de retornar el valor que determina si el núcleo está o no finalizado
    public bool getIsEnded(){
        return isEnded;
    }

    // Función que permite modificar el tiempo al cual el terraformador se carga
    public void setProgressTime(float value){
        progressTime = value;
    }

    // Función que permite obtener el tiempo al cual el terraformador se carga
    public float getProgressTime(){
        return progressTime;
    }

    // Función encargada de setear el valor que determina si el núcleo está o no finalizado
    public void setIsEnded(bool value){
        isEnded = value;
    }

    // Función encargada de obtener la vida 
    public float getVida(){
        return edifVida;
    }

    // Función encargada de obtener la vida máxima 
    public float getMaxVida(){
        return edifMaxVida;
    }

    // Función encargada de dañar la vida del objetivo
    public void damageHealth(float value){
        edifVida -= value;
        lifeSlide.GetComponent<VidaUI>().setVida(edifVida);

        GameObject hitSound = Instantiate(soundHit,transform.position,transform.rotation);
        hitSound.GetComponent<AudioSource>().Play();
        Destroy(hitSound,1f);

        if(!isAlive()){
            Debug.Log("Murió el núcleo :(");
            destroyed = true;

            GameObject deathSound = Instantiate(soundDeath,transform.position,transform.rotation);
            deathSound.GetComponent<AudioSource>().Play();
            Destroy(deathSound,1f);
        } 
    }

    // Función que aumenta la vida actual del objetivo en un % X de la vida máxima
    public bool plusHealth(float value){
        bool aplicado = true;
        
        if(edifVida == edifMaxVida){
            aplicado = false;
        }

        edifVida += edifMaxVida*value;
        if(edifVida>edifMaxVida){
            edifVida = edifMaxVida;
        }
        lifeSlide.GetComponent<VidaUI>().setVida(edifVida);

        return aplicado;
    }

    // Función que verifica si el objetivo sigue vivo
    private bool isAlive(){
        if(edifVida>0){
            return true;
        } else{
            return false;
        }
    }

    // Función que permite obtener el estado actual de la edificación
    public bool getDestroyed(){
        return destroyed;
    }

    // Función que permite cambiar el estado actual de la edificación
    public void setDestroyed(bool value){
        destroyed = value;
    }

    // Función que permite obtener el estado actual de activación
    public bool getActivated(){
        return activated;
    }

    // Función que permite cambiar el estado actual de activación
    public void setActivated(bool value){
        activated = value;
    }

    // Corrutina encargada de aumentar el progreso de terraformación
    IEnumerator progressActivation(){
        while(true){
            if(activated){
                progressTerraform += 1;
                progressSlide.GetComponent<ProgresoUI>().setProgress(progressTerraform);
                yield return new WaitForSeconds(progressTime);
            } else{
                if(GetComponent<SpriteRenderer>().color != Color.white){
                    GetComponent<SpriteRenderer>().color = Color.white;
                    transform.parent.GetComponentInChildren<Light2D>().color = Color.white;
                }
            }
            yield return null;
        }
    }

    // Función que provoca que el núcleo genere un pulso
    public void pulseNucleo(){
        GameObject instaPulse = Instantiate(pulseLight,transform.position,transform.rotation);
        GameObject instaPulseSound = Instantiate(pulseLightSound,transform.position,transform.rotation);
        
        Camera.main.GetComponent<FollowCamZoom>().shakeCamCustomPower(1f,0.2f);
        
        Destroy(instaPulse,2f);
        Destroy(instaPulseSound,2f);
    }

    // Función encargada de retornar el progreso actual de la terraformación
    public float getProgress(){
        return progressTerraform;
    }

    // Función encargada de retornar el progreso máximo a alcanzar por la terraformación
    public float getMaxProgress(){
        return progressTerraformMax;
    }

    // Función que permite modificar el progreso actual de la terraformación
    public void setProgress(float value){
        progressTerraform = value;
        progressSlide.GetComponent<ProgresoUI>().setProgress(progressTerraform);
    }
}
