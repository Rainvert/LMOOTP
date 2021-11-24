using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnSuccesiveBlock : MonoBehaviour
{
    [SerializeField] private GameObject Object;

    [SerializeField] private float waitTime,deCastTime;

    [SerializeField] private float width; 

    [SerializeField] private float damageSPAttack;

    private List<GameObject> attackElem;

    private bool inAttackMode;

    [SerializeField] private Vector3 offsetPos;

    void Start(){
        width = Object.GetComponent<SpriteRenderer>().bounds.size.x;
    }

    public void destroyAttack(){
        if(attackElem!= null){
            for (int i = 0; i < attackElem.Count; i++)
            {
                Destroy(attackElem[i]); 
            }
            attackElem.Clear();
        }

    }

    // Función que llama a la invocación y expansión del ataque
    public void activateAttack(){
        StartCoroutine(expaSpawn());
    }

    // Función que permite obtener el valor de inAttackMode
    public bool getAttackMode(){
        return inAttackMode;
    }

    // Corrutina encargada de spawnear elementos de manera progresiva en un ángulo específico
    IEnumerator expaSpawn(){

        inAttackMode = true;

        float[] angulos = {0,45,90,135,180,225,270,315,360};

        if(Random.Range(0f,1f)>=0.5f){
            for (int i = 0; i < angulos.Length; i++)
            {
                angulos[i] += 22.5f;
            }
        }

        attackElem = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            float offsetRot = angulos[i];
            GameObject instaObj = Instantiate(Object,transform.position+offsetPos,transform.rotation);  
            instaObj.GetComponent<BossAttackSP>().setDamage(damageSPAttack);  
            attackElem.Add(instaObj);
            instaObj.transform.localScale = new Vector3(0f,1f,1f);
            instaObj.transform.Rotate(0f,0f,Mathf.Atan2(transform.forward.y,transform.forward.x)* Mathf.Rad2Deg - offsetRot);
            StartCoroutine(changeSizeSpawn(instaObj,0f,1f));
        }

        yield return new WaitForSeconds(waitTime);

        for (int i = 0; i < attackElem.Count; i++)
        {
            StartCoroutine(changeSizeSpawn(attackElem[i],1f,0f));  
        }

        yield return new WaitForSeconds(deCastTime);
        
        for (int i = 0; i < attackElem.Count; i++)
        {
            Destroy(attackElem[i]);
        }        

        inAttackMode = false;

        attackElem.Clear();

        yield return null;
    }

    // Corrutina encargada de cambiar el tamaño de los objetos spawneados de manera progresiva
    IEnumerator changeSizeSpawn(GameObject target,float initValue,float endValue){
        float contador = 0f;
        float timeLimit = 0.5f;
        float porcentaje = 0f;
        while(contador<timeLimit){
            if(target == null){
                break;
            }
            target.transform.localScale = new Vector3(Mathf.Lerp(initValue,endValue,porcentaje),1f,1f);
            contador = contador + Time.deltaTime;
            porcentaje += 0.05f;
            yield return null;
        }
        if(target!= null){
            target.transform.localScale = Vector3.one*endValue;
        } 
        yield return null;
    }
}
