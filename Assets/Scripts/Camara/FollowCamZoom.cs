using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamZoom : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<Transform> players;

    [SerializeField] private Vector3 offsetCam;

    [SerializeField] private Camera cam;

    [SerializeField] private float zoomValue;

    private Vector3 velocidad;
    private float suavizado = 0.3f;

    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 8f;
    private float limite = 10f;

    void Start(){
        cam = this.GetComponent<Camera>();
    }

    // Función que actualiza los jugadores en cámara
    public void updateListT(List<GameObject> playersObject){
        List<Transform> playersTrans = new List<Transform>();
        foreach (var player in playersObject)
        {
            playersTrans.Add(player.transform);
        }
        players = playersTrans;
    }

    // Función que agrega un jugador al seguimiento con la cámara
    public void addPlayerFollowCam(Transform playerPos){
        players.Add(playerPos);
    }

    // Función que remueve a un jugador del seguimiento de la cámara
    public void removePlayerFollowCam(Transform playerPos){
        players.Remove(playerPos);
    }

    // Función encargada de retornar el tamaño de zoom de la cámara actualmente
    public float getZoomValue(){
        return zoomValue;
    }

    // Función que reposiciona la cámara
    void LateUpdate(){

        if(players.Count == 0){
            return;
        }
        moverCamara();
        zoomCamara();
    }

    // Función encargada de seguir a los jugadores activos
    private void moverCamara(){
        Vector3 puntoCentral = ObtenerCenterPlayers();
        Vector3 nuevaPos = puntoCentral + offsetCam;
        transform.position = Vector3.SmoothDamp(transform.position,nuevaPos, ref velocidad,suavizado);
    }

    // Función encargada de aumentar o disminuir el zoom de la camara
    private void zoomCamara(){
        float nuevoZoom = Mathf.Lerp(minZoom,maxZoom,obtenerDistMax()/limite);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize,nuevoZoom,Time.deltaTime);
        zoomValue = cam.orthographicSize;
    }

    // Función que obtiene la distancia más larga entre los jugadores
    private float obtenerDistMax(){
        var bounds = new Bounds(players[0].position,Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.size.x;
    }

    // Función que retorna el centro de los jugadores activos
    private Vector3 ObtenerCenterPlayers(){
        if(players.Count == 1){
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position,Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    }

    // Función que permite agitar la cámara externamente dependiendo de la distancia del objeto
    public void shakeCamDistance(Vector3 distancia, float tiempoMaximo){
        StopAllCoroutines();
        StartCoroutine(shakeCamera(distancia,tiempoMaximo));
    }

// Función que permite agitar la cámara externamente dependiendo de la potencia entregada
    public void shakeCamCustomPower(float potencia, float tiempoMaximo){
        StopAllCoroutines();
        StartCoroutine(shakeCameraCustom(potencia,tiempoMaximo));
    }

    // Corrutina encargada de agitar la camára
    IEnumerator shakeCamera(Vector3 distancia, float tiempoMaximo){
        
        Vector3 posicionOriginal = transform.position;
        posicionOriginal.z = 0;

        float totalAgitado = 0f;

        float distanciaExplision = Vector3.Distance(posicionOriginal,distancia);
        if(distanciaExplision<=1){
            distanciaExplision = 1f;
        }
        
        while(totalAgitado<tiempoMaximo){
            Vector3 movimiento = new Vector3(Random.Range(-0.05f,0.05f),Random.Range(-0.05f,0.05f))/distanciaExplision + transform.position;

            transform.position = movimiento;

            totalAgitado += Time.deltaTime;

            yield return null;
        }

        yield break;
    }

    // Corrutina encargada de agitar la camára por el player
    IEnumerator shakeCameraCustom(float potencia, float tiempoMaximo){

        float totalAgitado = 0f;
        
        while(totalAgitado<tiempoMaximo){
            Vector3 movimiento = new Vector3(Random.Range(-0.1f,0.1f),Random.Range(-0.1f,0.1f))*potencia + transform.position;

            transform.position = movimiento;

            totalAgitado += Time.deltaTime;

            yield return null;
        }

        yield break;
    }

}
