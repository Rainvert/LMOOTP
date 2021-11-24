using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStatsInGame : MonoBehaviour
{
    [SerializeField] private Animator animUI;
    [SerializeField] private TextMeshProUGUI dañoTotal;
    [SerializeField] private TextMeshProUGUI shootDelay;
    [SerializeField] private TextMeshProUGUI empujeBullet;
    [SerializeField] private TextMeshProUGUI shootDelayEmpuje;
    [SerializeField] private TextMeshProUGUI speed;
    [SerializeField] private TextMeshProUGUI botiquines;


    // Función que permite mostrar u ocultar las estadísticas
    public void showUI(bool value){
        if(value){
            animUI.SetBool("isShown",true);
        } else{
            animUI.SetBool("isShown",false);
        }
    }

    // Función que permite cambiar el valor del daño total
    public void setDañoTotal(float value){
        dañoTotal.text = value.ToString("F2");
    }


    // Función que permite cambiar el valor del daño total
    public void setShootDelay(float value){
        shootDelay.text = value.ToString("F2");
    }

    // Función que permite cambiar el valor del daño total
    public void setSpeed(float value){
        speed.text = value.ToString("F2");
    }

    // Función que permite cambiar el valor del daño total
    public void setEmpuje(float value){
        empujeBullet.text = value.ToString("F2");
    }

    // Función que permite cambiar el valor del daño total
    public void setEmpujeDelay(float value){
        shootDelayEmpuje.text = value.ToString("F2");
    }

    // Función que permite cambiar el valor del daño total
    public void setBotiquines(int value, int maxValue){
        botiquines.text = value.ToString()+"/"+maxValue.ToString();
    }  
    
}
