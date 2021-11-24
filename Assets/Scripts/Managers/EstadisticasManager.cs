using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadisticasManager : MonoBehaviour
{
    // Variables necesarias 
    [SerializeField] private GameObject canvasScore;
    [SerializeField] private GameObject canvasRound;
    [SerializeField] private List<ScoreBoardHolder> UIScore;
    [SerializeField] private List<GameObject> players;

    void Start(){
        UIScore = new List<ScoreBoardHolder>(canvasScore.GetComponentsInChildren<ScoreBoardHolder>());
    }

    // Función que actualiza a los jugadores en campo
    public void updateListPlayers(List<GameObject> lista){
        players = lista;
    }

    // Función que prepara los valores de score para todos los jugadores
    public void updateScore(int lastRound){

        canvasRound.GetComponent<ScoreRoundHolder>().setRoundText(lastRound);
        for (int i = 0; i < players.Count; i++)
        {
            UIScore[i].setInUse(true);
            UIScore[i].setNombrePlayer("Player "+players[i].GetComponent<Estadisticas>().getIndex());
            UIScore[i].setCtdaEliminados(players[i].GetComponent<Estadisticas>().getKills());
            UIScore[i].setCtdaDaño(players[i].GetComponent<Estadisticas>().getDamage());
            UIScore[i].setColorPlayer(players[i].GetComponent<PlayerStats>().getColorLife());
        }
        for (int i = 0; i < UIScore.Count; i++)
        {
            if(!UIScore[i].getInUse()){
                UIScore[i].gameObject.SetActive(false);
            }
        }
    }

    // Función que muestra u oculta la pantalla de Score
    public void showScoreBoard(bool value){
        canvasScore.GetComponent<Animator>().SetBool("needToShow",value);
    }
    
}
