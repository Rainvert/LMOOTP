using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowUIText : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Image imagenSugg; 
    [SerializeField] private List<Sprite> tecladoSprite;
    [SerializeField] private List<Sprite> controlSprite;
    private int variante;

    // Función encargada de cambiar la imagen de la sugerencia según el dispotivo del usuario
    public void checkPlayerDevice(string dispositivo,int index){
        if(tecladoSprite.Count-1 >= index){
            variante = index;
        }
        if(dispositivo == "Teclado"){
            imagenSugg.sprite = tecladoSprite[variante];
        }
        if(dispositivo == "Control"){
            imagenSugg.sprite = controlSprite[variante];
        }
    }
}
