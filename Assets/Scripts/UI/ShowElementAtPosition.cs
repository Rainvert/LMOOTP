using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowElementAtPosition : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private Transform camara;
    [SerializeField] private CanvasGroup alphaCanvas;

    [SerializeField] private UISuggestControlsMenu controlSuggest;
    [SerializeField] private float refDistanceToShow;

    private bool activated = false;
    
    void Start(){
        alphaCanvas.alpha = 0f;
        alphaCanvas.interactable = false;
        alphaCanvas.blocksRaycasts = false;
    }

    void Update(){
        if(Input.anyKeyDown && !activated){
            activated = true;
            StopAllCoroutines();
            controlSuggest.showUIControls(false);
            StartCoroutine(moveAlphaMenu(1f));
        }
        if(Mathf.Abs(camara.position.x) <= refDistanceToShow && !activated){
            StopAllCoroutines();
            controlSuggest.showUIControls(false);
            StartCoroutine(moveAlphaMenu(1f));
            activated = true;
        }
        
        if(Mathf.Abs(camara.position.x) <= refDistanceToShow*3 && Mathf.Abs(camara.position.x) >= refDistanceToShow*2.5f && !activated){
            controlSuggest.showUIControls(true);
        }
        if(Mathf.Abs(camara.position.x) <= refDistanceToShow*2.5f && !activated){
            controlSuggest.showUIControls(false);
        }
    }


    // Corrutina encargada de cambiar el alpha de la UI de una manera progresiva (Fade-in and Fade-out)
    IEnumerator moveAlphaMenu(float finalValue){
        float initValue = alphaCanvas.alpha;
        float porcentaje = 0f;
        float error = 0.01f;

        while(true){
            alphaCanvas.alpha = Mathf.Lerp(initValue,finalValue,porcentaje);
            porcentaje += 0.01f;

            if(Mathf.Abs(alphaCanvas.alpha - finalValue) <= error){
                break;
            }
            yield return new WaitForSeconds(0.01f);
        }
        alphaCanvas.alpha = finalValue;
        alphaCanvas.interactable = true;
        alphaCanvas.blocksRaycasts = true;
    }
}
