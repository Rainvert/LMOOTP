using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIACompor : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private bool isAlive;
    [SerializeField] private Transform objetivo;    
    [SerializeField] private Animator animMov;
    [SerializeField] private GameObject alertSound;

    [SerializeField] private LayerMask isAtacable;
    [SerializeField] private Vector2 velocidad;
    [SerializeField] private Vector2 direcAtac;

    [SerializeField] private EnemyStats stats;

    [SerializeField] private GameObject bullet;
    [SerializeField] private float potenciaTiro;
    [SerializeField] private float damageBullet;
    [SerializeField] private bool friendStance;
    private NavMeshAgent agente;

    [SerializeField] private float radioDeAtaque,radioDeVision,coolDown;

    private bool isInAtaque, isInVision, needDaño, canAttack, soundPlayed, aggro;
    [SerializeField] private bool enemyFromSpawn;

    [SerializeField] private bool leftDirection,rightDirection,upDirection,downDirection;

    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        agente = GetComponent<NavMeshAgent>();
        agente.updateRotation = false;
        agente.updateUpAxis = false;

        canAttack = true;
        aggro = false;

        soundPlayed = false;

        if(enemyFromSpawn){
            aggroTarget(Camera.main.gameObject);
        } else{
            objetivo = null;
        }

        
        setDirectionStart();
    }

    // Función que permite cambiar el valor de isAlive
    public void setIsAlive(bool value){
        isAlive = value;
    }

    // Update is called once per frame
    void Update()
    {
        if(!friendStance && isAlive){
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

    // Función que detiene el movimiento de la entidad actual
    public void stopMovement(){
        agente.SetDestination(this.transform.position);
    }

    // Función que permite setear una dirreción específica a la que mire el enemigo
    private void setDirectionStart(){
        if(leftDirection){
            direcAtac = new Vector2(-1,0); 
        }
        if(rightDirection){
            direcAtac = new Vector2(1,0); 
        }
        if(upDirection){
            direcAtac = new Vector2(0,1); 
        }
        if(downDirection){
            direcAtac = new Vector2(0,-1); 
        }
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);
    }

    // Función que modifica el valor de EnemyFromSpawn
    public void setEnemyFromSpawn(bool value){
        enemyFromSpawn = value;
    }


    // Función que permite setear la amistad o enemistad del enemigo
    public void setFriendStance(bool value){
        friendStance = value;
    }

    // Función encargada de activar el aggro del enemigo
    public void aggroTarget(GameObject targetAggro){
        objetivo = targetAggro.transform;
        StartCoroutine(aggroCompor());
        FollowPlayer();
    }

    // Corrutina que moviliza al enemigo a una zona y lo pone en estado de alerta
    IEnumerator aggroCompor(){
        aggro = true;
        yield return new WaitForSeconds(3f);
        aggro = false;
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

        animMov.SetBool("Chasing",true);

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);

    }

    // Función encargada de mantener al enemigo en un patrullaje
    private void StaticVigia(){

        soundPlayed = false;

        agente.isStopped = true;

        if(objetivo!= null && objetivo.tag!="MainCamera"){
            direcAtac= FindRealTarget(objetivo.gameObject) - this.transform.position;
            direcAtac = direcAtac.normalized;
        }

        animMov.SetBool("Chasing",false);

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);
    }

    // Función encargada de atacar al target actual
    private void AtacarTarget(){
        agente.isStopped = true;

        animMov.SetBool("Chasing",false);

        if(canAttack){
            StopAllCoroutines();
            StartCoroutine(coolDownAttack());
        }
    }

    // Función que controla el delay y la animación entre ataques de un enemigo
    IEnumerator coolDownAttack(){
        canAttack = false;

        direcAtac= FindRealTarget(objetivo.gameObject) - this.transform.position;

        direcAtac = direcAtac.normalized;

        // Se setean los valores para el animador 
        animMov.SetFloat("Horizontal",direcAtac.x);
        animMov.SetFloat("Vertical",direcAtac.y);

        GameObject insBullet = Instantiate(bullet,transform.position,transform.rotation);
        insBullet.GetComponent<Rigidbody2D>().velocity = direcAtac*potenciaTiro;
        insBullet.GetComponent<BulletComporEnemy>().setBulletOwner(this.gameObject);
        insBullet.GetComponent<BulletComporEnemy>().setBulletDamage(damageBullet);
        insBullet.transform.Rotate(0f,0f,Mathf.Atan2(direcAtac.y,direcAtac.x)* Mathf.Rad2Deg);

        yield return new WaitForSeconds(coolDown);

        canAttack = true;

    }

    // Función que permite obtener la dirección de ataque del enemigo
    public Vector2 getDirecPJ(){
        return velocidad;
    }

}
