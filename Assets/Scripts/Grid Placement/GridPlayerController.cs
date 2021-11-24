using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlayerController : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Vector2 mousePos;
    [SerializeField] private Vector2 movGrid;

    [SerializeField] private bool colocar = false;

    // Update is called once per frame
    void Update()
    {
        //mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);       
        //transform.position = new Vector2(Mathf.Round(mousePos.x),Mathf.Round(mousePos.y));
    }

    // Función que responde al callback al presionar el botón
    public void onGridMov(InputAction.CallbackContext context){
        movGrid = context.ReadValue<Vector2>();
    }

    // Función que responde al callback al colocar un objeto
    public void onPlaceObject(InputAction.CallbackContext context){
        if(context.performed){
            colocar = true;
        }
    }

    // Función que permite obtener el movimiento asociado a las teclas
    public Vector2 getGridMov(){
        return movGrid;
    }

    // Función que cambia el estado de colocar
    public void setColocar(bool value){
        colocar = value;
    }

    // Función que permite obtener el estado de colocar
    public bool getColocar(){
        return colocar;
    }
}
