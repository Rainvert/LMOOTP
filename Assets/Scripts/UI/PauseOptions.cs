using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class PauseOptions : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<GameObject> uiShops;
    [SerializeField] private List<GameObject> players;

    [SerializeField] private CanvasGroup menuGeneral;
    [SerializeField] private CanvasGroup menuAjustes;

    [SerializeField] private int indexPlayer;
    [SerializeField] private GameObject playerGO;

    [SerializeField] private GameObject firstButtonPausa;

    [SerializeField] private GameObject firstButtonConfig;

    [SerializeField] private GameObject prevFB;
    [SerializeField] private GameObject prevRoot;

    [SerializeField] private string deviceInPause;

    private bool shopWasOpen;
    private bool inPause;

    void Start()
    {
        // Se desactiva el menu de pausa
        menuGeneral.alpha = 0f;
        menuGeneral.interactable = false;
        menuGeneral.blocksRaycasts = false;        
    
        menuAjustes.alpha = 0f;
        menuAjustes.interactable = false;
        menuAjustes.blocksRaycasts = false; 
    }

    // Función que permite obtener el estado del menu
    public bool getInPause(){
        return inPause;
    }

    // Función que permite obtener el dispositivo que pidió la pause
    public string getInPauseDevice(){
        return deviceInPause;
    }

    // Función encargada de activar el menú de pausa
    public void pauseMenuActive(GameObject player){

        deviceInPause = player.GetComponent<Apuntado>().getDevice();
        shopWasOpen = player.GetComponent<ChangeControl>().isAnyShopOpen();

        if(deviceInPause == "Teclado"){
            CursorConfig cursor = FindObjectOfType<CursorConfig>();
            cursor.desconfinarCursor();
            cursor.mostrarCursor(true);
        }

        inPause = true;

        indexPlayer = player.GetComponent<PlayerStats>().getIDPlayer()-1;
        playerGO = player;
        
        for (int i = 0; i < players.Count; i++)
        {
            if(indexPlayer != i){
                players[i].GetComponent<ChangeControl>().blockControls(true);
            }
        }

        // Se activa el menu de pausa
        menuGeneral.alpha = 1f;
        menuGeneral.interactable = true;
        menuGeneral.blocksRaycasts = true;    

        // Se asigna el usuario correspondiente la UI
        MultiplayerEventSystem multi =  uiShops[indexPlayer].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();

        prevFB = multi.currentSelectedGameObject;
        prevRoot = multi.playerRoot;

        multi.playerRoot = null;
        multi.playerRoot = this.gameObject;
        multi.SetSelectedGameObject(firstButtonPausa);

        // Pausar tiempo de Juego
        Time.timeScale = 0f;
    }


    // Función encargada de desactivar el menú de pausa
    public void pauseMenuDeActive(){

        if(deviceInPause == "Teclado" && !shopWasOpen){
            CursorConfig cursor = FindObjectOfType<CursorConfig>();
            cursor.confinarCursor();
            cursor.mostrarCursor(false);
        }

        inPause = false;
        
        // Se desactiva el menu de pausa
        menuGeneral.alpha = 0f;
        menuGeneral.interactable = false;
        menuGeneral.blocksRaycasts = false;    

        // Se asigna el usuario correspondiente la UI
        MultiplayerEventSystem multi =  uiShops[indexPlayer].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();

        multi.playerRoot = null;
        multi.playerRoot = prevRoot;
        multi.SetSelectedGameObject(prevFB);  

        for (int i = 0; i < players.Count; i++)
        {
            if(indexPlayer != i){
                players[i].GetComponent<ChangeControl>().blockControls(false);
            }
        }

        playerGO.GetComponent<ChangeControl>().resumeGameControls();

    }

    // Función encargada de abrir el menú de Ajustes
    public void ajustesMenuActive(){
        // Se desactiva el menú de pausa
        menuGeneral.alpha = 0f;
        menuGeneral.interactable = false;
        menuGeneral.blocksRaycasts = false;    

        // Se activa el menú de ajustes
        menuAjustes.alpha = 1f;
        menuAjustes.interactable = true;
        menuAjustes.blocksRaycasts = true;    

        // Se asigna el usuario correspondiente la UI
        MultiplayerEventSystem multi =  uiShops[indexPlayer].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();

        multi.playerRoot = null;
        multi.playerRoot = this.gameObject;
        multi.SetSelectedGameObject(firstButtonConfig);

    }

    // Función encargada de cerrar el menú de Ajustes
    public void ajustesMenuDeactive(){
        // Se desactiva el menú de ajustes
        menuAjustes.alpha = 0f;
        menuAjustes.interactable = false;
        menuAjustes.blocksRaycasts = false;  

        // Se activa el menú de pausa
        menuGeneral.alpha = 1f;
        menuGeneral.interactable = true;
        menuGeneral.blocksRaycasts = true;    
  

        // Se asigna el usuario correspondiente la UI
        MultiplayerEventSystem multi =  uiShops[indexPlayer].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();

        multi.playerRoot = null;
        multi.playerRoot = this.gameObject;
        multi.SetSelectedGameObject(firstButtonPausa);

    }

    // Función que asigna la lista de uiShops de los jugadores activos
    public void setUIShops(List<GameObject> tiendasActivas){
        uiShops = tiendasActivas;
    }

    // Función que asigna la lista de jugadores activos
    public void setPlayersActive(List<GameObject> playersList){
        players = playersList;
    }

}
