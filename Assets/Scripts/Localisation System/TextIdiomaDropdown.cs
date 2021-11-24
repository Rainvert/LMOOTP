using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIdiomaDropdown : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private LocalisationSystem lenSystemRef;
    [SerializeField] private string[] llavesAsignada;
    [SerializeField] private TMP_Dropdown dropdown_Text;

    void Start(){
        lenSystemRef = GameObject.Find("LocalisationSystem").GetComponent<LocalisationSystem>();
        eventInit();
        getTextFromKey();
    }

    // Función onEnable
    void eventInit(){
        lenSystemRef.onIdiomaChange += getTextFromKey;
    }

    // Función onDisable 
    void OnDisable(){
        lenSystemRef.onIdiomaChange -= getTextFromKey;
    }

    // Función que permite obtener y setear el valor del texto asignado para el drowndown
    private void getTextFromKey(){
        for (int i = 0; i < llavesAsignada.Length; i++)
        {
            dropdown_Text.options[i].text = lenSystemRef.getLanguajeText(llavesAsignada[i]);
        }
        dropdown_Text.RefreshShownValue();
    }

}
