using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnText : MonoBehaviour
{
    [SerializeField] private GameObject text;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            Instantiate(text,transform.position,transform.rotation);
            
        }
    }
}
