using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ScoreBoardHolder : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private bool inUse;
    [SerializeField] private Image fondoPlayer;
    [SerializeField] private TextMeshProUGUI nombrePlayer;
    [SerializeField] private TextMeshProUGUI cantidadMuertos;
    [SerializeField] private TextMeshProUGUI cantidadDaño;

    // Función que permite cambiar el valor de uso del Score
    public void setInUse(bool value){
        inUse = value;
    }

    // Función que permite obtener el valor de uso del score
    public bool getInUse(){
        return inUse;
    }

    // Función que permite cambiar el nombre del player en la tabla
    public void setNombrePlayer(string value){
        nombrePlayer.text = value;
    }

    // Función que permite cambiar el valor de la cantidad de enemigos eliminados
    public void setCtdaEliminados(float value){
        cantidadMuertos.text = value.ToString();
    }
    
    // Función que permite cambiar el valor de la cantidad de daño infligido
    public void setCtdaDaño(float value){
        cantidadDaño.text = value.ToString();
    }

    // Función que permite cambiar el color del fondo del player
    public void setColorPlayer(Color value){
        Color colorNuevo = new Color(value.r,value.g,value.b,0.5f);
        fondoPlayer.color = colorNuevo;
    }
}
