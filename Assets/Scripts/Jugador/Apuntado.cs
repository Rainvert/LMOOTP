using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Apuntado : MonoBehaviour
{

    // Variables necesarias
    private PlayerControls playerBinds;
    private Camera mainCam;
    [SerializeField] GameObject mira;
    [SerializeField] GameObject linterna;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletEmpuje;
    [SerializeField] float distanciaMira;
    [SerializeField] float distanciaLinterna;
    [SerializeField] Animator movAnim;

    [SerializeField] float potenciaTiro;
    [SerializeField] private bool haveWeapon;
    private Vector3 bulletDirect;
    private Vector3 targetPos;
    private Vector2 movMira;

    private bool apuntando;
    private bool canShoot1st,canShoot2nd;
    private bool holdingShoot1st,holdingShoot2nd;
    private PlayerStats stats;

    [SerializeField] private string device = "None";

    void Start(){
        mira.GetComponent<SpriteRenderer>().enabled = false;
        stats = GetComponent<PlayerStats>();
        linterna.SetActive(false);
        mainCam = Camera.main;
        apuntando = false;
        targetPos = new Vector2(1f,1f);
        canShoot1st = true;
        canShoot2nd = true;

        StartCoroutine(CheckShootBullet1st());
        StartCoroutine(CheckShootBullet2nd());
    }

    void Update(){
        if(apuntando && haveWeapon){
            MoveMiraToPoint();
        }
    }

    // Función que permite cambiar el color de la mira del jugador
    public void setColorMira(Color newColor){
        mira.GetComponent<SpriteRenderer>().color = newColor;
    }

    // Función que permite setear el dispositivo que está usando el player
    public void setDevice(string value){
        device = value;
    }

    // Función que permite obtener el dipositivo que está usando el player
    public string getDevice(){
        return device;
    }

    // Funciones encargadas de manejar los eventos del Input Handler
    public void onDisparoPrimario(InputAction.CallbackContext context){
        if(context.performed && haveWeapon){
            holdingShoot1st = true;
            //ShootBullet();
            //Debug.Log("Disparando");
        }
        if(context.canceled){
            holdingShoot1st = false;
            //Debug.Log("Se soltó el disparo");
        }
    }

    public void onDisparoSecundario(InputAction.CallbackContext context){
        if(context.performed && haveWeapon){
            holdingShoot2nd = true;
            //ShootBullet();
            //Debug.Log("Disparando");
        }
        if(context.canceled){
            holdingShoot2nd = false;
            //Debug.Log("Se soltó el disparo");
        }
    }

    public void onApuntar(InputAction.CallbackContext context){
        if(context.performed && haveWeapon){
            ApuntarArma();
        }
    }

    public void onMoveMira(InputAction.CallbackContext context){
        if(haveWeapon){
            movMira = context.ReadValue<Vector2>();
        }
    }

    // Función encargada de Mover la mira a la posición del mouse
    private void MoveMiraToPoint(){
        Vector2 posScreen = movMira;
        
        // Se obtiene la posicion del mouse en la pantalla
        if(device != "Teclado"){
            //Debug.Log("Control");
            if(posScreen!=Vector2.zero){
                targetPos = posScreen*10f;
            }
        } else{
            //Debug.Log("Mouse");
            Vector3 worldScreenPos = mainCam.ScreenToWorldPoint(posScreen);

            // Se obtiene el vector que apunta en la dirección del mouse en todo momento (Quieto o Moviendose)
            targetPos = worldScreenPos - transform.position;
        }

        Vector3 targetPosMira = new Vector3(targetPos.x,targetPos.y,0f);
        Vector3 targetPosLinterna = new Vector3(targetPos.x,targetPos.y,0f);

        // Se posiciona la mira
        if(targetPosMira.magnitude>distanciaMira && device!="Teclado"){
            targetPosMira = targetPosMira.normalized;
            targetPosMira *= distanciaMira;
        }

        mira.transform.localPosition = targetPosMira;

        movAnim.SetFloat("xMira",targetPosMira.x);
        movAnim.SetFloat("yMira",targetPosMira.y);

        // Se posiciona la linterna
        if(targetPosLinterna.magnitude>distanciaLinterna){
            targetPosLinterna  = targetPosLinterna.normalized;
            targetPosLinterna  *= distanciaLinterna;
        }

        // Se ubica la rotación de la linterna
        Vector2 dirLinterna = new Vector2(mira.transform.localPosition.x,mira.transform.localPosition.y);
        linterna.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dirLinterna.y,dirLinterna.x)* Mathf.Rad2Deg - 90));
        linterna.transform.localPosition = targetPosLinterna;
    }

    // Corrutina encargada de revisar si se debe o no disparar una bala primaria
    IEnumerator CheckShootBullet1st(){
        while(true){
            if(holdingShoot1st){
                if(apuntando){
                    if(canShoot1st){
                        StartCoroutine(coolDownShoot1st());
                        StartCoroutine(ShootingBullet1st());
                    }
                }
            }
            yield return null;
        }
    }

    // Corrutina encargada de revisar si se debe o no disparar una bala secundaria
    IEnumerator CheckShootBullet2nd(){
        while(true){
            if(holdingShoot2nd){
                if(apuntando){
                    if(canShoot2nd){
                        StartCoroutine(coolDownShoot2nd());
                        StartCoroutine(ShootingBullet2nd());
                    }
                }
            }
            yield return null;
        }
    }

    // Función encargada del coolDown entre disparos
    IEnumerator coolDownShoot1st(){
        canShoot1st = false;
        float delay = stats.getBulletCoolDown();
        //Debug.Log(delay);
        //Debug.Log("Se disparó...recargando");
        yield return new WaitForSeconds(delay);
        //Debug.Log("Se puede volver a disparar");
        canShoot1st = true;
    }

    // Función encargada del coolDown entre disparos
    IEnumerator coolDownShoot2nd(){
        canShoot2nd = false;
        float delay = stats.getBulletCoolDown2nd();
        //Debug.Log(delay);
        //Debug.Log("Se disparó...recargando");
        yield return new WaitForSeconds(delay);
        //Debug.Log("Se puede volver a disparar");
        canShoot2nd = true;
    }

    // Función encargada de disparar el proyectil
    IEnumerator ShootingBullet1st(){
        Vector2 dirBullet = new Vector2(mira.transform.localPosition.x,mira.transform.localPosition.y).normalized;
        Vector2 posicion = new Vector2(transform.position.x,transform.position.y);

        GameObject bulletIns = Instantiate(bullet,posicion+(dirBullet*0.6f),transform.rotation);
            
        bulletIns.GetComponent<Rigidbody2D>().velocity = dirBullet*potenciaTiro;
        bulletIns.GetComponent<BulletCompor>().setBulletDamage(stats.getBulletDamage());
        bulletIns.GetComponent<BulletCompor>().setBulletOwner(this.gameObject);

        bulletIns.transform.Rotate(0f,0f,Mathf.Atan2(dirBullet.y,dirBullet.x)* Mathf.Rad2Deg);
        yield return null;
    }

    // Función encargada de disparar el proyectil
    IEnumerator ShootingBullet2nd(){
        Vector2 dirBullet = new Vector2(mira.transform.localPosition.x,mira.transform.localPosition.y).normalized;
        Vector2 posicion = new Vector2(transform.position.x,transform.position.y);

        GameObject bulletIns = Instantiate(bulletEmpuje,posicion+(dirBullet*0.6f),transform.rotation);
            
        bulletIns.GetComponent<Rigidbody2D>().velocity = dirBullet*potenciaTiro;
        bulletIns.GetComponent<BulletEmpuje>().setBulletDamage(stats.getBulletEmpujeDamage());
        bulletIns.GetComponent<BulletEmpuje>().setRadioEmpuje(stats.getRadioEmpuje());
        bulletIns.GetComponent<BulletEmpuje>().setBulletOwner(this.gameObject);

        bulletIns.transform.Rotate(0f,0f,Mathf.Atan2(dirBullet.y,dirBullet.x)* Mathf.Rad2Deg);
        yield return null;
    }

    // Función encargada de apuntar el arma y ajustar la mira
    private void ApuntarArma(){
        if(!apuntando){
            if (movAnim != null && movAnim.isActiveAndEnabled)
            {
                apuntando = true;
                movAnim.SetBool("Apuntando",apuntando);
                mira.GetComponent<SpriteRenderer>().enabled = true;
                linterna.SetActive(true);
            }
        } else{
            if (movAnim != null && movAnim.isActiveAndEnabled)
            {
                apuntando = false;
                movAnim.SetBool("Apuntando",apuntando);
                mira.GetComponent<SpriteRenderer>().enabled = false;
                linterna.SetActive(false);
            }
        }
        //Debug.Log("Ejecutado");
    }

    // Función que retorna el estado actual de apuntando
    public bool getApuntandoState(){
        return apuntando;
    }


    // Función que forzosamente desactiva el estado del apuntado del PJ
    public void desApuntar(){
        if(apuntando){
            ApuntarArma();
        }
    }

    // Función encargada desactivar la linterna del Player
    public void setLinterna(bool value){
        linterna.SetActive(value);
    }

    // Función encargada de setear el valor de haveWeapon
    public void setHaveWeapon(bool value){
        haveWeapon = value;
    }

}
