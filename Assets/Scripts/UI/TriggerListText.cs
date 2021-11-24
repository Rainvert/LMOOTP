using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerListText : MonoBehaviour
{

    [SerializeField] private int playerInArea;
    [SerializeField] private List<GameObject> textToShow;
    [SerializeField] private List<GameObject> UITextToShow;
    [SerializeField] private CanvasGroup UITextSugg;

    // Función encargada de revisar cuando se entra a un trigger para mostrar el texto
    void OnTriggerEnter2D(Collider2D col){
        if(textToShow[0] != null){
            if(col.tag == "Player"){
                playerInArea += 1;
                foreach (var text in textToShow)
                {
                    text.GetComponent<ShowHelpOnTrigger>().ShowHelpText(col.GetComponent<Apuntado>().getDevice());
                }
            }
        }

    }

    // Función encargada de revisar cuando se sale de un trigger para mostrar el texto
    void OnTriggerExit2D(Collider2D col){
        if(textToShow[0] != null){
            if(col.tag == "Player"){
                playerInArea -= 1;
                if(playerInArea==0){
                    foreach (var text in textToShow)
                    {
                        text.GetComponent<ShowHelpOnTrigger>().HideHelpText();
                    }
                }
            }
        }
    }

    // Función que actualiza las sugerencias según el dispositivo detectado
    public void checkSuggDevice(string device, int variante){
        for (int i = 0; i < UITextToShow.Count; i++)
        {
            UITextToShow[i].GetComponent<ShowUIText>().checkPlayerDevice(device,variante);
        }
    }

    // Función que muestra las sugerencias o las oculta a petición
    public void showUISugg(bool value){
        if(value){
            UITextSugg.alpha = 1;
        } else{
            UITextSugg.alpha = 0;
        }
    }
}
