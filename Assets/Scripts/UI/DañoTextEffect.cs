using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DañoTextEffect : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private float timeLife;
    [SerializeField] private Vector3 scaleMax;
    [SerializeField] private float movRandom;

    private float velocMov = 0.01f;
    private TextMesh alpha;
    private float alphaValue;
    private Vector3 posTowards;

    void Start(){
        posTowards.x = Random.Range(-movRandom,movRandom);
        posTowards.y = movRandom;
        posTowards.z = 0f;
        posTowards += transform.position;
        alpha = GetComponent<TextMesh>();
        alphaValue = 0f;
        StartCoroutine(moveAndScale());
        StartCoroutine(destroyText());
    }

    // Corrutina que destruye el objeto
    IEnumerator destroyText(){
        yield return new WaitForSeconds(timeLife);
        Destroy(this.gameObject);
    }


    // Corrutina que controla el movimiento y escala del texto
    IEnumerator moveAndScale(){
        while(true){
            transform.localPosition = Vector3.MoveTowards(transform.localPosition,posTowards,velocMov*10);
            transform.localScale = Vector3.MoveTowards(transform.localScale,scaleMax,velocMov);
            float nuevoAlpha = Mathf.Lerp(alpha.color.a,0,alphaValue);
            alpha.color = new Color(alpha.color.r,alpha.color.g,alpha.color.b,nuevoAlpha);
            alphaValue += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
