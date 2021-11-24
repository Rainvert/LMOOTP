using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class BulletEmpuje : MonoBehaviour
{

    private float bulletDamage;

    [SerializeField] private float radioImpacto,fuerzaImpacto,duracionEmpuje;
    [SerializeField] private GameObject SpawnSound;
    [SerializeField] private GameObject DestroySound;
    [SerializeField] private Animator animBullet;
    [SerializeField] private GameObject bulletOwner;
    [SerializeField] private bool isPlayerBullet;
    [SerializeField] private LayerMask atacable;
    
    private Light2D luzBullet;
    private Rigidbody2D rb_bullet;
    private Collider2D col_bullet;

    // Start is called before the first frame update
    void Start()
    {
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

    // Función encargada de explotar el proyectil al impactar con un elemento/escenario/player
    void OnCollisionEnter2D(Collision2D col){
        rb_bullet.velocity = Vector2.zero;  
        col_bullet.enabled = false;
        animBullet.enabled = true;

        Camera.main.GetComponent<FollowCamZoom>().shakeCamDistance(transform.position,0.2f);

        animBullet.SetBool("isDestroyed",true);
        GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
        instance.GetComponent<AudioSource>().Play();
        Destroy(instance,2f);

    }

    // Función encargada de explotar el proyectil al impactar con un elemento interactivo enemigo/escenario
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Enemy"){

            // Se empuja al enemigo según la potencia de disparo
            Collider2D[] collidedWith = Physics2D.OverlapCircleAll(transform.position,radioImpacto,atacable);
            
            StopAllCoroutines();
            StartCoroutine(stopKnockBack(collidedWith));

            // Se anima la explosión y se detiene la colisión para evitar múltiples efectos
            rb_bullet.velocity = Vector2.zero;  
            col_bullet.enabled = false;
            animBullet.enabled = true;

            Camera.main.GetComponent<FollowCamZoom>().shakeCamDistance(transform.position,0.2f);

            animBullet.SetBool("isDestroyed",true);

            GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
            instance.GetComponent<AudioSource>().Play();
            Destroy(instance,2f);

        } else{
            if((col.gameObject.layer != 9 && col.gameObject.layer != 10) && col.tag!="Bullet" && isPlayerBullet){
                // Se anima la explosión y se detiene la colisión para evitar múltiples efectos
                rb_bullet.velocity = Vector2.zero;  
                col_bullet.enabled = false;
                animBullet.enabled = true;

                animBullet.SetBool("isDestroyed",true);

                Camera.main.GetComponent<FollowCamZoom>().shakeCamDistance(transform.position,0.2f);

                GameObject instance = Instantiate(DestroySound,transform.position,transform.rotation);
                instance.GetComponent<AudioSource>().Play();
                Destroy(instance,2f);
            }
        }

    }

    // Función encargada del impulso dado por el knockback
    IEnumerator stopKnockBack(Collider2D[] colliders){

        foreach (var coll in colliders)
        {
            if(coll.tag=="Enemy"){
                Rigidbody2D rbEnemyInit = coll.GetComponent<Rigidbody2D>();
                if(rbEnemyInit!= null){
                    rbEnemyInit.AddForce(rb_bullet.velocity.normalized*fuerzaImpacto,ForceMode2D.Impulse);
                    coll.GetComponent<EnemyStats>().decreaseHealth(Mathf.Floor(bulletDamage), bulletOwner);
                }
            }
        }
        
        yield return new WaitForSeconds(duracionEmpuje);

        foreach (var coll in colliders)
        {
            if(coll!=null){
                if(coll.tag=="Enemy"){
                    Rigidbody2D rbEnemyEnd = coll.GetComponent<Rigidbody2D>();
                    rbEnemyEnd.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                }
            }
            
        }
        
        yield break;
    }

    // Función que instancia el daño que tiene el proyectil lanzado
    public void setBulletDamage(float damage){
        bulletDamage = damage;
    }

    // Función que instancia el radio de explosión del proyectil
    public void setRadioEmpuje(float value){
        radioImpacto = value;
    }

    private void DestroyBullet(){
        Destroy(this.gameObject);
    }
}

