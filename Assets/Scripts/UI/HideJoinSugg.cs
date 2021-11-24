using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HideJoinSugg : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private CanvasGroup joinSugg;
    [SerializeField] private CanvasGroup joinSuggInGame;
    [SerializeField] private int maxPlayers;
    void Start(){
        joinSugg.alpha = 1f;
        joinSuggInGame.alpha = 0f;
    }

    // Función que oculta la sugerencia de unirse
    public void hideJoinSugg(PlayerInput context){
        if(joinSugg!=null){
            joinSugg.alpha = 0f;
            joinSuggInGame.alpha = 0.3f;
        }
    }

    // Función que revisa si se pueden unir más aliados 
    public void checkPlayersConnected(PlayerInput context){
        int ctdaJugadores = FindObjectOfType<PlayerManager>().getPlayers().Count;
        if(ctdaJugadores == maxPlayers){
            joinSuggInGame.alpha = 0f;
            GetComponent<PlayerInputManager>().DisableJoining();
        }
    }

}
