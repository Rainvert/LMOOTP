using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[ExecuteAlways]
public class NavigationHandler : MonoBehaviour
{
    // Variables necesarias

    [SerializeField] private LocalisationSystem lenSystemRef;
    [SerializeField] private List<CustomButton> botonesInterfaz;
    [SerializeField] private GameObject firstButton = null;
    [SerializeField] private List<ItemProps> itemsToButton;
    [SerializeField] private ScrollRect scrollBarParent;
    [SerializeField] private RectTransform scrollContainer;
    [SerializeField] private int grupoBotones;
    [SerializeField] private Transform playerOwner;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TextMeshProUGUI tituloTienda;
    [SerializeField] private TextMeshProUGUI cashPlayer;

    [SerializeField] private Transform scrollShop;

    // Función onDisable 
    void OnDisable(){
        if(playerOwner!= null){
            playerOwner.GetComponent<PlayerStats>().onUpdateCash -= updateMoneyUI;
        }    
    }

    // Función que actualiza el dinero del jugador
    public void updateMoneyUI(){
        cashPlayer.text = "$ "+playerOwner.GetComponent<PlayerStats>().getCashPlayer();
    }

    // Función que encuentra el sistema de lenguaje
    private void findLenSystem(){
        lenSystemRef = GameObject.Find("LocalisationSystem").GetComponent<LocalisationSystem>();
    }

    // Función encargada de transformar los items a botones
    public void defineButtonsToItems(){
        if(lenSystemRef == null){
            findLenSystem();
        }

        List<CustomButton> lista = new List<CustomButton>();
        for (int i = 0; i < itemsToButton.Count; i++)
        {
            GameObject boton = Instantiate(buttonPrefab,transform.position,transform.rotation);
            if(firstButton == null){
                firstButton = boton;
            }
            boton.transform.SetParent(scrollShop);
            boton.transform.localScale = Vector3.one;
            boton.GetComponent<CustomButton>().setNavigationHandler(this);
            boton.GetComponent<CustomButton>().setLocalizationSystem(lenSystemRef);
            boton.GetComponent<CustomButton>().setItem(itemsToButton[i]);
            lista.Add(boton.GetComponent<CustomButton>());
        }
        botonesInterfaz = lista;

        // Se actualiza el tamaño del contenedor dependiendo de la cantidad de objetos
        changeContainerSize();

    }

    // Función encargada de cambiar el tamaño del contenedor de la UI dependiendo de la cantidad de objetos en esta
    private void changeContainerSize(){
        float newBottom = 0;
        if(botonesInterfaz.Count>=4){
            newBottom = (botonesInterfaz.Count-4)*100;
        }
        if(scrollContainer!= null){
            scrollContainer.offsetMin = new Vector2(scrollContainer.offsetMin.x,-newBottom);
        }

    }

    // Función encargada de tomar el listado de botones que posee y asignarles su grupo, parent y adyacentes
    public void defineButtonInteraction(){
        for (int i = 0; i < botonesInterfaz.Count; i++)
        {
            botonesInterfaz[i].setGroup(grupoBotones);
            botonesInterfaz[i].setPosAndOtherButtons(i,botonesInterfaz.Count);
            botonesInterfaz[i].setBotonesGrupo(botonesInterfaz);
            botonesInterfaz[i].setParentScroll(scrollBarParent);
        }
    }

    // Función que permite modificar la propiedad que especifica a quien pertenece esta UI
    public void setOwnerUI(Transform player){
        playerOwner = player;
        tituloTienda.text = "PLAYER "+player.GetComponent<PlayerStats>().getIDPlayer();
        cashPlayer.text = "$ "+player.GetComponent<PlayerStats>().getCashPlayer();

        playerOwner.GetComponent<PlayerStats>().onUpdateCash += updateMoneyUI;

    }

    // Función que retorna el dueño del Navigation Handler
    public GameObject getOwnerUI(){
        return playerOwner.gameObject;
    }

    // Función que permite modificar el grupo al cual pertenecen los botones, está vinculado al Player
    public void setGroupNum(int value){
        grupoBotones = value;
    }

    // Función que permite obtener el primer botón creado
    public GameObject getFirstButton(){
        return firstButton;
    }
}
