using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWeapons : MonoBehaviour
{

    [SerializeField] private GameObject triggerSound;

    // Función encargada de activar las armas del jugador al desplazarse por la zona 
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            col.GetComponent<Apuntado>().setHaveWeapon(true);
            col.GetComponent<Movimiento>().setHaveWeapon(true);
            col.GetComponent<PlayerStats>().setNeedUI(true);
            col.GetComponent<ChangeControl>().setCanSeeStats(true);

            GameObject insSound = Instantiate(triggerSound,transform.position,transform.rotation);
            insSound.GetComponent<AudioSource>().Play();
            Destroy(insSound,1f);
        } 
    }
}
