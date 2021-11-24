using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuild : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject finalObj;
    [SerializeField] private Vector2 movPlayer;
    [SerializeField] private bool isMoving = false;
    [SerializeField] private float movSpeed;
    [SerializeField] private float costo;
    [SerializeField] private float modificador;

    [SerializeField] private LayerMask noConstruible;

    [SerializeField] private NavigationHandler navUI;

    [SerializeField] private bool puedeColocar;

    [SerializeField] private CustomButton botonOrigin;
    [SerializeField] private GameObject soundPlace;

    void Start(){
        StartCoroutine(esperarActivar());
    }

    // Corrutina que no permite construir inmediatamente para evitar que se coloque una pieza al activar el control
    IEnumerator esperarActivar(){
        puedeColocar = false;
        yield return new WaitForSeconds(0.2f);
        puedeColocar = true;
    }

    // Función que setea el botón de donde viene el item
    public void setButtonOrigin(CustomButton boton){
        botonOrigin = boton;
    }

    // Función que setea la UI para actualizar los costo
    public void setNavUI(NavigationHandler nav){
        navUI = nav;
    }

    // Función que setea el costo del objeto
    public void setCosto(float value, float modiffier){
        costo = value;
        modificador = modiffier;
    }

    // Función que setea el objeto final a colocar
    public void setFinalObj(GameObject objeto){
        finalObj = objeto;
        //GetComponent<SpriteRenderer>().sprite = finalObj.GetComponentInChildren<SpriteRenderer>().sprite;
    }

    // Función que setea al player correspondiente
    public void setPlayerGridMov(GameObject playerRef){
        player = playerRef;
    }

    void Update(){
        if(player != null){
            setMovGrid();
        }
        if(!puedeColocar || isMoving){
            player.GetComponent<GridPlayerController>().setColocar(false);
        }
        if(player.GetComponent<GridPlayerController>().getColocar()){
            player.GetComponent<GridPlayerController>().setColocar(false);
            colocarItem();
        } 
    }

    // Función encargada de colocar el item en el lugar solicitado
    private void colocarItem(){
        bool somethingInplace = Physics2D.OverlapBox(transform.position,Vector2.one*0.5f,0f,noConstruible);
        Collider2D col = Physics2D.OverlapBox(transform.position,Vector2.one*0.5f,0f,noConstruible);
        if(!somethingInplace){
            if(costo<=player.GetComponent<PlayerStats>().getCashPlayer()){
                GameObject construccion = Instantiate(finalObj,transform.position,transform.rotation);
                construccion.GetComponentInChildren<TorretaCompor>().setOwner(player);
                
                player.GetComponent<PlayerStats>().lessCashPlayer((int) costo);
                navUI.updateMoneyUI();
                botonOrigin.changeMoneyCostButton(modificador);
                setCosto(botonOrigin.getMoneyModCostButton(),modificador);

                GameObject sonido = Instantiate(soundPlace,transform.position,transform.rotation);
                Destroy(sonido,1f);

            } else{
                //Debug.Log("Player no tiene suficiente dinero para construir");
            }
        }

    }

    // Función encargada de mover el objeto en función del dispositivo que se esté manejando
    private void setMovGrid(){
        if(player.GetComponent<Apuntado>().getDevice() == "Teclado"){
            movPlayer = Camera.main.ScreenToWorldPoint(player.GetComponent<GridPlayerController>().getGridMov());       
            transform.position = new Vector2(Mathf.Round(movPlayer.x),Mathf.Round(movPlayer.y));
        } else{
            movPlayer = player.GetComponent<GridPlayerController>().getGridMov();
            if(movPlayer != Vector2.zero && !isMoving){
                StartCoroutine(movWithEase());
            }
        }
    }

    // Corrutina que permite desplazar el cursor de manera controlada a través del Grid
    IEnumerator movWithEase(){
        isMoving = true;

        Vector2 nowPos = transform.position;
        Vector2 finalPos = nowPos + new Vector2(Mathf.Round(movPlayer.x),Mathf.Round(movPlayer.y)) ;

        float tiempoPasado = 0f;

        while(tiempoPasado < movSpeed){
            transform.position = Vector2.Lerp(nowPos,finalPos,tiempoPasado/movSpeed);
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        transform.position = finalPos;

        isMoving = false;
    }
}
