using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estadisticas : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private int playerIndex;
    [SerializeField] private int monsterKill;
    [SerializeField] private float totalDamage;

    // Función encargada de setear el index del player
    public void setIndex(int value){
        playerIndex = value;
    }

    // Función que añade un número determinado de enemigos a la cuenta total
    public void addKill(int value){
        monsterKill += value;
    }

    // Función que añade una cantidad determinada de daño al total acumulado
    public void addDamage(float value){
        totalDamage += value;
    }

    // Función que permite obtener el index del player
    public int getIndex(){
        return playerIndex;
    }

    // Función que permite obtener la cantidad de enemigos derrotados
    public int getKills(){
        return monsterKill;
    }

    // Función que permite obtener la cantidad total de daño infligido 
    public float getDamage(){
        return totalDamage;
    }
}
