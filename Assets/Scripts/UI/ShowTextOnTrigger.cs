using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTextOnTrigger : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private TextMesh textoMostrar;

    [SerializeField] private string textTeclado;
    [SerializeField] private string textControl;
    [SerializeField] private string textVariante;

    private bool firstState;    

    private float alpha;

    void Start(){
        alpha = 0f;
        firstState = true;
        textoMostrar.color = new Color(textoMostrar.color.r,textoMostrar.color.g,textoMostrar.color.b,alpha);
   }

    // Función encargada de revisar cuando se entra a un trigger para mostrar el texto
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            alpha = 1f;
            textoMostrar.color = new Color(textoMostrar.color.r,textoMostrar.color.g,textoMostrar.color.b,alpha);
            if(firstState){
                if(col.GetComponent<Apuntado>().getDevice() == "Teclado"){
                    textoMostrar.text = textTeclado;
                }
                if(col.GetComponent<Apuntado>().getDevice() == "Control"){
                    textoMostrar.text = textControl;
                }
            } else{
                textoMostrar.text = textVariante;
            }
        }
    }

    // Función encargada de revisar cuando se sale de un trigger para mostrar el texto
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            alpha = 0f;
            textoMostrar.color = new Color(textoMostrar.color.r,textoMostrar.color.g,textoMostrar.color.b,alpha);
        }
    }

    // Función que permite cambiar la variante de texto
    public void setState(bool value){
        firstState = value;
    }

}
