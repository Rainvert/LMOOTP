using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackSP : MonoBehaviour
{

    [SerializeField] private float damage;

    // Función que permite setear el daño del ataque
    public void setDamage(float value){
        damage = value;
    }

    // Función encargada de explotar el proyectil al impactar con un elemento interactivo enemigo/escenario
    void OnTriggerEnter2D(Collider2D col){
        // Si es un player o un edificio atacable
        if(col.gameObject.layer == 6 || col.gameObject.layer == 10){

            if(col.GetComponent<PlayerStats>()!=null){
                col.GetComponent<PlayerStats>().damageHealth(damage);
            }
            if(col.GetComponent<NucleoStats>()!=null){
                col.GetComponent<NucleoStats>().damageHealth(damage);
            }
            if(col.GetComponent<BuildStats>()!=null){
                col.GetComponent<BuildStats>().damageHealth(damage);
            }
        } else{
            if(col.gameObject.layer!= 8 && col.gameObject.layer!= 9){

                if(col.GetComponent<EnemyStats>()!=null){
                    col.GetComponent<EnemyStats>().decreaseHealth(damage,this.gameObject);
                }
            }
        }
    }
}
