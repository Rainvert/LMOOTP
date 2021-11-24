using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogoControl : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private DialogoBoxFixed dialogoHandler;

    public void Start(){
        dialogoHandler = FindObjectOfType<DialogoBoxFixed>();
    }

    // Función que recibe el input del botón para mostrar diálogos
    public void onDialogoNext(InputAction.CallbackContext context){
        if(context.performed){
            showNextDialogo();
        }
    }

    // Función encargada de enviar al Dialogo Handler la petición para mostrar el siguiente diálogo si existe
    private void showNextDialogo(){
        if(dialogoHandler!= null){
            dialogoHandler.mostrarSiguienteDialogo();
        }
    }
}
