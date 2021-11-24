using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStats : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private EnemySpawnManager spawner;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float speed;
    [SerializeField] private float attack;
    [SerializeField] private float potenciaEmpuje;
    [SerializeField] private GameObject deathInstance;
    [SerializeField] private GameObject deathSound;
    [SerializeField] private GameObject cashDrop;

    [SerializeField] private CanvasGroup UIEnemigo;
    [SerializeField] private VidaUI enemyHealth;
    [SerializeField] private GameObject textDaño;
    [SerializeField] private int cashValue;

    [SerializeField] private bool needUI;

    [SerializeField] private bool invulnerable;

    [SerializeField] private NavMeshAgent agente;

    private void Start(){
        // Se inicializan las variables

        invulnerable = false;

        enemyHealth.setMaxVida(maxHealth);
        enemyHealth.setVida(health);

        if(needUI){
            UIEnemigo.alpha = 1f;
        } else{
            UIEnemigo.alpha = 0f;
        }

    }

    // Función encargada de setear el spawner de donde viene el enemigo
    public void setSpawner(EnemySpawnManager value){
        spawner = value;
    }

    // Función que permite cambiar el valor de invulnerabilidad
    public void setInvulnerable(bool value){
        invulnerable = value;
    }

    // Función que permite setear la vida máxima del objetivo
    public void setMaxHealth(float value){
        maxHealth = value;
        enemyHealth.setMaxVida(maxHealth);
    }

    // Función que permite setear la vida actual del objetivo
    public void setHealth(float value){
        health = value;
        enemyHealth.setVida(health);
    }

    // Función que retorna la vida del objetivo
    public float getHealth(){
        return health;
    }

    // Función que retorna la vida máxima del objetivo
    public float getMaxHealth(){
        return maxHealth;
    }

    // Función que permite dañar al enemigo
    public void decreaseHealth(float daño, GameObject dañoOwner){
        if(invulnerable && dañoOwner!=null){
            return;
        }

        health -= daño;

        if(dañoOwner!= null){
            if(GetComponent<EnemyCompor>()!= null){
                GetComponent<EnemyCompor>().aggroTarget(dañoOwner);
            } 
            if(GetComponent<EnemyIACompor>()!= null){
                GetComponent<EnemyIACompor>().aggroTarget(dañoOwner);
                GetComponent<EnemyIACompor>().setFriendStance(false);
            }
            if(GetComponent<BossCompor>()!= null){
                GetComponent<BossCompor>().aggroTarget(dañoOwner);
                GetComponent<BossCompor>().setFriendStance(false);
            }
        }

        //if(dañoOwner!=null){
            //Vector2 direcAtac= dañoOwner.transform.position - this.transform.position;
            //direcAtac = direcAtac.normalized*-1f;

            //StopAllCoroutines();
            //StartCoroutine(pushHit(direcAtac));
        //}

        if(dañoOwner!= null && dañoOwner.tag=="Player"){
            dañoOwner.GetComponent<Estadisticas>().addDamage(daño);
        }

        // Se instancia texto de Daño
        GameObject text = Instantiate(textDaño,transform.position,transform.rotation);
        text.GetComponent<TextMesh>().text = daño.ToString();

        enemyHealth.setVida(health);
        if(!enemyAlive()){

            if(GetComponent<BossSpawnSuccesiveBlock>()!= null){
                GetComponent<BossSpawnSuccesiveBlock>().destroyAttack();
            }

            //GameObject cashEnemy = Instantiate(cashDrop,transform.position,transform.rotation);
            //cashEnemy.GetComponent<CashDrop>().setCashValue(cashValue);
            //cashEnemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-3f,3f),Random.Range(-3f,3f)),ForceMode2D.Impulse);
            //cashValue -= cashEnemy.GetComponent<CashDrop>().getCashValue();
            
            if(dañoOwner!= null && dañoOwner.tag=="Player"){
                dañoOwner.GetComponent<PlayerStats>().plusCashPlayer(cashValue);
                dañoOwner.GetComponent<Estadisticas>().addKill(1);
            }
            

            if(spawner!=null){
                spawner.deleteEnemyDeath(this.gameObject);
            }

            if(deathSound!=null){
                GameObject soundDeath = Instantiate(deathSound,transform.position,transform.rotation);
                soundDeath.GetComponent<AudioSource>().pitch += Random.Range(-0.1f,0.1f);
                soundDeath.GetComponent<AudioSource>().Play();

                Destroy(soundDeath,3f);
            }
        
            GameObject deathInst = Instantiate(deathInstance,transform.position,transform.rotation);
            Vector2 direccion = Vector2.zero;
            if(GetComponent<EnemyCompor>()!= null){
                direccion = GetComponent<EnemyCompor>().getDirecPJ();
                GetComponent<EnemyCompor>().setIsAlive(false);
            } 
            if(GetComponent<EnemyIACompor>()!=null){
                direccion = GetComponent<EnemyIACompor>().getDirecPJ();
                GetComponent<EnemyIACompor>().setIsAlive(false);
            }
            if(GetComponent<BossCompor>()!= null){
                direccion = GetComponent<BossCompor>().getDirecPJ();
                GetComponent<BossCompor>().setIsAlive(false);
            }
            
            //Debug.Log(direccion);
            deathInst.GetComponent<Animator>().SetFloat("Horizontal",direccion.x);
            deathInst.GetComponent<Animator>().SetFloat("Vertical",direccion.y);
            Destroy(deathInst,8f);
            Destroy(this.gameObject);
        }
    }

    // Función que empuja al objetivo según la dirección entregada
    IEnumerator pushHit(Vector2 direccion){
        GetComponent<Rigidbody2D>().AddForce(direccion*potenciaEmpuje,ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.05f);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    // Función que permite cambiar los valores de patrones básicos de un enemigo
    public void setSpawnStats(float velocidad, float daño, float vida, float maxVida, int dinero){
        speed = velocidad;
        agente.speed = speed;

        attack = daño;

        health = vida;
        maxHealth = maxVida;
        cashValue = dinero;
        enemyHealth.setVida(health);
        enemyHealth.setMaxVida(maxHealth);
    }

    // Función que permite cambiar los valores de patrones básicos de un enemigos sin alterar sus valores iniciales de Spawn
    public void addSetSpawnStats(float velocidad, float daño, float vida, float maxVida, int dinero){
        speed += velocidad;
        attack += daño;
        health += vida;
        maxHealth += maxVida;
        cashValue += dinero;

        agente.speed = speed;
        enemyHealth.setVida(health);
        enemyHealth.setMaxVida(maxHealth);
    }

    // Función que verifica si el objetivo está vivo
    private bool enemyAlive(){
        if(health>0){
            return true;
        } else{
            return false;
        }
    }

    // Función que retorna el daño que el enemigo realiza
    public float getDañoAttack(){
        return Mathf.Floor(attack);
    }
    
}
