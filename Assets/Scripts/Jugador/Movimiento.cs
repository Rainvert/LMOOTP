using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movimiento : MonoBehaviour
{
    // Variables necesarias 
    [SerializeField] float velocidad,powerDash;
    [SerializeField] Vector2 velocidadVector;
    [SerializeField] Animator movAnim;
    [SerializeField] GameObject linterna;
    [SerializeField] float distanciaLinterna;

    [SerializeField] private float coolDownDashTime,coolDownBlockMove;
    [SerializeField] private bool haveWeapon;

    private Rigidbody2D rb_player;
    [SerializeField] private bool canDash,canDashMove,canMove;

    private Apuntado apuntandoState;

    private PlayerStats stats;
    private Vector2 direccion;

    // Función encargada de asignar elementos antes del Start
    void Awake(){
    }

    void Start(){
        apuntandoState = GetComponent<Apuntado>();
        stats = GetComponent<PlayerStats>();
        velocidad = stats.getSpeed();
        rb_player = GetComponent<Rigidbody2D>();
        linterna.SetActive(false);
        canDash = true;
        canDashMove = true;
        canMove = true;
    }

    // Función encargada de la llamada por el Input Handler
    public void onMove(InputAction.CallbackContext context){
        direccion = context.ReadValue<Vector2>();
    }
    public void onDash(InputAction.CallbackContext context){
        if(context.performed && haveWeapon){
            dashMov();
        }
    } 

    void Update(){
        if(canMove){
            Vector2 direccionMov = direccion;

            // Se activan las animaciones dependiendo del valor de movimiento
            movAnim.SetFloat("Horizontal",direccionMov.x);
            movAnim.SetFloat("Vertical",direccionMov.y);

            // Se mueve la lintera a la posicion y rotación correspondiente
            if(haveWeapon){
                moverLinterna(direccionMov);
            } else{
                linterna.SetActive(false);
            }
        }
    }

    // Función encargada de los movimientos relacionados a la física
    void FixedUpdate(){
        if(canMove && canDashMove){
            Vector2 direccionMov = direccion;
            // Se mueve al personaje según su velocidad
            if(direccionMov != Vector2.zero){
                setMovimiento(direccionMov);
                //Debug.Log("Moviendose!");
                movAnim.SetBool("Moving",true);
            } else{
                if(getMovimiento() != Vector2.zero){
                    setMovimiento(Vector2.zero);
                    //Debug.Log("Seteo de Velocidad a 0");
                    movAnim.SetBool("Moving",false);
                }
            }
            velocidadVector = rb_player.velocity;
        }
    }

    // Función encargada de setear el movimiento del Personaje
    private void setMovimiento(Vector2 direccion){
        rb_player.velocity = direccion * velocidad;
    }

    // Función que obtiene el valor actual de movimiento del personaje
    private Vector2 getMovimiento(){
        return rb_player.velocity;
    }

    // Función encargada de setear el valor de la velocidad
    private void setVelocidad(float value){
        velocidad = value;
    }

    // Función encargada de obtener el valor de la velocidad
    private float getVelocidad(){
        return velocidad;
    }

    // Función encargada de habilitar el Dash
    private void dashMov(){
        if(canDash && canMove){
            StartCoroutine(coolDownDash());
            StartCoroutine(coolDownDashMove());
            //StartCoroutine(dashTime());
            dashMove();
        }
    }

    // Función encargada del dash
    private void dashMove(){
        rb_player.AddForce(direccion.normalized*powerDash,ForceMode2D.Impulse);
    }

    // Corrutina encargada del dash
    IEnumerator dashTime(){
        float velocidadActual = getVelocidad();
        setVelocidad(velocidadActual+20f);
        yield return new WaitForSeconds(0.1f);
        setVelocidad(velocidadActual);
    }

    // Función encargada del cooldown del dash para el movimiento
    IEnumerator coolDownDash(){
        canDash = false;
        //Debug.Log("Under CoolDown");
        yield return new WaitForSeconds(coolDownDashTime);
        //Debug.Log("Cand Dash Again");
        canDash = true;
    }

    // Función encargada del cooldown del dash
    IEnumerator coolDownDashMove(){
        canDashMove = false;
        //Debug.Log("Under CoolDown");
        yield return new WaitForSeconds(coolDownBlockMove);
        //Debug.Log("Cand Dash Again");
        canDashMove = true;
    }

    // Función encargada de mover la linterna a la posición y rotación correspondiente
    private void moverLinterna(Vector2 direc){
        if(!apuntandoState.getApuntandoState()){

            linterna.SetActive(true);

            Vector2 dirLinterna = direc.normalized;
            dirLinterna *= distanciaLinterna;
            
            // Offset para la posicion Idle
            float offset = 0f;
            if(dirLinterna==Vector2.zero){
                offset = -90f;
            }

            // Se ubica la rotación de la linterna
            linterna.transform.rotation = Quaternion.Euler(new Vector3(0,0,Mathf.Atan2(dirLinterna.y,dirLinterna.x)* Mathf.Rad2Deg - 90 + offset));
            linterna.transform.localPosition = dirLinterna;

        } else{
            linterna.SetActive(false);
        }
    }

    // Función que actualiza la velocidad si recibe un cambio en el player
    public void updateSpeed(){
        velocidad = stats.getSpeed();
    }

    // Función que actualiza el estado de poder moverse
    public void setCanMove(bool value){
        canMove = value;
    }

    // Función que permite forzosamente prender/apagar la linterna de exploración
    public void setLinterna(bool value){
        linterna.SetActive(value);
    }

    // Función encargada de setear el valor de haveWeapon
    public void setHaveWeapon(bool value){
        haveWeapon = value;
    }
}
