using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoBoxTrigger : MonoBehaviour
{

    [SerializeField] private RectTransform dialogoUI;
    [SerializeField] private AudioSource audioClip;

    [SerializeField] private bool wasRead, canRepeat;

    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] private int maxText;

    [TextArea] [SerializeField] private string texto;

    void Start(){
        dialogoUI.localScale = new Vector3(dialogoUI.localScale.x,0f,dialogoUI.localScale.z);
        wasRead = false;
    }

    // Función encargada de revisar cuando se entra a un trigger para mostrar el texto
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            StopAllCoroutines();
            StartCoroutine(moveAlphaMenu(1f));
            StartCoroutine(showTextByLetter());
        }
    }

    // Función encargada de revisar cuando se sale de un trigger para mostrar el texto
    void OnTriggerExit2D(Collider2D col){
        if(col.tag == "Player"){
            StopAllCoroutines();
            StartCoroutine(moveAlphaMenu(0f));
        }
    }

    // Corrutina encargada de mostrar el texto letra por letra
    IEnumerator showTextByLetter(){
        textMesh.text = "";
        int contador = 0;
        foreach (var letra in texto)
        {
            textMesh.text += letra;
            contador++;
            audioClip.Play();
            if(contador>maxText){
                textMesh.text += "-";
                yield return new WaitForSeconds(1f);
                textMesh.text = "-";
                contador = 0;
            }
            yield return new WaitForSeconds(0.05f);
        }
        if(!canRepeat){
            wasRead = true;
        }
        
    }

    // Corrutina encargada de cambiar la escala dde la UI de una manera progresiva (Fade-in and Fade-out)
    IEnumerator moveAlphaMenu(float finalValue){
        float contador = 0f;
        float timeLimit = 0.5f;
        float porcentaje = 0f;
        while(contador<timeLimit){
            dialogoUI.localScale = new Vector3(dialogoUI.localScale.x,Mathf.Lerp(dialogoUI.localScale.y,finalValue,porcentaje),dialogoUI.localScale.z);
            contador = contador + Time.deltaTime;
            porcentaje += 0.01f;
            yield return null;
        }
        dialogoUI.localScale = new Vector3(dialogoUI.localScale.x,finalValue,dialogoUI.localScale.z);

        if(wasRead){
            Destroy(this.gameObject);
        }
    }
}
