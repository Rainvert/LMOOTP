using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ToggleCustom : Toggle
{

    // Variables necesarias
    [SerializeField] private PauseOptions pausa;

    [SerializeField] private PlayerInput playersInput;

    [SerializeField] private string device;

    // Función que permite al Toggle trabajar con desplazamiento con control y snap to position
    public override void OnSelect(BaseEventData eventData){

        if(pausa== null){ 
            device = playersInput.currentControlScheme;
        }else{
            device = pausa.getInPauseDevice();
        }

        if(device!="Teclado"){
            Debug.Log(device);

            int index = transform.GetSiblingIndex()-1;

            RectTransform rectContainer = transform.parent.GetComponent<RectTransform>();

            float offset = GetComponent<RectTransform>().sizeDelta.y;

            Vector3 newPos = new Vector3(rectContainer.localPosition.x,index*offset,rectContainer.localPosition.z);

            rectContainer.localPosition = newPos;

        }

        base.OnSelect(eventData);
    }
    
}
