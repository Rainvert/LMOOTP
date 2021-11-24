using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] List<GameObject> bgmList;

    [SerializeField] List<GameObject> effectList;

    void Start(){
        deactiveBGM();
        bgmList[0].GetComponent<BGMMusic>().subirVolumen();
    }

    // Función encargada de activar la música solicitada si existe en la lista
    public void activateBGM(BGMType tipo){
        for (int i = 0; i < bgmList.Count; i++)
        {
            if((int) bgmList[i].GetComponent<BGMMusic>().getTypeBGM() == (int) tipo){
                Debug.Log("Existe BGM es: " + bgmList[i]);
                bgmList[i].GetComponent<BGMMusic>().subirVolumen();
            } else{
                bgmList[i].GetComponent<BGMMusic>().bajarVolumen();
            }
        }
    }

    // Función encargada de desactivar toda la música de BGM
    public void deactiveBGM(){
        for (int i = 0; i < bgmList.Count; i++)
        {
            bgmList[i].GetComponent<BGMMusic>().bajarVolumenNow();
        } 
    }

    // Función que reproduce un clip específico
    public void playEffect(int index){
        effectList[index].GetComponent<BGMMusic>().playOnce();
    }

}


