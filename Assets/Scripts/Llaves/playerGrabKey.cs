using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class playerGrabKey : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private GameObject pickUpSound;
    private bool wasPicked = false;
    private Color oldColor;
    private Transform playerWithKey;

    // Función encargada de revisar si un player tuvo contacto con la llave para asignarla a este
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player" && !wasPicked){

            wasPicked = true;
            playerWithKey = col.transform;

            oldColor = GetComponentInChildren<Light2D>().color;
            GetComponent<SpriteRenderer>().color = new Color(oldColor.r,oldColor.g,oldColor.b,0f);

            GameObject instSound = Instantiate(pickUpSound,transform.position,transform.rotation);
            instSound.GetComponent<AudioSource>().Play();
            Destroy(instSound,1f);

            col.GetComponent<ChangeControl>().setActNucleoColor(oldColor);

        }
    }

    void Update(){
        if(playerWithKey!=null){
            transform.position = playerWithKey.transform.position;
        }
    }
}
