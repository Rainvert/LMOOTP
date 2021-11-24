using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    // Variables necesarias

    public delegate void OnUpdate();
    public event OnUpdate onUpdateCash; 

    [SerializeField] private UIStatsInGame statsUI;

    [SerializeField] private int ID = 0;
    [SerializeField] private float playerSpeed;

    [SerializeField] private Light2D luzReferencial;

    [SerializeField] private GameObject hitSound;    
    [SerializeField] private GameObject soundDeath;
    [SerializeField] private GameObject stimPackSound;

    [SerializeField] private int botiquines;
    [SerializeField] private int maxBotiquines;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerMaxHealth;

    [SerializeField] private float playerBulletDamage;

    [SerializeField] private float playerBulletEmpujeDamage;
    [SerializeField] private float BulletEmpujeRadio;

    [SerializeField] private float playerCoolDownDash;

    [SerializeField] private float playerCoolDownDisparo1st,playerCoolDownDisparo2nd;
    [SerializeField] private float limitCoolDown;
    [SerializeField] private float limitVelocidad;

    [SerializeField] private GameObject lifeSlide;

    [SerializeField] private CanvasGroup UIplayer;

    [SerializeField] private Text dineroJugador;

    [SerializeField] private GameObject textDaño;

    [SerializeField] private GameObject DeathPlayer;

    [SerializeField] private int cashPlayer;

    private GameObject gameManager;

    private bool needRestartWorld;

    private bool needUI;

    void Start(){
        lifeSlide.GetComponent<VidaUI>().setMaxVida(playerMaxHealth);
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        StartCoroutine(checkForRestart());
        updateCash();
        needRestartWorld = false;
        botiquines = 0;
        checkNeedUI();
        updateUIStats();
    }

    // Función que instancia las estadísticas iniciales
    private void updateUIStats(){
        statsUI.setDañoTotal(playerBulletDamage);
        statsUI.setShootDelay(playerCoolDownDisparo1st);
        statsUI.setEmpuje(BulletEmpujeRadio);
        statsUI.setEmpujeDelay(playerCoolDownDisparo2nd);
        statsUI.setSpeed(playerSpeed);
        statsUI.setBotiquines(botiquines,maxBotiquines);
    }

    // Función que permite modificar el valor de los botiquines
    public bool changeCtdaBotiquines(int value){
        int tempValue = botiquines;
        tempValue += value;
        if(tempValue > maxBotiquines){
            return false;
        }
        if(tempValue < 0){
            return false;
        }
        botiquines = tempValue;
        statsUI.setBotiquines(botiquines,maxBotiquines);
        return true;

    }

    // Función que utiliza un botiquín y cura al jugador
    public void useBotiquin(){
        if(botiquines>0 && playerHealth!=playerMaxHealth){
            
            GameObject healSound = Instantiate(stimPackSound,transform.position,transform.rotation);
            healSound.GetComponent<AudioSource>().Play();
            Destroy(healSound,1f);

            changeCtdaBotiquines(-1);
            plusHealth(0.3f);
            Debug.Log("botiquin usado");
        }else{
            Debug.Log("No quedan botiquines o tiene la vida máxima");
        }
    }

    // Función encargada de restar dinero a la cantidad actual del dinero del jugador
    public void lessCashPlayer(int value){
        cashPlayer -= value;
        updateCash();
        if(onUpdateCash!= null){
            onUpdateCash();
            Debug.Log("Evento de cambio de dinero Activado");
        }
    }

    // Función encargada de devolver la cantidad actual de dinero del jugador
    public int getCashPlayer(){
        return cashPlayer;
    }

    // Función encargada de cambiar el color al life slide
    public void setColorLife(Color value){
        lifeSlide.GetComponentInChildren<Image>().color = value; 
    }

    // Función encargada de obtener el color del player
    public Color getColorLife(){
        return lifeSlide.GetComponentInChildren<Image>().color;
    }

    // Función encargadad de cambiar el valor de mostrar la UI
    public void setNeedUI(bool value){
        needUI = value;
        checkNeedUI();
    }

    // Función que permite aumentar la cantidad que el player tiene de dinero
    public void plusCashPlayer(int value){
        cashPlayer += value;
        updateCash();
        if(onUpdateCash!= null){
            onUpdateCash();
        }
    }

    // Función encargada de aumentar la vida máxima en un % X
    public bool plusMaxHealth(float value){
        bool aplicado = true;
        
        playerMaxHealth += playerMaxHealth*value;
        lifeSlide.GetComponent<VidaUI>().setMaxVida(playerMaxHealth);
        return aplicado;
    }

    // Función que retorna la máxima salud
    public float getMaxHealth(){
        return playerHealth;
    }

    // Función que retorna la vida actual del personaje
    public float getHealth(){
        return playerHealth;
    }

    // Función que aumenta la vida actual del personaje en un % X de la vida máxima
    public bool plusHealth(float value){
        bool aplicado = true;
        if(playerHealth == playerMaxHealth){
            aplicado = false;
        }

        playerHealth += playerMaxHealth*value;

        if(playerHealth>playerMaxHealth){
            playerHealth = playerMaxHealth;
        }
        lifeSlide.GetComponent<VidaUI>().setVida(playerHealth);
        return aplicado;
    }

    // Función encargada de retornar el valor actual del Cooldown para disparar
    public float getBulletCoolDown(){
        return playerCoolDownDisparo1st;
    }
    
    // Función encargada de retornar el valor actual del Cooldown para disparo secundario
    public float getBulletCoolDown2nd(){
        return playerCoolDownDisparo2nd;
    }
    

    // Función encargada de disminuir el valor actual del Cooldown en un % X
    public bool lessBulletCoolDown(float value){
        bool aplicado = true;
        
        if(playerCoolDownDisparo1st == limitCoolDown){
            aplicado = false;
        }

        playerCoolDownDisparo1st -= playerCoolDownDisparo1st*value;
        
        if(playerCoolDownDisparo1st<=limitCoolDown){
            playerCoolDownDisparo1st = limitCoolDown;
        }

        statsUI.setShootDelay(playerCoolDownDisparo1st);

        return aplicado;
    }

    // Función encargada de disminuir el valor actual del Cooldown para el 2do disparo en un % X
    public bool lessBulletCoolDown2nd(float value){
        bool aplicado = true;
        
        if(playerCoolDownDisparo2nd == limitCoolDown){
            aplicado = false;
        }

        playerCoolDownDisparo2nd -= playerCoolDownDisparo2nd*value;
        
        if(playerCoolDownDisparo2nd<=limitCoolDown){
            playerCoolDownDisparo2nd = limitCoolDown;
        }
        
        statsUI.setEmpujeDelay(playerCoolDownDisparo2nd);

        return aplicado;
    }

    // Función que permite aumentar el tamaño del radio de empuje en un % X
    public bool addRadioEmpuje(float value){
        BulletEmpujeRadio += BulletEmpujeRadio*value;

        statsUI.setEmpuje(BulletEmpujeRadio);
        return true;
    }

    // Función que permite obtener el tamaño del radio de empuje de la bala
    public float getRadioEmpuje(){
        return BulletEmpujeRadio;
    }


    // Función encargada de aumentar la velocidad en un % X
    public bool plusSpeed(float value){
        bool aplicado = true;
        
        if(playerSpeed == limitVelocidad){
            aplicado = false;
        }

        playerSpeed += playerSpeed*value;

        if(playerSpeed>=limitVelocidad){
            playerSpeed = limitVelocidad;
        }

        statsUI.setSpeed(playerSpeed);

        GetComponent<Movimiento>().updateSpeed();
        return aplicado;
    }

    // Función que retorna la velocidad actual del personaje
    public float getSpeed(){
        return playerSpeed;
    }

    // Función encargada de aumentar el daño base de los proyectiles en un % X
    public bool plusDamage(float value){
        bool aplicado = true;
        playerBulletDamage += playerBulletDamage*value;

        statsUI.setDañoTotal(playerBulletDamage);

        return aplicado;
    }

    // Función que retorna el daño actual de los proyectiles primarios del player
    public float getBulletDamage(){
        return playerBulletDamage;
    }

    // Función que retorna el daño actual de los proyectiles secundarios del player
    public float getBulletEmpujeDamage(){
        return playerBulletEmpujeDamage;
    }

   // Función encargada de dañar al personaje
    public void damageHealth(float value){
        playerHealth -= value;

        Camera.main.GetComponent<FollowCamZoom>().shakeCamCustomPower(0.2f,0.1f);

        GameObject hitInstance = Instantiate(hitSound,transform.position,transform.rotation);
        hitInstance.GetComponent<AudioSource>().Play();
        Destroy(hitInstance,1f);

        // Se instancia texto de Daño
        GameObject text = Instantiate(textDaño,transform.position,transform.rotation);
        text.GetComponent<TextMesh>().color = Color.red;
        text.GetComponent<TextMesh>().text = value.ToString();


        lifeSlide.GetComponent<VidaUI>().setVida(playerHealth);

        if(isAlive()){
            StartCoroutine(reboundHit());
        } else{

            GameObject deathSound = Instantiate(soundDeath,transform.position,transform.rotation);
            deathSound.GetComponent<AudioSource>().Play();
            Destroy(deathSound,3f);

            // Se instancia un sprite de muerte
            GameObject deathInst = Instantiate(DeathPlayer,transform.position,transform.rotation);
            Destroy(deathInst,20f);

            turnOffPJ();
        }
    }

    // Función encargada de revisar si el jugador sigue vivo
    private bool isAlive(){
        if(playerHealth > 0){
            return true;
        } else{
            return false;
        }
    }

    // Función que permite revisar externamente si el jugador sigue vivo
    public bool isPlayerAlive(){
        if(playerHealth > 0){
            return true;
        } else{
            return false;
        }
    }

    // Función que genera un rebound en el personaje
    IEnumerator reboundHit(){
        GetComponent<SpriteRenderer>().color = new Color(1f,0.35f,0.35f,1f);
        
        //GetComponent<Movimiento>().setCanMove(false);
        GetComponent<Animator>().SetBool("Golpeado",true);
        
        float[] valores = {-2f,-0.5f,0.5f,2f};
        Vector2 randomDirection = randomVector2Direct(valores);
        GetComponent<Rigidbody2D>().AddForce(randomDirection,ForceMode2D.Impulse);
        
        yield return new WaitForSeconds(0.3f);
        
        GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        GetComponent<Animator>().SetBool("Golpeado",false);
        //GetComponent<Movimiento>().setCanMove(true);
    }

    // Función que multiplica las componentes por -1
    private Vector2 randomVector2Direct(float[] values){
        
        Vector2 direction;
        int random;

        random = Random.Range(0,4);
        direction.x = values[random];
        random = Random.Range(0,4);
        direction.y = values[random];

        return direction;
    }

    // Funciones encargadas de la activación y desactivación de los controles
    public void OnEnable(){
        GetComponent<PlayerInput>().enabled = true;
    }

    public void OnDisable(){
        GetComponent<PlayerInput>().enabled = false;
    }

    // Función encargada de desactivar por completo al PJ
    public void turnOffPJ(){
        // Se deshabilita el personaje Layer 6 - PJ Atacable | 0 = Default
        this.gameObject.layer = 0;
        
        GetComponent<SpriteRenderer>().enabled = false;
        //GetComponent<PlayerInput>().actions.Disable();

        UIplayer.alpha = 0f;
        luzReferencial.intensity = 0f;

        Camera.main.GetComponent<FollowCamZoom>().removePlayerFollowCam(transform);

        GetComponent<Movimiento>().setCanMove(false);
        GetComponent<Movimiento>().setLinterna(false);
        
        GetComponent<Apuntado>().desApuntar();
        GetComponent<Apuntado>().setLinterna(false);
        GetComponent<Apuntado>().setHaveWeapon(false);
        GetComponent<ChangeControl>().setCanAction(false);
        GetComponent<ChangeControl>().setCanSeeStats(false);
        GetComponent<ChangeControl>().showUIStatsExterno();
        GetComponent<ChangeControl>().closeAnyShop();

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

    }

    // Función encargada de  reiniciar o activar por completo al PJ
    public void restartOnPJ(){
        // Se deshabilita el personaje Layer 10 - Jugador | 0 = Default
        this.gameObject.layer = 10;
        GetComponent<SpriteRenderer>().enabled = true;
        //GetComponent<PlayerInput>().actions.Enable();
        
        UIplayer.alpha = 1f;
        luzReferencial.intensity = 0.5f;

        Camera.main.GetComponent<FollowCamZoom>().addPlayerFollowCam(transform);

        GetComponent<Apuntado>().desApuntar();
        GetComponent<Movimiento>().setCanMove(true);
        GetComponent<Movimiento>().setLinterna(true);
        GetComponent<Apuntado>().setHaveWeapon(true);
        GetComponent<ChangeControl>().setCanAction(true);
        GetComponent<ChangeControl>().setCanSeeStats(true);

        playerHealth = playerMaxHealth;
        lifeSlide.GetComponent<VidaUI>().setVida(playerHealth);
    }

    // Función que permite obtener el ID del jugador
    public int getIDPlayer(){
        return ID;
    }

    // Función que permite setear el ID del jugador
    public void setIDPlayer(int value){
        ID = value;
    }

    // Función que envía mensaje acerca de querer reiniciar 
    public void solicitarReinicio(InputAction.CallbackContext context){
        if(context.performed){
            needRestartWorld = true;
            Debug.Log("reincio solicitado");
        }
    }

    // Corrutina encargada de revisar si se llamó o no a un reinicio
    IEnumerator checkForRestart(){
        while(true){
            if(needRestartWorld){
                gameManager.GetComponent<GameplayManager>().restartPartida();
                needRestartWorld = false;
            }
            yield return null;
        }
    }

    // Función encargada de actualizar el UI de dinero del jugador
    private void updateCash(){
        dineroJugador.text = "$ "+cashPlayer;
    }

    // Función que oculta la UI del player
    private void checkNeedUI(){
        if(needUI){
            UIplayer.alpha = 1f;
        } else{
            UIplayer.alpha = 0f;
        }
    }
}
