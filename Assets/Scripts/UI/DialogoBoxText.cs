using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoBoxText : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private DialogoBoxFixed dialogoHolder;
    [SerializeField] private Animator animDialog;
    [SerializeField] private Color colorDialogo;
    [SerializeField] private Sprite talkerSprite; 

    [SerializeField] private int TipoDialogo;
    [SerializeField] private bool canRepeat;

    [SerializeField] private string[] clavesDictTexto;
    [TextArea(3,10)][SerializeField] private string[] textoBox;

    public void Start(){
        dialogoHolder = FindObjectOfType<DialogoBoxFixed>();
        animDialog.SetInteger("TipoDialogo",TipoDialogo);
    }

    // Función encargada de revisar cuando se entra a un trigger para mostrar el texto
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            setColorDialogo();
            setTalkerDialogo();
            addDialogo();
            if(!canRepeat){
                Destroy(this.gameObject);
            }
        }
    }

    // Función que envia un mensaje a la cola para mostrar en el dialogoBox
    private void addDialogo(){
        dialogoHolder.AgregarClaveDialogos(clavesDictTexto);
    }

    // Función que permite setear el color del dialogoHolder
    private void setColorDialogo(){
        dialogoHolder.SetColorDialogoHolder(colorDialogo);
    }
 
    // Función que permite setear la imagen del dialogoHolder
    private void setTalkerDialogo(){
        dialogoHolder.SetTalkerSprite(talkerSprite);
    }


}
