using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPoint : MonoBehaviour
{
    [SerializeField] private Vector3 finalPoint;
    [SerializeField] private float step;

    void Start(){
        StartCoroutine(moveCamera());
    }

    // Corrutina encargada de desplazar la cámara al punto determinado
    IEnumerator moveCamera(){
        bool inMove = true;
        while(inMove){
            transform.position = Vector3.MoveTowards(transform.position,finalPoint,step);
            if(Mathf.Abs(transform.position.x - finalPoint.x) < 0.01f){
                inMove = false;
            }
            yield return new WaitForSeconds(0.01f);
        }
        transform.position = finalPoint;
    }
}
