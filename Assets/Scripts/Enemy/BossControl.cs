using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private bool isAlive;
    [SerializeField] private GameObject blockArea;
    [SerializeField] private GameObject blockAreaText;
    [SerializeField] private GameObject blockBossStage;
    [SerializeField] private GameObject boss;
    [SerializeField] private EnemyStats bossStats;
    [SerializeField] private GameObject keyOfEnd;
    [SerializeField] private float ctdaJugadores;
    private int jugadorInArea;
    private bool activated;
    private PlayerManager mangPlayers;

    private Vector2 posKey;

    void Start(){

        activated = false;

        if(boss!=null){
            isAlive = true;
        }

        StartCoroutine(checkBossAlive());
        blockBossStage.SetActive(false);
        mangPlayers = FindObjectOfType<PlayerManager>();
        posKey = boss.transform.position;
        boss.GetComponent<BoxCollider2D>().enabled =false;
    }

    // Corrutina encargada de revisar si el boss sigue con vida 
    IEnumerator checkBossAlive(){
        while(true){
            if(!isAlive){
                deathOfBoss();
                blockBossStage.SetActive(false);
                break;
            }
            if(boss == null){
                isAlive = false;
            }
            yield return null;
        }

        yield break;
    }

    // Función que elimina el bloqueo para permitir acceder al escenario del Boss
    public void deleteBlockArea(){
        blockArea.SetActive(false);
        if(blockAreaText!=null){
            blockAreaText.SetActive(false);
        }
    }

    // Función que activa las acciones si es que el jefe muere
    public void deathOfBoss(){
        GameObject instance = Instantiate(keyOfEnd,posKey,transform.rotation);
    }

    // Función encargada de revisar cuando un player entra al stage del boss y activar la barrera invisible
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player" && !activated){
            jugadorInArea++;
            if(mangPlayers.getPlayers().Count == jugadorInArea){
                activated = true;
                blockBossStage.SetActive(true);
                float vidaMaxima = boss.GetComponent<EnemyStats>().getMaxHealth();
                boss.GetComponent<EnemyStats>().setMaxHealth(vidaMaxima*jugadorInArea);
                boss.GetComponent<EnemyStats>().setHealth(vidaMaxima*jugadorInArea);
                boss.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
    // Función encargada de revisar cuando un player sale del stage del boss
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            jugadorInArea--;
        }
    }

}
