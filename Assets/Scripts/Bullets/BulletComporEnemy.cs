using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BulletComporEnemy : MonoBehaviour
{
    private float bulletDamage;
    [SerializeField] private GameObject bulletOwner;
    [SerializeField] private GameObject SpawnSound;
    [SerializeField] private GameObject DestroySound;
    [SerializeField] private Animator animBullet;
    private Light2D luzBullet;
    private Rigidbody2D rb_bullet;
    private Collider2D col_bullet;

    // Start is called before the first frame update
    void Start()
    {
        animBullet.enabled = false;
        rb_bullet = GetComponent<Rigidbody2D>();
        col_bullet = GetComponent<Collider2D>();
        luzBullet = GetComponent<Light2D>();
        
        GameObject instance = Instantiate(SpawnSound,transform.position,transform.rotation);
        instance.GetComponent<AudioSource>().Play();
        Destroy(instance,2f);
        
        Destroy(this.gameObject,2f);
    }

    // Función que setea el owner de la bala
    public void setBulletOwner(GameObject owner){
        bulletOwner = owner;
    }

    // Función que retorna el owner de la bala
    public GameObject getBulletOwner(){
        return bulletOwner;
    }

    // Función encargada de explotar el proyectil al impactar con un collider
    void OnCollisionEnter2D(Collision2D col){
        rb_bullet.velocity = Vector2.zero;  
        col_bullet.enabled = false;
        animBullet.enabled = true;
        luzBullet.pointLightOuterRadius = 0.5f;

        animBullet.SetBool("isDestroyed",true);

        GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
        instance.GetComponent<AudioSource>().Play();
        Destroy(instance,2f);

    }

    // Función encargada de explotar el proyectil al entrar a un trigger
    void OnTriggerEnter2D(Collider2D col){
        // Si es un player o un edificio atacable
        if(col.gameObject.layer == 6 || col.gameObject.layer == 10){
            // Se anima la explosión y se detiene la colisión para evitar múltiples efectos
            rb_bullet.velocity = Vector2.zero;  
            col_bullet.enabled = false;
            animBullet.enabled = true;
            luzBullet.pointLightOuterRadius = 0.5f;

            animBullet.SetBool("isDestroyed",true);

            GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
            instance.GetComponent<AudioSource>().Play();
            Destroy(instance,2f);

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
            if(bulletOwner!= null){
                if(col.gameObject.layer!= 9 && col.gameObject.layer!=bulletOwner.layer){
                    rb_bullet.velocity = Vector2.zero;  
                    col_bullet.enabled = false;
                    animBullet.enabled = true;
                    luzBullet.pointLightOuterRadius = 0.5f;

                    animBullet.SetBool("isDestroyed",true);

                    if(col.GetComponent<EnemyStats>()!=null){
                        col.GetComponent<EnemyStats>().decreaseHealth(bulletDamage,this.gameObject);
                    }

                    GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
                    instance.GetComponent<AudioSource>().Play();
                    Destroy(instance,2f);
                }
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
