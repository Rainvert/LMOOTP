using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{

    [SerializeField] private PauseOptions optionPause;

    [SerializeField] private int escena;

    // Función encargada de Cargar la primera escena del juego
    public void LoadStage(){
        Debug.Log("Escena Cargada");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + escena);
    }

    // Función encargada de cerrar la aplicación
    public void ExitGame(){
        Debug.Log("Aplicación Cerrada");
        Application.Quit();
    }

    // Función encargada de resumir el juego luego de pausarse
    public void ResumeGame(){
        if(optionPause!=null){
            Time.timeScale = 1f;
            optionPause.pauseMenuDeActive();
        }

    }

    // Función encargada de abrir el menú de configuraciones
    public void ConfigMenu(){
        optionPause.ajustesMenuActive();
    }

    // Función encargada de enviar al menú principal 
    public void QuitToMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
