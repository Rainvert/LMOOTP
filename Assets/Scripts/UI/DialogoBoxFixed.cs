using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoBoxFixed : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Queue<string> colaClavesDialogos;
    [SerializeField] private Queue<string> colaDialogo;

    [SerializeField] private Animator animDialog;
    [SerializeField] private AudioSource audioClip;
    [SerializeField] private AudioSource audioClipOpen;
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private Image dialogoSprite;
    [SerializeField] private Image talkerSprite;

    // Variables necesarias
    [SerializeField] private LocalisationSystem lenSystemRef;
    [SerializeField] private TMP_Dropdown dropdown_Text;

    void Start(){
        lenSystemRef = GameObject.Find("LocalisationSystem").GetComponent<LocalisationSystem>();
        eventInit();
        colaDialogo = new Queue<string>();
        colaClavesDialogos = new Queue<string>();
    }

    // Función onEnable
    void eventInit(){
        lenSystemRef.onIdiomaChange += updateDialogoQueue;
    }

    // Función onDisable 
    void OnDisable(){
        lenSystemRef.onIdiomaChange -= updateDialogoQueue;
    }

    // Función que permite obtener y setear el valor del texto asignado para los distintos diálogos en cola
    private void updateDialogoQueue(){
        if(colaClavesDialogos.Count==0){
            return;
        }
        mostrarDialogo();
    }

    // Función que recibe un conjunto de diálogos y los muestra por pantalla
    public void AgregarClaveDialogos(string[] clavesTextos){
        if(clavesTextos.Length!=0){
            colaClavesDialogos.Clear();
            for (int i = 0; i < clavesTextos.Length; i++)
            {
                colaClavesDialogos.Enqueue(clavesTextos[i]);
            }
            animDialog.SetBool("DialogoIsOpen",true);
            audioClipOpen.Play();
            mostrarDialogo();
        }
    }

    // Función que cambiar el color del Sprite de Diálogo
    public void SetColorDialogoHolder(Color colorSet){
        dialogoSprite.color = colorSet;
    }

    // Función que cambiar el Sprite de quien está hablando
    public void SetTalkerSprite(Sprite imageTalker){
        talkerSprite.sprite = imageTalker;
    }

    // Función encargada de mostrar el diálogo en la cola
    private void mostrarDialogo(){
        StopAllCoroutines();
        StartCoroutine(showTextByLetter());
    }

    // Corrutina encargada de mostrar el texto letra por letra
    IEnumerator showTextByLetter(){
        textMesh.text = "";
        string clave = colaClavesDialogos.Peek();
        string texto = lenSystemRef.getLanguajeText(clave);
        foreach (var letra in texto)
        {
            yield return new WaitForSeconds(0.01f);
            textMesh.text += letra;
            if(!audioClip.isPlaying){
                audioClip.Play();
            }
        }      
        yield break;
    }

    // Función encargada de mostrar el siguiente diálogo en la cola con acceso externo
    public void mostrarSiguienteDialogo(){
        if(colaClavesDialogos.Count==0){
            return;
        }

        StopAllCoroutines();
        colaClavesDialogos.Dequeue();
        if(colaClavesDialogos.Count!= 0){
            mostrarDialogo();
        } else{
            animDialog.SetBool("DialogoIsOpen",false);
        }
    }
}
