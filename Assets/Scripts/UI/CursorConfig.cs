using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorConfig : MonoBehaviour
{

    // Función encargada de confinar el cursor a la pantalla de juego
    public void confinarCursor(){
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Función encargada de desconfinar el cursor de la pantalla de juego
    public void desconfinarCursor(){
        Cursor.lockState = CursorLockMode.None;
    }

    // Función que muestra u oculta el cursor
    public void mostrarCursor(bool value){
        Cursor.visible = value;
    }

}
