using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UISuggestControlsMenu : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Animator animControl;
    [SerializeField] private TextMeshProUGUI textoControls;

    // Función que setea el valor para mostrar u ocultar la interfaz
    public void showUIControls(bool value){
        animControl.SetBool("isShown",value);
    }

    
}
