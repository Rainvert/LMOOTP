using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreRoundHolder : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private TextMeshProUGUI textRound;
    [SerializeField] private TextMeshProUGUI highScoreNumber;
    [SerializeField] private GameObject newCanvas;

    private float highScore;

    void Start(){
        highScore = PlayerPrefs.GetFloat("RondaMaxima",0);
        highScoreNumber.text = highScore.ToString();
        newCanvas.SetActive(false);
    }

    // Función que permite cambiar la ronda final obtenida
    public void setRoundText(int value){
        textRound.text = value.ToString();
        verifyHighScore(value);
    }

    // Función que verifica si el highscore de la partida
    private void verifyHighScore(int value){
        if(value>highScore){
            highScore = value;
            highScoreNumber.text = highScore.ToString();
            PlayerPrefs.SetFloat("RondaMaxima",highScore);
            newCanvas.SetActive(true);
        }
    }

}
