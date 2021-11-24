using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyCompor : MonoBehaviour
{
    
    // Variables necesarias
    [SerializeField] private Transform objetivo;
    [SerializeField] private Transform objetivoAggro;
    [SerializeField] private Transform nucleo;
    [SerializeField] private Animator animMov;
    [SerializeField] private GameObject alertSound;

    [SerializeField] private EnemyStats stats;

    [SerializeField] private LayerMask isAtacable;
    [SerializeField] private Vector2 velocidad;
    [SerializeField] private bool isAlive;
    
    private NavMeshAgent agente;

    [SerializeField] private float radioDeAtaque,radioDeVision,coolDown;

    [SerializeField] private bool isInAtaque, isInVision, needDaño, canAttack, soundPlayed, hasSpawned, enemyFromSpawn;

    [SerializeField] private float calmDownTimer, calmContador;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;
        needDaño = false;
        canAttack = true;

        soundPlayed = false;
        hasSpawned = false;

        objetivo = null;

        GameObject findNucleo = GameObject.FindGameObjectWithTag("Nucleo");
        if(findNucleo != null){
            nucleo = findNucleo.transform;
        } else{
            nucleo = Camera.main.transform;
        }
    
        StartCoroutine(AtaqueTarget());
    }

    // Función que permite cambiar el valor de isAlive
    public void setIsAlive(bool value){
        isAlive = value;
    }

    // Update is called once per frame
    void Update()
    {
        if(hasSpawned && isAlive){
            // Se chequea si el personaje está dentro del rango de ataque y visión
            isInVision = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);
            Collider2D targetSeguir = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);

            isInAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);
            Collider2D targetAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);

            // Perseguir
            if(isInVision && !isInAtaque){
                objetivo = targetSeguir.gameObject.transform;
                objetivoAggro = objetivo;
                calmContador = 0f;
                //Debug.Log("Siguiendolo");
                FollowPlayer();
                JustOnceSound();
            }

            // Estático
            if(!isInVision && !isInAtaque){
                //Debug.Log("Yendo al Núcleo");
                if(enemyFromSpawn && objetivoAggro == null){
                    objetivoAggro = nucleo;
                }
                GoingTo(objetivoAggro);
                CalmAggro();
            }

            // Ataque
            if(isInVision && isInAtaque){
                objetivo = targetAtaque.gameObject.transform;
                //Debug.Log("Ataca");
                AtacarTarget();
            }
        }
    }

    // Función que detiene el movimiento de la entidad actual
    public void stopMovement(){
        nucleo = null;
        agente.SetDestination(this.transform.position);
    }

    // Función que detiene la persecución contra un objetivo al salir de su rango
    private void CalmAggro(){
        if(objetivoAggro != nucleo && objetivoAggro != null){
            calmContador += Time.deltaTime;
            if(calmContador >= calmDownTimer){
                calmContador = 0f;
                if(enemyFromSpawn){
                    objetivoAggro = nucleo;
                } else{
                    objetivoAggro = null;
                    soundPlayed = false;
                }
            }
        }
    }

    // Función que modifica el valor de EnemyFromSpawn
    public void setEnemyFromSpawn(bool value){
        enemyFromSpawn = value;
    }

    // Función que permite setear el valor del rango de vision
    public void setRangoDeVision(float value){
        radioDeVision = value;
    }

    // Función que permite activar el valor de hasSpawned
    public void activateHasSpawned(){
        hasSpawned = true;
    }

    // Función encargada de activar el aggro del enemigo
    public void aggroTarget(GameObject targetAggro){
        objetivoAggro = targetAggro.transform;
    }

    // Función que alerta un sonido cuando el enemigo detecto un edificio o jugador
    private void JustOnceSound(){
        if(!soundPlayed && alertSound!= null){
            soundPlayed = true;
            GameObject soundInst = Instantiate(alertSound,transform.position,transform.rotation);
            soundInst.GetComponent<AudioSource>().pitch += Random.Range(-0.1f,0.1f);
            soundInst.GetComponent<AudioSource>().Play();
            Destroy(soundInst,1f);
        }
    }

    // Función encargada de Seguir al personaje
    private void FollowPlayer(){

        agente.isStopped = false;

        agente.SetDestination(objetivo.position);
        velocidad = agente.velocity;

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",velocidad.x);
        animMov.SetFloat("Vertical",velocidad.y);

        animMov.SetBool("Atacar",false);
    }

    // Función encargada de llevar al enemigo directo a un objetivo establecido
    private void GoingTo(Transform objPos){

        agente.isStopped = false;

        if(objPos!=null){
            agente.SetDestination(objPos.position);
            velocidad = agente.velocity;
        } else{
            agente.isStopped = true;
            velocidad = Vector2.zero;
        }

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",velocidad.x);
        animMov.SetFloat("Vertical",velocidad.y);

        animMov.SetBool("Atacar",false);
    }

    // Función encargada de atacar al target actual
    private void AtacarTarget(){
        //agente.isStopped = true;
        
        if(canAttack){
            StartCoroutine(coolDownAttack());
        }
    }

    // Función que controla el delay y la animación entre ataques de un enemigo
    IEnumerator coolDownAttack(){
        canAttack = false;

        Vector2 direcAtac= objetivo.transform.position - this.transform.position;
        direcAtac = direcAtac.normalized;

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);

        animMov.SetBool("Atacar",true);

        yield return new WaitForSeconds(coolDown);

        canAttack = true;

    }

    // Función que permite obtener la dirección de ataque del enemigo
    public Vector2 getDirecPJ(){
        return velocidad;
    }

    // Funciones de animación
    private void Dañar(){
        //Debug.Log("daño");
        needDaño = true;
    }

    private void EndDañar(){
        animMov.SetFloat("Horizontal",0);
        animMov.SetFloat("Vertical",0);
        animMov.SetBool("Atacar",false);
    }

    // Función encargada de monitorear si se debe dañar o no al target correspondiente
    IEnumerator AtaqueTarget(){
        while(true){
            if(needDaño){
                //Debug.Log("Ataco");
                if(objetivo!=null){
                    if(objetivo.GetComponent<PlayerStats>()!=null){
                        objetivo.GetComponent<PlayerStats>().damageHealth(GetComponent<EnemyStats>().getDañoAttack());
                    }
                    if(objetivo.GetComponent<NucleoStats>()!=null){
                        objetivo.GetComponent<NucleoStats>().damageHealth(GetComponent<EnemyStats>().getDañoAttack());
                    }
                    if(objetivo.GetComponent<BuildStats>()!=null){
                        objetivo.GetComponent<BuildStats>().damageHealth(GetComponent<EnemyStats>().getDañoAttack());
                    }
                    needDaño = false;
                }

            }
            yield return null;
        }
    }
}
