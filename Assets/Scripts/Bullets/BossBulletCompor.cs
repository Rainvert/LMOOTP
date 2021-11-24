using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BossBulletCompor : MonoBehaviour
{
    private float bulletDamage;
    private Transform target;
    private float velocidad;

    [SerializeField] private GameObject bulletOwner;

    [SerializeField] private GameObject DestroySound;
    [SerializeField] private Animator animBullet;
    [SerializeField] private float radioDeVision;

    [SerializeField] private LayerMask isAtacable;
    private Light2D luzBullet;
    private Rigidbody2D rb_bullet;
    private Collider2D col_bullet;
    private bool isInVision;

    // Start is called before the first frame update
    void Start()
    {
        rb_bullet = GetComponent<Rigidbody2D>();
        col_bullet = GetComponent<Collider2D>();
        luzBullet = GetComponent<Light2D>();

        Destroy(this.gameObject,3f);
    }

    void Update(){
        if(target!=null){
            transform.position = Vector2.MoveTowards(transform.position,target.position,velocidad*Time.deltaTime);
        } else{
            isInVision = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);
            Collider2D targetSeguir = Physics2D.OverlapCircle(transform.position,radioDeVision,isAtacable);
            if(isInVision){
                target = targetSeguir.transform;
            }
        }
    }

    // Función que setea el owner de la bala
    public void setBulletOwner(GameObject owner){
        bulletOwner = owner;
    }

    // Función que retorna el owner de la bala
    public GameObject getBulletOwner(){
        return bulletOwner;
    }

    // Función que permite setear el target que persigue el objetivo
    public void setTarget(Transform objetivo){
        target = objetivo;
    }

    // Función que permite setear la velocidad con la que persigue al objetivo
    public void setVelocidad(float value){
        velocidad = value;
    }

    // Función encargada de explotar el proyectil al impactar con un collider
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag != "Bullet"){
            rb_bullet.velocity = Vector2.zero;  
            col_bullet.enabled = false;
            luzBullet.pointLightOuterRadius = 0.5f;

            animBullet.SetBool("Explote",true);

            GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
            instance.GetComponent<AudioSource>().Play();
            Destroy(instance,2f);

            Destroy(this.gameObject,0.5f);
        }


    }

    // Función encargada de explotar el proyectil al entrar a un trigger
    void OnTriggerEnter2D(Collider2D col){
        // Si es un player o un edificio atacable
        if(col.gameObject.layer == 6 || col.gameObject.layer == 10){
            // Se anima la explosión y se detiene la colisión para evitar múltiples efectos
            rb_bullet.velocity = Vector2.zero;  
            col_bullet.enabled = false;
            luzBullet.pointLightOuterRadius = 0.5f;

            animBullet.SetBool("Explote",true);

            GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
            instance.GetComponent<AudioSource>().Play();
            Destroy(instance,2f);
            
            Destroy(this.gameObject,0.5f);

            if(col.GetComponent<PlayerStats>()!=null){
                col.GetComponent<PlayerStats>().damageHealth(bulletDamage);
            }
            if(col.GetComponent<NucleoStats>()!=null){
                col.GetComponent<NucleoStats>().damageHealth(bulletDamage);
            }
            if(col.GetComponent<BuildStats>()!=null){
                col.GetComponent<BuildStats>().damageHealth(bulletDamage);
            }
        } else{
            if(col.gameObject.layer!= 8 && col.gameObject.layer!= 9 && col.gameObject.layer!=11 && col.gameObject.tag!="Bullet"){
                rb_bullet.velocity = Vector2.zero;  
                col_bullet.enabled = false;
                animBullet.enabled = true;
                luzBullet.pointLightOuterRadius = 0.5f;

                animBullet.SetBool("Explote",true);

                if(col.GetComponent<EnemyStats>()!=null){
                    col.GetComponent<EnemyStats>().decreaseHealth(bulletDamage,this.gameObject);
                }

                GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
                instance.GetComponent<AudioSource>().Play();
                Destroy(instance,2f);

                Destroy(this.gameObject,0.5f);
            }
        }
    }

    // Función que instancia el daño que tiene el proyectil lanzado
    public void setBulletDamage(float damage){
        bulletDamage = damage;
    }

    private void DestroyBullet(){
        Destroy(this.gameObject);
    }
}
