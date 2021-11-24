using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<GameObject> entitiesList;
    [SerializeField] private GameObject[] enemy;

    [SerializeField] private FollowCamZoom camFollowCompor;

    [SerializeField] private float offsetMaxSpeed;

    private Transform cameraTracker;
    private float speed;
    private float damage;
    private float health;
    private float maxHealth;
    private int dinero;

    [SerializeField] private float radioDeVision;

    [SerializeField] private float offsetSpawn;

    [SerializeField] private Vector3[] SpawnArea = new Vector3[4];

    void Start(){
        camFollowCompor = Camera.main.GetComponent<FollowCamZoom>();
        cameraTracker = Camera.main.transform;
    }

    // Función encargada de generar y actualizar un área de apareción para los enemigos
    public void generateAreaSpawn(){
        float vertical = camFollowCompor.getZoomValue();
        float horizontal = vertical*2.5f;

        float verticalOffset = vertical + offsetSpawn;
        float horizontalOffset = horizontal + offsetSpawn;

        // Se generan 4 puntos aleatorios para spawnear
        // Izquierda
        Vector3 leftSide = new Vector3(Random.Range(-horizontalOffset,-horizontal),Random.Range(-verticalOffset,verticalOffset),10f) + cameraTracker.position;
        // Derecha
        Vector3 rightSide = new Vector3(Random.Range(horizontal,horizontalOffset),Random.Range(-verticalOffset,verticalOffset),10f) + cameraTracker.position;
        // Superior
        Vector3 upperSide = new Vector3(Random.Range(-horizontalOffset,horizontalOffset),Random.Range(vertical,verticalOffset),10f) + cameraTracker.position;
        // Inferior
        Vector3 lowerSide = new Vector3(Random.Range(-horizontalOffset,horizontalOffset),Random.Range(-verticalOffset,-vertical),10f) + cameraTracker.position;
        
        SpawnArea[0] = leftSide;
        SpawnArea[1] = rightSide;
        SpawnArea[2] = upperSide;
        SpawnArea[3] = lowerSide;
    }

    // Función que instancia un enemigo a petición en una posición aleatoria
    public void SpawnAPeticion(){

        generateAreaSpawn();

        float offsetSpeed = Random.Range(0f,offsetMaxSpeed);

        int posIndex = Random.Range(0,4);

        GameObject instanceEnem = Instantiate(enemy[0],SpawnArea[posIndex],transform.rotation);
        instanceEnem.GetComponent<EnemyStats>().setSpawner(this);
        instanceEnem.GetComponent<EnemyStats>().addSetSpawnStats(speed+offsetSpeed,damage,health,maxHealth,dinero);
        
        if(instanceEnem.GetComponent<EnemyCompor>()!= null){
            instanceEnem.GetComponent<EnemyCompor>().setRangoDeVision(radioDeVision);
            instanceEnem.GetComponent<EnemyCompor>().setEnemyFromSpawn(true);
        }
        if(instanceEnem.GetComponent<EnemyIACompor>()!= null){
            instanceEnem.GetComponent<EnemyIACompor>().setEnemyFromSpawn(true);
        }
        if(instanceEnem.GetComponent<BossCompor>()!=null){
            instanceEnem.GetComponent<BossCompor>().setEnemyFromSpawn(true);
        }

        entitiesList.Add(instanceEnem);
    }

    // Función que detiene a la entidades y sus movimientos
    public void stopMovementeEntities(){
        for (int i = 0; i < entitiesList.Count; i++)
        {

            if(entitiesList[i].GetComponent<EnemyCompor>()!= null){
                entitiesList[i].GetComponent<EnemyCompor>().stopMovement();
            }
            if(entitiesList[i].GetComponent<EnemyIACompor>()!= null){
                entitiesList[i].GetComponent<EnemyIACompor>().stopMovement();
            }
            if(entitiesList[i].GetComponent<BossCompor>()!=null){
                entitiesList[i].GetComponent<BossCompor>().stopMovement();
            }
        }
    }

    // Función que instancia un enemigo a petición en una posición aleatoria del pool de enemigos
    public void SpawnAPeticionFromPool(int dificultad){

        generateAreaSpawn();

        float offsetSpeed = Random.Range(0f,offsetMaxSpeed);

        int posIndex = Random.Range(0,4);

        int randomIndex = pickRandomEnemy(dificultad);

        GameObject instanceEnem = Instantiate(enemy[randomIndex],SpawnArea[posIndex],transform.rotation);
        instanceEnem.GetComponent<EnemyStats>().setSpawner(this);
        instanceEnem.GetComponent<EnemyStats>().addSetSpawnStats(speed+offsetSpeed,damage,health,maxHealth,dinero);
        
        if(instanceEnem.GetComponent<EnemyCompor>()!= null){
            instanceEnem.GetComponent<EnemyCompor>().setRangoDeVision(radioDeVision);
            instanceEnem.GetComponent<EnemyCompor>().setEnemyFromSpawn(true);
        }
        if(instanceEnem.GetComponent<EnemyIACompor>()!= null){
            instanceEnem.GetComponent<EnemyIACompor>().setEnemyFromSpawn(true);
        }
        if(instanceEnem.GetComponent<BossCompor>()!=null){
            instanceEnem.GetComponent<BossCompor>().setEnemyFromSpawn(true);
        }
        
        entitiesList.Add(instanceEnem);
    }
    
    // Función que evalua la ronda y dificultad para permitir la salida de enemigos más fuertes
    public int pickRandomEnemy(int dificultad){
        bool canSpawn = false;
        float randomPerc = 0f;
        int randomNumber = 0;
        while(!canSpawn){
            randomPerc = Random.Range(0f,1f);
            if(randomPerc<=0.8f){
                randomNumber = 0;
            }
            if(randomPerc>0.8f && randomPerc<=0.95f){
                randomNumber = 1;
            }
            if(randomPerc>0.95f && randomPerc<1.0f){
                randomNumber = 2;
            }
            if(randomNumber <= dificultad){
                canSpawn = true;
            }
            Debug.Log("spawing fine");
        }
        return randomNumber;
    }

    // Función que elimina a un elemento de la lista de enemigos activos
    public void deleteEnemyDeath(GameObject enemyDeath){
        entitiesList.Remove(enemyDeath);
    }

    // Función que permite obtener el largo de la cantidad enemigos en campo
    public int getEntitiesSpawnerActive(){
        return entitiesList.Count;
    }

    // Función que elimina a todos los enemigos de este spawner
    public void killThemAll(){
        while(entitiesList.Count!=0){
            entitiesList[entitiesList.Count-1].GetComponent<EnemyStats>().decreaseHealth(1000f,null);
        }
    }

    // Función encargada de actualizar los parámetros para spawnear un enemigo
    public void updateStats(float velocidad, float daño, float vida, float maxVida, int valor){
        speed = velocidad;
        damage = daño;
        health = vida;
        maxHealth = maxVida;
        dinero = valor;
    }


    // Función encargada de actualizar los parámetros para spawnear un enemigo
    public void updateSpecificStats(float value){
        if(value !=0){
            radioDeVision = value;
        }
    }

}
