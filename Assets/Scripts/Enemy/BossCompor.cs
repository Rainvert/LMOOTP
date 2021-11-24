using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossCompor : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private bool isAlive;
    [SerializeField] private Transform objetivo;    
    [SerializeField] private Animator animMov;
    [SerializeField] private GameObject alertSound;

    [SerializeField] private BossSpawnSuccesiveBlock SPAttack;
    [SerializeField] private EnemyStats statsEnemy;

    [SerializeField] private LayerMask isAtacable;
    [SerializeField] private Vector2 velocidad;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float potenciaTiro;
    [SerializeField] private float damageBullet;
    [SerializeField] private bool friendStance;
    [SerializeField] private float bulletMax;
    [SerializeField] private float offsetVelocidad;
    [SerializeField] private float offsetDistancia;
    [SerializeField] private float bulletBetween;

    private NavMeshAgent agente;

    [SerializeField] private float radioDeAtaque,radioDeVision,coolDown;

    [SerializeField] private bool shootTime;

    private bool isInAtaque, isInVision, needDaño, canAttack, soundPlayed, aggro, enemyFromSpawn;
    [SerializeField] private bool inEngage, inSomething;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        SPAttack = GetComponent<BossSpawnSuccesiveBlock>();
        statsEnemy = GetComponent<EnemyStats>();
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        canAttack = true;
        aggro = false;
        inSomething = false;
        soundPlayed = false;

        if(enemyFromSpawn){
            aggroTarget(Camera.main.gameObject);
        } else{
            objetivo = null;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(isAlive){
            if(statsEnemy.getHealth() <= statsEnemy.getMaxHealth()/2 && !inEngage){
                inEngage = true;
                inSomething = true;
                shootTime = false;
                statsEnemy.setInvulnerable(true);
                animMov.SetTrigger("Engage");
                animMov.SetBool("InRage",true);
                radioDeAtaque = 3;
                radioDeVision = 20;
                agente.speed *= 1.5f;
                agente.isStopped = true;
            }

            if(!friendStance && !inSomething){
                // Se chequea si el personaje está dentro del rango de ataque y visión
                isInVision = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);
                Collider2D targetSeguir = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);

                isInAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);
                Collider2D targetAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);

                // Perseguir
                if(isInVision && !isInAtaque){
                    objetivo = targetSeguir.gameObject.transform;
                    //Debug.Log("Siguiendolo");
                    FollowPlayer();
                    JustOnceSound();
                }

                // Estático
                if(!isInVision && !isInAtaque && !aggro){
                    //Debug.Log("Yendo al Núcleo");
                    StaticVigia();
                }

                // Ataque
                if(isInVision && isInAtaque){
                    objetivo = targetSeguir.gameObject.transform;
                    //Debug.Log("Ataca");
                    AtacarTarget();
                }
            }
        }
        
    }
    
    // Función que permite cambiar el valor de isAlive
    public void setIsAlive(bool value){
        isAlive = value;
    }

    // Función que detiene el movimiento de la entidad actual
    public void stopMovement(){
        agente.SetDestination(this.transform.position);
    }

    // Función que modifica el valor de EnemyFromSpawn
    public void setEnemyFromSpawn(bool value){
        enemyFromSpawn = value;
    }

    // Función que permite cambiar el valor de invulnerabilidad luego de realizar la transformación
    public void endPhaseChange(){
        statsEnemy.setInvulnerable(false);
        inSomething = false;
    }

    // Función que permite cambiar el valor de shootTime
    public void timetoShoot(){
        shootTime = true;
    }


    // Función que permite setear la amistad o enemistad del enemigo
    public void setFriendStance(bool value){
        friendStance = value;
    }

    // Función encargada de activar el aggro del enemigo
    public void aggroTarget(GameObject targetAggro){
        if(!isInAtaque){
            StartCoroutine(aggroCompor());
            objetivo = targetAggro.transform;
            FollowPlayer();
        }
    }

    // Corrutina que moviliza al enemigo a una zona y lo pone en estado de alerta
    IEnumerator aggroCompor(){
        Debug.Log("hello");
        aggro = true;
        yield return new WaitForSeconds(3f);
        aggro = false;

        yield return null;
    }

    // Función encargada de encontrar el punto exacto real del Target
    public Vector3 FindRealTarget(GameObject targetSup){
        float altura = targetSup.GetComponent<SpriteRenderer>().bounds.size.y/2;
        Vector3 targetPos = targetSup.transform.position;
        targetPos.y -= altura*0.8f;
        return targetPos;
    }

    // Función que alerta un sonido cuando el enemigo detecto un edificio o jugador
    private void JustOnceSound(){
        if(!soundPlayed && alertSound!= null){
            soundPlayed = true;
            GameObject soundInst = Instantiate(alertSound,transform.position,transform.rotation);
            soundInst.GetComponent<AudioSource>().Play();
            Destroy(soundInst,1f);
        }
    }

    // Función encargada de Seguir al personaje
    private void FollowPlayer(){

        agente.isStopped = false;
        shootTime = false;

        agente.SetDestination(objetivo.position);
        velocidad = agente.velocity;

        animMov.SetBool("AttackPlayer",false);
        animMov.SetBool("Chasing",true);
        
        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",velocidad.x);
        animMov.SetFloat("Vertical",velocidad.y);

    }

    // Función encargada de mantener al enemigo en un patrullaje
    private void StaticVigia(){

        soundPlayed = false;
        shootTime = false;

        agente.isStopped = true;

        animMov.SetBool("AttackPlayer",false);
        animMov.SetBool("Chasing",false);

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",velocidad.x);
        animMov.SetFloat("Vertical",velocidad.y);
    }

    // Función encargada de atacar al target actual
    private void AtacarTarget(){
        agente.isStopped = true;

        animMov.SetBool("Chasing",false);
        animMov.SetBool("AttackPlayer",true);

        if(canAttack){
            StopAllCoroutines();
            if(!inEngage){
                StartCoroutine(coolDownAttack());
                Debug.Log("ataque normal");
            } else{
                StartCoroutine(coolDownAttackSP());
                Debug.Log("ataque SP");
            }
            
        }
    }

    // Función que controla el delay y la animación entre ataques de un enemigo
    IEnumerator coolDownAttack(){
        canAttack = false;

        while(!shootTime){
            yield return null;
        }

        Vector2 direcAtac= FindRealTarget(objetivo.gameObject) - this.transform.position;

        direcAtac = direcAtac.normalized;

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);
        
        for (int i = 0; i < bulletMax; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(0,offsetDistancia),Random.Range(0,offsetDistancia),Random.Range(0,offsetDistancia));
            GameObject insBullet = Instantiate(bullet,transform.position+randomOffset,transform.rotation);
            insBullet.GetComponent<BossBulletCompor>().setBulletOwner(this.gameObject);
            insBullet.GetComponent<BossBulletCompor>().setTarget(objetivo);
            insBullet.GetComponent<BossBulletCompor>().setVelocidad(potenciaTiro + Random.Range(0,offsetVelocidad));
            insBullet.GetComponent<BossBulletCompor>().setBulletDamage(damageBullet);       
            yield return new WaitForSeconds(bulletBetween);    
        }


        yield return new WaitForSeconds(coolDown);

        canAttack = true;

        yield return null;

    }

    // Función que controla el delay y la animación entre ataques de un enemigo
    IEnumerator coolDownAttackSP(){
        canAttack = false;
        inSomething = true;

        while(!shootTime){
            yield return null;
        }

        SPAttack.activateAttack();

        while(SPAttack.getAttackMode()){
            yield return null;
        }

        canAttack = true;
        inSomething = false;

        yield return null;

    }

    // Función que permite obtener la dirección de ataque del enemigo
    public Vector2 getDirecPJ(){
        return velocidad;
    }

}
