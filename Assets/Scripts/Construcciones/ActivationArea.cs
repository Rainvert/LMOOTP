using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationArea : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private NucleoStats nucleo;
    private bool haveKey = false;

    // Función que permite cambiar el valor de haveKey
    public void setHaveKey(bool value){
        haveKey = value;
    }

    // Función que termina el núcleo y la partida
    public void endKeyActivated(){
        nucleo.setIsEnded(true);
    }

   // Función encargada de revisar cuando un player entra a un trigger y activar la posibilidad de activar el núcleo
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player" && haveKey){
            col.GetComponent<ChangeControl>().setCanActivate(true,nucleo);
        }
    }

    // Función encargada de revisar cuando un player sale de un trigger y desactivar la posibilidad de activar el núcleo
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            col.GetComponent<ChangeControl>().setCanActivate(false,nucleo);
        }
    }
}
