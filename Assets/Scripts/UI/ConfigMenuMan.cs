using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
public class ConfigMenuMan : MonoBehaviour
{

    // Variables necesarias
    [SerializeField] private TMP_Dropdown dropdownCalidad;
    [SerializeField] private TMP_Dropdown dropdownResol;

    [SerializeField] private TMP_Dropdown dropdownIdioma;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumenGeneral;
    [SerializeField] private AudioMixer mixerGeneral;

    private Resolution[] resoluciones;

    void Start(){

        loadAllResolutions();
        loadCalidad();
        loadIdioma();
        loadFullScreen();
        loadVolumen();

    }

    // Función encargada de cargar el volumen del juego
    private void loadVolumen(){
        float value = PlayerPrefs.GetFloat("Volumen",1);
        volumenGeneral.value = value;
        mixerGeneral.SetFloat("volumenGeneral",Mathf.Log10(value)*20);
    }

    // Función encargada de cargar el idioma del juego
    private void loadIdioma(){
        int value = PlayerPrefs.GetInt("Idioma",1);
        dropdownIdioma.value = value;
        dropdownIdioma.RefreshShownValue();
    }

    // Función encargada de cargar el toggle de fullscreen
    private void loadFullScreen(){
        int value = PlayerPrefs.GetInt("Fullscreen",1);
        bool isFullScreen = false;
        if(value == 1){
            isFullScreen = true;
        }
        fullscreenToggle.isOn = isFullScreen;
        Screen.fullScreen = isFullScreen;

    }

    // Función encargada de cargar la calidad elegida
    private void loadCalidad(){
        int PreCalidad = PlayerPrefs.GetInt("Calidad",-1);

        if(PreCalidad == -1){
            PreCalidad = 2;
        }

        QualitySettings.SetQualityLevel(PreCalidad);
        dropdownCalidad.value = PreCalidad;
        dropdownCalidad.RefreshShownValue();
    }

    // Función encargada de listar las resoluciones disponibles y la guardada
    private void loadAllResolutions(){
        
        int PreWidth = PlayerPrefs.GetInt("Width",-1);
        int PreHeight = PlayerPrefs.GetInt("Height",-1);
        int PreHz = PlayerPrefs.GetInt("Hz",-1);

        if(PreWidth == -1){
            PreWidth = Screen.currentResolution.width;
            PreHeight = Screen.currentResolution.height;
            PreHz = Screen.currentResolution.refreshRate;
        }

        // Se obtienen las resoluciones disponibles y se agregan
        List<string> opResol = new List<string>(); 
        int resolActual = 0;
        
        dropdownResol.ClearOptions();

        resoluciones = Screen.resolutions;
        for (int i = 0; i < resoluciones.Length; i++)
        {
            string addOption = resoluciones[i].width + " x " + resoluciones[i].height + " "+resoluciones[i].refreshRate+"Hz";
            opResol.Add(addOption);

            if(resoluciones[i].width == PreWidth && resoluciones[i].height == PreHeight && resoluciones[i].refreshRate == PreHz){
                resolActual = i;
            } 
        }

        dropdownResol.AddOptions(opResol);
        dropdownResol.value = resolActual;
        dropdownResol.RefreshShownValue();

    }

    // Función que permite cambiar la resolución
    public void setResolucion(int index){
        Resolution resolElegida = resoluciones[index];
        Screen.SetResolution(resolElegida.width,resolElegida.height,Screen.fullScreen);

        PlayerPrefs.SetInt("Width",resolElegida.width);
        PlayerPrefs.SetInt("Height",resolElegida.height);
        PlayerPrefs.SetInt("Hz",resolElegida.refreshRate);

    }

    // Función que permite cambiar la calidad del juego
    public void setCalidad(int index){
        QualitySettings.SetQualityLevel(index);

        PlayerPrefs.SetInt("Calidad",index);

    }

    // Función que permite cambiar entre fullscreen y ventana
    public void setFullScreen(bool value){
        Screen.fullScreen = value;
        if(value){
            PlayerPrefs.SetInt("Fullscreen",1);
        } else{
            PlayerPrefs.SetInt("Fullscreen",0);
        }
    }

    // Función que permite modificar el idioma del juego
    public void setIdioma(int index){
        PlayerPrefs.SetInt("Idioma",index);
        FindObjectOfType<LocalisationSystem>().setIdiomaSelected(index);
    }

    // Función que permite modificar el volumen general del mixer
    public void setVolumen(float value){
        mixerGeneral.SetFloat("volumenGeneral",Mathf.Log10(value)*20);
        PlayerPrefs.SetFloat("Volumen",value);
    }
}
