using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorretaCompor : BuildStats
{
    // Variables necesarias
    [SerializeField] private float damage;
    [SerializeField] private GameObject bulletInstance;

    [SerializeField] private Transform target;
    [SerializeField] private LayerMask isAtacable;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float speedRotation;
    [SerializeField] private float coolDown;
    [SerializeField] private float radioDeAtaque;
    [SerializeField] private bool isInAtaque;
    [SerializeField] private bool inCoolDown = false;

    [SerializeField] private GameObject linternaTorre;

    [SerializeField] private GameObject playerOwner;

    void Update(){

        // Se chequea si el enemigo está dentro del rango de ataque
        isInAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);
        Collider2D targetAtaque = Physics2D.OverlapCircle(transform.position,radioDeAtaque,isAtacable);

        if(isInAtaque){
            target = targetAtaque.gameObject.transform;
            GetComponent<Animator>().SetBool("Atacando",true);
            this.transform.parent.GetComponentInChildren<Light2D>().color = Color.red;
            this.transform.parent.GetComponentInChildren<Light2D>().intensity = 0.6f;
            apuntarLinterna();
            atacarTarget();
        } else{
            GetComponent<Animator>().SetBool("Atacando",false);
            this.transform.parent.GetComponentInChildren<Light2D>().color = Color.white;
            this.transform.parent.GetComponentInChildren<Light2D>().intensity = 0.2f;
            rotateAroundLinterna();
        }
    }

    // Función que setea el owner de la construcción
    public void setOwner(GameObject value){
        playerOwner = value;
    }

    // Función encargada de atacar a un objetivo si este está dentro del radio de ataque
    private void atacarTarget(){
        if(!inCoolDown){
            StartCoroutine(spawnBullet());
        }
    }

    // Función encargada de actualizar la posición de la linterna relativa al objetivo que está viendo en dicho momento
    private void apuntarLinterna(){
        
        Vector2 dir = (target.position - transform.position).normalized;

        // Se ubica la rotación de la linterna
        Vector2 dirLinterna = new Vector2(dir.x,dir.y);
        linternaTorre.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dirLinterna.y,dirLinterna.x)* Mathf.Rad2Deg - 90));

    }

    // Función encargada de rotar la linterna alrededor de sí misma
    private void rotateAroundLinterna(){
        
        linternaTorre.transform.Rotate(Vector3.forward*Time.deltaTime*speedRotation,Space.Self);
    }    

    // Corrutina encargada de spawnear una bala en la dirección del enemigo
    IEnumerator spawnBullet(){
        
        inCoolDown = true;
        
        Vector2 dir = (target.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletInstance,transform.position,transform.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = dir*bulletSpeed;
        bullet.GetComponent<BulletEmpuje>().setBulletOwner(playerOwner);
        bullet.GetComponent<BulletEmpuje>().setBulletDamage(damage);
        bullet.transform.Rotate(0f,0f,Mathf.Atan2(dir.y,dir.x)* Mathf.Rad2Deg);

        yield return new WaitForSeconds(coolDown);

        inCoolDown = false;
    }

}
