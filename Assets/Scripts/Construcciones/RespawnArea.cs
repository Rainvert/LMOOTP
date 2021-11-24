using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnArea : MonoBehaviour
{
    [SerializeField] private PlayerManager playerMang;

    [SerializeField] private bool canRespawn;

    void Start(){
        canRespawn = true;
    }

    // Función que permite cambiar el valor de respawn para permitir o bloquear la capacidad de hacerlo
    public void setCanRespawn(bool value){
        canRespawn = value;
    }

    // Función encargada de revisar cuando un player entra a un trigger y activar el respawnear de jugadores
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player" && canRespawn){
            Debug.Log("Tratando de revivir");
            bool allAlive = playerMang.restartDeathPlayers(transform.position);
            canRespawn = allAlive;
        }
    }

}
