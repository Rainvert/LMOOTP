using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem;

public class ShopArea : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<GameObject> uiPlayersShop;
    [SerializeField] private List<GameObject> uiPlayersBuild;
    [SerializeField] private Vector3 offset;

    [SerializeField] private CanvasGroup progressBarNucleo;

    private bool showingUI;

    [SerializeField] private ModoJuego modoJuego;

    public void Start(){
        // Método se encargada de revisar que las UI no se interpongan unas en el camino de las otras
        StartCoroutine(checkDistanceUI());
        showingUI = false;
        if(progressBarNucleo!= null){
            progressBarNucleo.alpha = 0f;
        }
    }

    // Función que actualiza la lista de las UI Shop existentes en el escenario
    public void updatePlayersShop(List<GameObject> list){
        uiPlayersShop = list;
        //deactivateUI();
    }

    // Función que actualiza la lista de las UI Build existentes en el escenario
    public void updatePlayersBuild(List<GameObject> list){
        uiPlayersBuild = list;
        //deactivateUI();
    }

    // Función encargada de desactivar las UI 
    private void deactivateUI(){
        for (int i = 0; i < uiPlayersShop.Count; i++)
        {
            uiPlayersShop[i].GetComponent<CanvasGroup>().interactable = false;
            uiPlayersShop[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
            uiPlayersShop[i].GetComponent<CanvasGroup>().alpha = 0f;
        }
        //Debug.Log("Hola");
    }

    // Función encargada de revisar cuando un player entra a un trigger y activar la posibilidad de abrir la tienda
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            if(modoJuego == ModoJuego.Endless){
                col.GetComponent<ChangeControl>().setCanStartRonda(true);
            }
            col.GetComponent<ChangeControl>().setRangeOfShop(true,this);
            col.GetComponent<ChangeControl>().setRangeOfBuild(true,this);
            if(!showingUI && progressBarNucleo!= null){
                StartCoroutine(moveAlphaMenu(progressBarNucleo,0.6f,0.02f));
            }
        }
    }

    // Función encargada de revisar cuando un player sale de un trigger y desactivar la posibilidad de abrir la tienda
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            if(modoJuego == ModoJuego.Endless){
                col.GetComponent<ChangeControl>().setCanStartRonda(false);
            }
            col.GetComponent<ChangeControl>().setRangeOfShop(false,null);
            col.GetComponent<ChangeControl>().setRangeOfBuild(false,this);
            col.GetComponent<ChangeControl>().closeAnyShop();
        }
    }

    // Función encargada de activar o desactivar la UI de la tienda para el player correspondiente
    public void OpenShop(int index,bool openShop, bool otherOpen, Transform playerPos){
        if(openShop && !otherOpen){

            uiPlayersShop[index].GetComponent<CanvasGroup>().interactable = true;
            uiPlayersShop[index].GetComponent<CanvasGroup>().blocksRaycasts = true;

            MultiplayerEventSystem multi =  uiPlayersShop[index].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();
            InputSystemUIInputModule inputSystem = uiPlayersShop[index].transform.parent.gameObject.GetComponent<InputSystemUIInputModule>();

            multi.playerRoot = null;
            multi.playerRoot = uiPlayersShop[index];
            multi.SetSelectedGameObject(uiPlayersShop[index].GetComponent<NavigationHandler>().getFirstButton());

            uiPlayersShop[index].GetComponent<NavigationHandler>().updateMoneyUI();
            uiPlayersShop[index].transform.position = playerPos.position + offset;
            uiPlayersShop[index].GetComponent<Animator>().SetBool("isShown",true);

            if(playerPos.GetComponent<Apuntado>().getDevice() == "Teclado"){
                CursorConfig cursor = FindObjectOfType<CursorConfig>();
                cursor.mostrarCursor(true);
            }

            GetComponent<TriggerListText>().checkSuggDevice(playerPos.GetComponent<Apuntado>().getDevice(),0);
            GetComponent<TriggerListText>().showUISugg(true);
            //StopAllCoroutines();
            //StartCoroutine(moveAlphaMenu(uiPlayersShop[index].GetComponent<CanvasGroup>(),0.9f,0.05f));
        } else{

            if(playerPos.GetComponent<Apuntado>().getDevice() == "Teclado"){
                CursorConfig cursor = FindObjectOfType<CursorConfig>();
                cursor.mostrarCursor(false);
            }

            uiPlayersShop[index].GetComponent<CanvasGroup>().interactable = false;
            uiPlayersShop[index].GetComponent<CanvasGroup>().blocksRaycasts = false;
            uiPlayersShop[index].GetComponent<Animator>().SetBool("isShown",false);

            GetComponent<TriggerListText>().showUISugg(false);
            //StopAllCoroutines();
            //StartCoroutine(moveAlphaMenu(uiPlayersShop[index].GetComponent<CanvasGroup>(),0f,0.05f));
        }
    }

    // Función encargada de activar o desactivar la UI de construcción para el player correspondiente
    public void OpenBuild(int index,bool OpenBuild,bool otherOpen, Transform playerPos){
        if(OpenBuild && !otherOpen){

            uiPlayersBuild[index].GetComponent<CanvasGroup>().interactable = true;
            uiPlayersBuild[index].GetComponent<CanvasGroup>().blocksRaycasts = true;

            MultiplayerEventSystem multi =  uiPlayersBuild[index].transform.parent.gameObject.GetComponent<MultiplayerEventSystem>();
            multi.playerRoot = null;
            multi.playerRoot = uiPlayersBuild[index];
            multi.SetSelectedGameObject(uiPlayersBuild[index].GetComponent<NavigationHandler>().getFirstButton());

            uiPlayersBuild[index].GetComponent<NavigationHandler>().updateMoneyUI();
            uiPlayersBuild[index].transform.position = playerPos.position + offset;
            uiPlayersBuild[index].GetComponent<Animator>().SetBool("isShown",true);

            if(playerPos.GetComponent<Apuntado>().getDevice() == "Teclado"){
                CursorConfig cursor = FindObjectOfType<CursorConfig>();
                cursor.mostrarCursor(true);
            }

            GetComponent<TriggerListText>().checkSuggDevice(playerPos.GetComponent<Apuntado>().getDevice(),1);
            GetComponent<TriggerListText>().showUISugg(true);
            //StopAllCoroutines();
            //StartCoroutine(moveAlphaMenu(uiPlayersBuild[index].GetComponent<CanvasGroup>(),0.9f,0.1f));
        } else{

            if(playerPos.GetComponent<Apuntado>().getDevice() == "Teclado"){
                CursorConfig cursor = FindObjectOfType<CursorConfig>();
                cursor.mostrarCursor(false);
            }

            uiPlayersBuild[index].GetComponent<CanvasGroup>().interactable = false;
            uiPlayersBuild[index].GetComponent<CanvasGroup>().blocksRaycasts = false;
            uiPlayersBuild[index].GetComponent<Animator>().SetBool("isShown",false);

            GetComponent<TriggerListText>().showUISugg(false);
            //StopAllCoroutines();
            //StartCoroutine(moveAlphaMenu(uiPlayersBuild[index].GetComponent<CanvasGroup>(),0f,0.1f));
        }
    }

    // Función encargada de activar o desactivar la UI de construcción para el player correspondiente cuando este está en modo construcción
    public void HideBuild(int index,bool OpenBuild, Transform playerPos){
        if(OpenBuild){

            uiPlayersBuild[index].GetComponent<CanvasGroup>().interactable = false;
            uiPlayersBuild[index].GetComponent<CanvasGroup>().blocksRaycasts = false;
            uiPlayersBuild[index].GetComponent<Animator>().SetBool("inHide",true);

        } else{

            uiPlayersBuild[index].GetComponent<CanvasGroup>().interactable = true;
            uiPlayersBuild[index].GetComponent<CanvasGroup>().blocksRaycasts = true;
            uiPlayersBuild[index].GetComponent<Animator>().SetBool("inHide",false);

        }
    }

    // Corrutina encargada de cambiar el alpha de un objeto determinado
    IEnumerator moveAlphaMenu(CanvasGroup target,float endValue, float velocidad){
        float initValue = target.alpha;
        float porcentaje = 0f;
        float error = 0.02f;
        while(true){

            target.alpha = Mathf.Lerp(initValue,endValue,porcentaje);
            
            porcentaje += velocidad;

            if(Mathf.Abs(target.alpha - endValue) <= error){
                break;
            }

            yield return null;
        }
        target.alpha = endValue;
        yield break;
    }

    // Corrutina encargada de verificar la distancia de la UI entre las otras para evitar que se interfieran entre sí
    IEnumerator checkDistanceUI(){
        while(true){
            if(uiPlayersShop.Count > 1){
                for (int i = 0; i < uiPlayersShop.Count; i++)
                {
                    for (int j = i+1; j < uiPlayersShop.Count; j++)
                    {
                        if(uiPlayersShop[i].GetComponent<CanvasGroup>().interactable && uiPlayersShop[j].GetComponent<CanvasGroup>().interactable){
                            Vector2 posI = uiPlayersShop[i].transform.position - uiPlayersShop[j].transform.position;
                            Vector3 offsetCorrect = Vector3.zero;
                            if(Mathf.Abs(posI.x)<3 && Mathf.Abs(posI.y)<3){
                                offsetCorrect.x = 1f;
                                offsetCorrect.y = 1f;
                            }
                            uiPlayersShop[i].transform.position += offsetCorrect;
                        }
                    }
                }
            }
            
            yield return null;
        }
    }

    // Función encargada de cambiar el tamaño de la tienda
    public bool changeSize(float value){
        transform.localScale += Vector3.one*value;
        return true;
    }
}
