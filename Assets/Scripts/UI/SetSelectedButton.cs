using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SetSelectedButton : MonoBehaviour
{
    
    [SerializeField] private CanvasGroup menuGeneral,menuOpciones;
    [SerializeField] private GameObject firstButtonGeneral,firstButtonOption;

    [SerializeField] private EventSystem multi;

    void Start(){
        hideMenus();
        setFirstButtonGeneral();
        FindObjectOfType<CursorConfig>().desconfinarCursor();
    }

    // Función que permite setear el 1er botón del menú general
    public void setFirstButtonGeneral(){
        multi.SetSelectedGameObject(firstButtonGeneral);
    }

    // Función que permite setear el 1er botón del menú de opciones
    public void setFirstButtonOptions(){
        multi.SetSelectedGameObject(firstButtonOption);
    }

    // Función que abre las opciones del menú
    public void showOptionMenu(){
        menuGeneral.alpha = 0f;
        menuGeneral.interactable = false;
        menuGeneral.blocksRaycasts = false;

        menuOpciones.alpha = 1f;
        menuOpciones.interactable = true;
        menuOpciones.blocksRaycasts = true;

        FindObjectOfType<SetSelectedButton>().setFirstButtonOptions();
    }

    // Función que abre el menú general
    public void showGeneralMenu(){
        menuOpciones.alpha = 0f;
        menuOpciones.interactable = false;
        menuOpciones.blocksRaycasts = false;

        menuGeneral.alpha = 1f;
        menuGeneral.interactable = true;
        menuGeneral.blocksRaycasts = true;
        
        FindObjectOfType<SetSelectedButton>().setFirstButtonGeneral();
    }

    // Función que oculta los menús 
    public void hideMenus(){
        menuGeneral.alpha = 0f;
        menuGeneral.interactable = false;
        menuGeneral.blocksRaycasts = false;
        
        menuOpciones.alpha = 0f;
        menuOpciones.interactable = false;
        menuOpciones.blocksRaycasts = false;


    }

}
