using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerNextLevel : MonoBehaviour
{

    [SerializeField] private GameObject canvasAlpha;

    // Función encargada de activar las armas del jugador al desplazarse por la zona 
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){
            StartCoroutine(transitionNextLevel());
        } 
    }

    // Corrutina encargada de mostrar la pantalla de muerte
    IEnumerator transitionNextLevel(){
        float alphaLerp = 0f;
        while(true){
            float alphaActual = canvasAlpha.GetComponent<CanvasGroup>().alpha;
            alphaActual = Mathf.Lerp(alphaActual,1,alphaLerp);
            canvasAlpha.GetComponent<CanvasGroup>().alpha = alphaActual;
            alphaLerp += 0.005f;
            yield return new WaitForSeconds(0.02f);

            if(alphaActual==1f){
                break;
            }
        }

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
