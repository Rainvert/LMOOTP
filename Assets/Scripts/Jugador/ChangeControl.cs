using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Experimental.Rendering.Universal;

public class ChangeControl : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private bool rangeOfShop,rangeOfBuild;
    [SerializeField] private ShopArea shopSpace;

    [SerializeField] private PauseOptions menuPause = null;
    [SerializeField] private NucleoStats nucleoRef;

    [SerializeField] private GameObject placeHolderBuilder;
    [SerializeField] private bool shopIsOpen, buildIsOpen, inConstruction;

    [SerializeField] private bool canActivate,canStartRonda;

    private Color actNucleoColor = new Color(0.75f,0.55f,0.95f);

    private bool inAccion = false;
    private bool inChange = false;
    private bool inPause = false;
    private bool canAction;

    [SerializeField] private string actualesAcciones;
    [SerializeField] private bool canSeeStats;
    private bool isUIStatsOpen= false;
    private string pausaActions;

    public void Start(){
        rangeOfShop = false;
        rangeOfBuild = false;
        shopIsOpen = false;
        buildIsOpen = false;
        canAction = true;
        canStartRonda = false;
        playerInput = this.GetComponent<PlayerInput>();
        menuPause = FindObjectOfType<PauseOptions>();
    }
    
    // Función que setea el valor de poder o no ver las estadísticas
    public void setCanSeeStats(bool value){
        canSeeStats = value;
    }

    // Función que permite obtener un valor de verdad cuando alguna tienda está abierta
    public bool isAnyShopOpen(){
        if(shopIsOpen || buildIsOpen){
            return true;
        }
        return false;
    }

    // Función que permite cambiar el valor de poder o no poder hacer una acción
    public void setCanAction(bool value){
        canAction = value;
    }

    // Función que permite cambiar el valor de poder o no poder activar una ronda del modo endless
    public void setCanStartRonda(bool value){
        canStartRonda = value;
    }

    // Función que permite cambiar el color de activacion del Nucleo
    public void setActNucleoColor(Color value){
        actNucleoColor = value;
    }

    // Función que se activa al presionar el control asociado para activar la tienda
    public void onShopChange(InputAction.CallbackContext context){
        if(context.performed && !inChange && canAction){
            changeCurrentMap("Shop");
        }
    }

    // Función que permite utilizar un botiquín del inventario
    public void onBandageUse(InputAction.CallbackContext context){
        if(context.performed && canSeeStats){
            useBandage();
        }
    }

    // Función que utiliza un botiquín del inventario
    private void useBandage(){
        GetComponent<PlayerStats>().useBotiquin();
    }

    // Función que se activa al presionar el control asociado para activar la construcción
    public void onBuildChange(InputAction.CallbackContext context){
        if(context.performed && !inChange && canAction){
            changeCurrentMap("Build");
        }
    }

    // Función que se activa al presionar el control asociado para cancelar una construcción
    public void onBuildCancel(InputAction.CallbackContext context){
        if(context.performed && !inChange && canAction){
            changeCurrentMap("CancelBuild");
        }
    }

    // Función que verifica si se presiona el botón de acción
    public void onAccion(InputAction.CallbackContext context){
        if(context.performed && !inAccion && canAction){
            StartCoroutine(accionHabilitada());
            if(canActivate){
                activarNucleo();
            }
            if(canStartRonda){
                activarRonda();
            }
        }
    }

    // Función que detecta si se necesita mostrar u ocultar la UI de estadísticas
    public void onUIStats(InputAction.CallbackContext context){
        if(context.performed && canSeeStats){
            showUIStats();
        }
    }

    // Función que muestra u oculta la UIStats
    private void showUIStats(){
        if(!isUIStatsOpen){
            isUIStatsOpen = true;
            GetComponentInChildren<UIStatsInGame>().showUI(isUIStatsOpen);
        } else{
            isUIStatsOpen = false;
            GetComponentInChildren<UIStatsInGame>().showUI(isUIStatsOpen);
        }
    }

    // Función que muestra u oculta las UIStats de manera externa
    public void showUIStatsExterno(){
        if(isUIStatsOpen){
            showUIStats();
        }
    }

    // Función que verificar si se presiona el botón de Pause
    public void onPausar(InputAction.CallbackContext context){
        if(context.performed && !inPause && !shopIsOpen && !buildIsOpen){
            StartCoroutine(esperarPausa());
            pausarJuego();
        }
    }

    // Función que cierra las UI de manera externa en caso de estar abiertas
    public void closeAnyShop(){
        Debug.Log("force close UI By Death or Leave Area");
        if(shopIsOpen){
            cambiodeControlShop();
        }
        if(buildIsOpen && !inConstruction){
            cambiodeControlBuild();
        }
        if(buildIsOpen && inConstruction){
            activateConstructionMode();
            cambiodeControlBuild();
        }
    }

    // Función que cierra las UI en caso de estar con teclado al apretar pausa como medio alternativo
    public void onCloseUI(InputAction.CallbackContext context){
        if(context.performed && !inPause){
            Debug.Log("force close UI");
            if(shopIsOpen){
                changeCurrentMap("Shop");
            }
            if(buildIsOpen && !inConstruction){
                changeCurrentMap("Build");
            }
            if(buildIsOpen && inConstruction){
                changeCurrentMap("CancelBuild");
                Debug.Log("trying to close");
            }
        }
    }

    // Función que bloquea o desblquea los controles a petición
    public void blockControls(bool value){
        if(value){
            actualesAcciones = playerInput.currentActionMap.name;
            playerInput.SwitchCurrentActionMap("NoControles");
        } else{
            playerInput.SwitchCurrentActionMap(actualesAcciones);
        }
    }

    // Función encargada de pausar el juego
    private void pausarJuego(){
        if(menuPause!=null){
            if(!menuPause.getInPause()){
                Debug.Log("Player "+GetComponent<PlayerStats>().getIDPlayer()+" En pausa");
                pausaActions = playerInput.currentActionMap.name;
                playerInput.SwitchCurrentActionMap("UI");

                menuPause.pauseMenuActive(this.gameObject);
            }
        }
    }

    // Función que resume la pausa de juego
    public void resumeGameControls(){
        Debug.Log("Player "+GetComponent<PlayerStats>().getIDPlayer()+" Desactivó pausa");
        playerInput.SwitchCurrentActionMap(pausaActions);
    }

    // Corrutina que controla que solo se inicie una sola pausa
    IEnumerator esperarPausa(){
        inPause = true;
        yield return new WaitForSeconds(0.2f);
        inPause = false;
    }

    // Función encargada de activar el núcleo
    private void activarNucleo(){
        nucleoRef.GetComponent<NucleoStats>().setActivated(true);
        nucleoRef.GetComponent<SpriteRenderer>().color = actNucleoColor;
        nucleoRef.transform.parent.GetComponentInChildren<Light2D>().color = actNucleoColor;
    }

    // Función encargada de activar la ronda antes de que termine el tiempo
    private void activarRonda(){
        FindObjectOfType<GameplayManager>().setRondaGoing(true);
        FindObjectOfType<GameplayManager>().setInEndless(true);
    }

    // Corrutina que se encarga de decir que el player activó el botón acción
    IEnumerator accionHabilitada(){
        inAccion = true;
        Debug.Log("Se apretó la acción");
        yield return new WaitForSeconds(0.2f);
        inAccion = false;
        
    }

    // Función que controla el cambio de mapas de control para evitar moverse mientras se controlan las UI para cada player
    private void changeCurrentMap(string tipo){
        if(menuPause!= null && menuPause.getInPause()){
            return;
        }
        if(tipo == "Shop"){
            if(!inChange && rangeOfShop && !buildIsOpen){
                StartCoroutine(esperarTiempo());
                cambiodeControlShop();
            }
            //Debug.Log("Ejecutado");
        } 
        if(tipo == "Build"){
            if(!inChange && rangeOfBuild && !shopIsOpen){
                StartCoroutine(esperarTiempo());
                cambiodeControlBuild();
            }
        }
        if(tipo == "CancelBuild"){
            if(!inChange && buildIsOpen){
                StartCoroutine(esperarTiempo());
                activateConstructionMode();
            }
        }
    }

    // Corrutina que evitar que las funciones de activación de cambio de mapa se activen multiples veces
    IEnumerator esperarTiempo(){
        inChange = true;
        yield return new WaitForSeconds(0.2f);
        inChange = false;
    }

    // Función encargada directamente de realizar el cambio de los action map para movimiento o UI
    private void cambiodeControlShop(){
        if(playerInput.currentActionMap.name == "PlayerActions"){
            //Debug.Log("Cambio de control a UI");
            playerInput.SwitchCurrentActionMap("UI");
            shopIsOpen = true;
            shopSpace.OpenShop(GetComponent<PlayerStats>().getIDPlayer()-1,true,buildIsOpen, this.transform);

        } else{
            //Debug.Log("estamos con control a Moverse"); 
            playerInput.SwitchCurrentActionMap("PlayerActions");
            shopIsOpen = false;
            shopSpace.OpenShop(GetComponent<PlayerStats>().getIDPlayer()-1,false,buildIsOpen, this.transform);
        }
    }

    // Función encargada directamente de realizar el cambio de los action map para movimiento o UI
    private void cambiodeControlBuild(){
        if(playerInput.currentActionMap.name == "PlayerActions"){
            Debug.Log("Cambio de control a UI");
            playerInput.SwitchCurrentActionMap("UI");
            buildIsOpen = true;
            shopSpace.OpenBuild(GetComponent<PlayerStats>().getIDPlayer()-1,true,shopIsOpen, this.transform);

        } else{
            Debug.Log("estamos con control a Moverse"); 
            playerInput.SwitchCurrentActionMap("PlayerActions");
            buildIsOpen = false;
            shopSpace.OpenBuild(GetComponent<PlayerStats>().getIDPlayer()-1,false,shopIsOpen, this.transform);
        }
    }   
    
    // Función encargada directamente de realizar el cambio de los action map para construcción 
    public void activateConstructionMode(){
        if(playerInput.currentActionMap.name == "UI"){
            Debug.Log("Cambio de control a Construction Mode");
            inConstruction = true;
            playerInput.SwitchCurrentActionMap("Construction");
            shopSpace.HideBuild(GetComponent<PlayerStats>().getIDPlayer()-1,true, this.transform);
            //shopSpace.OpenBuild(GetComponent<PlayerStats>().getIDPlayer()-1,true, this.transform);

        } else{
            Debug.Log("Cambio de control a UI desde Construcción"); 
            playerInput.SwitchCurrentActionMap("UI");
            inConstruction = false;
            Destroy(placeHolderBuilder);
            placeHolderBuilder = null;
            shopSpace.HideBuild(GetComponent<PlayerStats>().getIDPlayer()-1,false, this.transform);
            //shopSpace.OpenBuild(GetComponent<PlayerStats>().getIDPlayer()-1,false, this.transform);
        }
    }

    // Función que permite cambiar la posibilidad de abrir la tienda y el objeto mismo en la que el jugador está 
    public void setRangeOfShop(bool value, ShopArea shop){
        rangeOfShop = value;
        shopSpace = shop;
    }

    // Función que permite cambiar la posibilidad de abrir la construcción y el objeto mismo en la que el jugador está 
    public void setRangeOfBuild(bool value, ShopArea shop){
        rangeOfBuild = value;
        shopSpace = shop;
    }

    // Función que permite cambiar el valor para permitir o no la activación del núcleo
    public void setCanActivate(bool value, NucleoStats nucleo){
        canActivate = value;
        nucleoRef = nucleo;
    }

    // Función que permite setear el placeholder de edificaciones del player
    public void setPlaceHolderBuilder(GameObject holder){
        placeHolderBuilder = holder;
    }

}
