using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextIdioma : MonoBehaviour
{

    // Variables necesarias
    [SerializeField] private LocalisationSystem lenSystemRef;
    [SerializeField] private string llaveAsignada;
    [SerializeField] private TextMeshProUGUI texto;


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

    // Función que permite obtener y setear el valor del texto asignado
    private void getTextFromKey(){
        texto.text = lenSystemRef.getLanguajeText(llaveAsignada);
    }

}
