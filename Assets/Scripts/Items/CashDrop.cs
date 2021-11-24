using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashDrop : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private int valueMoney;
    [SerializeField] private GameObject textCash;
    [SerializeField] private GameObject powerUpSound;
    [SerializeField] private LayerMask layer;

    [SerializeField] private bool existBefore;

    void Start(){
        if(!existBefore){
            Destroy(this.gameObject,10f);
        }
    }

    void LateUpdate(){
        Collider2D targetSeguir = Physics2D.OverlapCircle(transform.position,2f,layer);
        if(targetSeguir != null){
            transform.position = Vector2.MoveTowards(transform.position,targetSeguir.transform.position+ new Vector3(0f,-0.2f,0f),0.1f);
        }
    }

    // Función que permite cambiar el valor externo de un cashDrop
    public void setCashValue(int value){
        valueMoney = value;
    }

    // Función encargada de revisar si un player tuvo contacto con la moneda y darle la cantidad que corresponde dinero
    void OnTriggerEnter2D(Collider2D col){
        if(col.tag == "Player"){

            List<GameObject> jugadores = FindObjectOfType<PlayerManager>().getPlayers();
            float division = valueMoney/jugadores.Count;

            for (int i = 0; i < jugadores.Count; i++)
            {  
                jugadores[i].GetComponent<PlayerStats>().plusCashPlayer((int) division);
                
                GameObject textInsta = Instantiate(textCash,jugadores[i].transform.position,jugadores[i].transform.rotation);
                textInsta.GetComponent<TextMesh>().color = Color.green;
                textInsta.GetComponent<TextMesh>().text = "$"+(int) division;
                
                GameObject soundTake = Instantiate(powerUpSound,jugadores[i].transform.position,jugadores[i].transform.rotation);
                soundTake.GetComponent<AudioSource>().Play();
                Destroy(soundTake,1f);
            }

            Destroy(this.gameObject);
        }
    }

    // Función que permite obtener el valor de la moneda spawneada
    public int getCashValue(){
        return valueMoney;
    }
}
