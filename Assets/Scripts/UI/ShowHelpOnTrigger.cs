using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowHelpOnTrigger : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Image imageHelp;
    
    [SerializeField] private bool canShow;

    [Header("Teclado Show")]
    [SerializeField] private Sprite spriteTeclado;

    [Header("Control Show")]
    [SerializeField] private Sprite spriteControl;

    void Start(){
        if(!canShow){
            GetComponent<CanvasGroup>().alpha = 0f;
        }
   }

    // Función encargada de mostrar el Texto y la Imagen
    public void ShowHelpText(string playerDevice){
        GetComponent<CanvasGroup>().alpha = 1f;
        if(playerDevice == "Teclado"){
            imageHelp.sprite = spriteTeclado;
        }
        if(playerDevice == "Control"){
            imageHelp.sprite = spriteControl;
        }
    }

    // Función encargada de ocultar el Texto y la Imagen
    public void HideHelpText(){
        GetComponent<CanvasGroup>().alpha = 0f;
    }

}
