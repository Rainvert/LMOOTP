using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerManager : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<GameObject> lightPosRef;
    [SerializeField] private List<GameObject> playersList;

    [SerializeField] private List<Color> playerColor;

    [SerializeField] private List<GameObject> uiplayersShop;
    [SerializeField] private List<GameObject> uiplayersBuild;

    [SerializeField] private FollowCamZoom camZoom;

    [SerializeField] private ShopArea shopArea;

    [SerializeField] private GameplayManager gameplayManager;
    [SerializeField] private EstadisticasManager statisticsManager;

    [SerializeField] private PauseOptions pauseMenu;
    [SerializeField] private int indexNextPlayer = 1;

    [SerializeField] private GameObject PreFabinputSystemUI;

    [SerializeField] private bool isfightMode, isTutorial;
    private int posIndex = 0;

    // Función encargada de resetear a los jugadores muertos a la partida
    public bool restartDeathPlayers(Vector2 respawnPos){
        bool allAlive = true;
        if(playersList.Count!= 0){
            foreach (var player in playersList){
                Debug.Log("Reapareciendo Jugadores Muertos");
                if(player.layer == 0){
                    player.GetComponent<PlayerStats>().restartOnPJ();
                    player.transform.position = respawnPos;
                    allAlive = false;
                }
            }
        }
        return allAlive;
    }

    // Función que se encarga de setear un ID al jugador entrante
    public void setIdentificationToPlayer(PlayerInput context){
        // Se obtienen todos los players existentes en escena
        GameObject [] players = GameObject.FindGameObjectsWithTag("Player");
        if(players!=null){
            for(int i=0;i<players.Length;i++){
                // Si el player no tiene ID, se le asigna 1
                if(players[i].GetComponent<PlayerStats>().getIDPlayer() == 0){

                    if(!isTutorial){
                        players[i].transform.position = transform.position;
                        players[i].GetComponent<ChangeControl>().setCanSeeStats(false);
                    } else{
                        SpawnAtCelda(players[i].transform,posIndex);
                        posIndex++;
                        if(posIndex == 3){
                            posIndex = 0;
                        }
                    }
                    
                    camZoom.addPlayerFollowCam(players[i].transform);

                    players[i].GetComponent<PlayerStats>().setIDPlayer(indexNextPlayer);
                    
                    players[i].GetComponent<PlayerStats>().setColorLife(playerColor[i]);
                    players[i].GetComponent<Apuntado>().setColorMira(playerColor[i]);

                    players[i].GetComponent<Estadisticas>().setIndex(indexNextPlayer);

                    if(isfightMode){
                        players[i].GetComponent<PlayerStats>().setNeedUI(true);
                        players[i].GetComponent<Apuntado>().setHaveWeapon(true);
                        players[i].GetComponent<Movimiento>().setHaveWeapon(true);
                        players[i].GetComponent<ChangeControl>().setCanSeeStats(true);
                    }

                    // Se instancian los sistemas para su UI
                    GameObject inputSystemUI = Instantiate(PreFabinputSystemUI,transform.position,transform.rotation);

                    // Se instancian las variables para las múltiples interfaces que puede poseer un player simultaneamente
                    NavigationHandler[] uiInside = inputSystemUI.GetComponentsInChildren<NavigationHandler>();
                    
                    for (int j = 0; j < uiInside.Length; j++)
                    {
                        // Se instancian las variables de la interfaz
                        uiInside[j].setGroupNum(indexNextPlayer);
                        uiInside[j].setOwnerUI(players[i].transform);
                        uiInside[j].defineButtonsToItems();
                        uiInside[j].defineButtonInteraction();

                        // Se activa el primer botón de la UI
                        //inputSystemUI.GetComponent<MultiplayerEventSystem>().firstSelectedGameObject = uiInside[j].getFirstButton();

                        // Se desactiva la UI para que no aparezca al spawnear el PJ
                        uiInside[j].gameObject.GetComponent<CanvasGroup>().interactable = false;
                        uiInside[j].gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
                        uiInside[j].gameObject.GetComponent<CanvasGroup>().alpha = 0f;                     
                    }

                    // Se asignan los elementos al player input para gestionar controles y Raycast
                    players[i].GetComponent<PlayerInput>().uiInputModule = inputSystemUI.GetComponent<InputSystemUIInputModule>();
                    players[i].GetComponent<PlayerInput>().actions.actionMaps[1].Disable();
                    players[i].GetComponent<PlayerInput>().actions.actionMaps[2].Disable();

                    indexNextPlayer += 1;
                } else{
                    Debug.Log("Id Player ya conectado: "+ players[i].GetComponent<PlayerStats>().getIDPlayer());
                }
                // Se asigna el control que el player está utilizando
                if(players[i].GetComponent<Apuntado>().getDevice() == "None"){
                    players[i].GetComponent<Apuntado>().setDevice(context.currentControlScheme);
                }
            }
        }

        // Se obtienen todas las UI existentes en escena
        GameObject [] uisShop = GameObject.FindGameObjectsWithTag("UIPlayer");
        GameObject [] uisBuild = GameObject.FindGameObjectsWithTag("UIBuild");


        // Se actualizan todas las listas y elementos que requiere de players y UI
        uiplayersShop = new List<GameObject>(uisShop);
        uiplayersBuild = new List<GameObject>(uisBuild);
        playersList = new List<GameObject>(players);
        
        //camZoom.updateListT(playersList);
        if(shopArea != null){
            shopArea.updatePlayersShop(uiplayersShop);
            shopArea.updatePlayersBuild(uiplayersBuild);
        }

        if(gameplayManager != null){
            gameplayManager.updateListPlayers(playersList);
        }

        if(statisticsManager!= null){
            statisticsManager.updateListPlayers(playersList);
        }

        pauseMenu.setUIShops(uiplayersShop);
        pauseMenu.setPlayersActive(playersList);
    }

    // Función que permite obtener los jugadores en partida
    public List<GameObject> getPlayers(){
        return playersList;
    }

    // Función que permite obtener las UI de los jugadores de la partida
    public List<GameObject> getUIPlayers(){
        return uiplayersShop;
    }

    // Función encargada de spawnear a los jugadores en una celda específica
    private void SpawnAtCelda(Transform playerTrans, int pos){
        lightPosRef[pos].GetComponent<Light2D>().intensity = 0.8f;
        lightPosRef[pos].GetComponent<BoxCollider2D>().enabled = false;
        playerTrans.position = lightPosRef[pos].transform.position;
    }
}
