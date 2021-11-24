using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalisationSystem : MonoBehaviour
{
    // Variables necesarias
    public delegate void OnLanguajeChange();
    public event OnLanguajeChange onIdiomaChange; 

    private bool isInit;
    private static LocalisationSystem instanceOnce;
    [SerializeField] private Lenguaje lan = Lenguaje.Español;
    [SerializeField] private Dictionary<string,string> spanishDict;
    [SerializeField] private Dictionary<string,string> englishDict;

    void Awake(){

        if(instanceOnce == null){
            instanceOnce = this;
        } else{
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Init();
    }

    void Start(){
        setIdiomaSelected(PlayerPrefs.GetInt("Idioma",1));
    }

    // Función que inicializa los distintos diccionarios por idioma
    public void Init(){
        csvMasterLoader csvCargado = new csvMasterLoader();
        csvCargado.cargarCSV();

        spanishDict = csvCargado.getValueDictionary("es");
        englishDict = csvCargado.getValueDictionary("en");

        isInit = true;
        
    }

    // Función que busca un valor en el diccionario del lenguaje y lo retorna
    public string getLanguajeText(string clave){

        string value = clave;

        if(!isInit){
            Init();
        }

        switch(lan){
            case Lenguaje.Español:
                spanishDict.TryGetValue(clave, out value);
                break;
            case Lenguaje.Inglés:
                englishDict.TryGetValue(clave, out value); 
                break;
        }
         
        return value;
    }

    // Función que retorna el valor de lenguaje del idioma
    public Lenguaje getIdiomaSelected(){
        return lan;
    }

    // Función que permite cambiar el valor del lenguaje seleccionado
    public void setIdiomaSelected(int index){
        if(index == (int) Lenguaje.Español){
            lan = Lenguaje.Español;
        }
        if(index == (int) Lenguaje.Inglés){
            lan = Lenguaje.Inglés;
        }
        
        if(onIdiomaChange!=null){
            onIdiomaChange();
        }
    }
}

// Enum para los distintos lenguajes
public enum Lenguaje{Español,Inglés}