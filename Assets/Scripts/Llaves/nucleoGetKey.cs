using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nucleoGetKey : MonoBehaviour
{

    // Variables necesarias
    [SerializeField] private GameObject putKeySound;

    // Función encargada de revisar si la llave tiene contacto con un nucleo para habilitarlo
    void OnTriggerEnter2D(Collider2D col){
        if(col.GetComponent<ActivationArea>()!= null){
            col.GetComponent<ActivationArea>().setHaveKey(true);
            GameObject instKey = Instantiate(putKeySound,transform.position,transform.rotation);
            instKey.GetComponent<AudioSource>().Play();
            Destroy(instKey,1f);
            Destroy(this.gameObject);
        }
    }
}
