using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<GameObject> playersActive;
    [SerializeField] private List<WeatherEffect> weatherChanges;
    [SerializeField] private EnemySpawnManager[] enemiesSpawn;
    [SerializeField] private NucleoStats nucleo;
    [SerializeField] private ActivationArea activationArea;

    [SerializeField] private PlayerManager playerMang;

    [SerializeField] private RespawnArea respawnArea;

    [SerializeField] private BossControl bossActive;

    [SerializeField] private GameObject deathScreen, winScreen;

    [SerializeField] private int rondaActual,rondasMax;

    [SerializeField] private float enemigosActuales;

    [SerializeField] private bool climaChecked,weatherExist;

    [SerializeField] private Vector2 respawnPlayerPos;

    [SerializeField] private GameObject bossCanvas, bossAlertFinal;

    [SerializeField] private ModoJuego modoJuego; 

    [Header("Enemy Config Spawn")]
    [SerializeField] private int MaxLimitEntities;
    [SerializeField] private float enemigosMax;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;
    [SerializeField] private float dinero;

    [Header("UI Round Config")]
    [SerializeField] private BGMManager BGMMusicaMan;
    [SerializeField] private GameObject rondaCanvas;
    [SerializeField] private GameObject canvasNegro;
    [SerializeField] private string[] clavesTextoRonda;
    [SerializeField] private TextMeshProUGUI textRonda;
    [SerializeField] private TextMeshProUGUI textClimaTitle;
    [SerializeField] private TextMeshProUGUI textClimaDescrip;
    [SerializeField] private bool rondaInit,rondaSpawn,rondaFinal;

    [Header("Endless Config")]

    [SerializeField] private int[] umbralesDificultad;
    [SerializeField] private int dificultadActual;
    [SerializeField] private GameObject roundEndCanvas;
    [SerializeField] private float roundTimer;
    [SerializeField] private float roundTimerInit;
    [SerializeField] private float roundBetween;
    [SerializeField] private float roundBetweenInit;
    [SerializeField] private bool roundGoing,startTimer,startTimerBetween;
    [SerializeField] private bool inEndless;
    [SerializeField] private TimerManager timeMang;
    
    //[SerializeField] private float radioVision;
    //[SerializeField] private float radioAtaque;
    //[SerializeField] private float coolDown;

   // Variables necesarias
    [SerializeField] private LocalisationSystem lenSystemRef;

    void Start(){

        lenSystemRef = GameObject.Find("LocalisationSystem").GetComponent<LocalisationSystem>();
        eventInit();

        CursorConfig cursor = FindObjectOfType<CursorConfig>();
        cursor.confinarCursor();
        cursor.mostrarCursor(false);

        startTimer = false;
        startTimerBetween = true;

        if(textClimaTitle!= null){
            updateTextLanguageVariant();
        }


        // Se activan los spawners si es que existen
        findEnemiesSpawn();
        if(enemiesSpawn.Length!=0){
            StartCoroutine(spawnEnemiesAtOrder());
            updateEnemyStats();
        }
            
        // Corrutinas que controlan distintos aspectos de la partida
        if((int) modoJuego == (int) ModoJuego.StoryMode){   
            StartCoroutine(revisionProgreso());
            StartCoroutine(revisarPerderNucleo());
            StartCoroutine(revisarVictoriaNucleo());
            StartCoroutine(revisarRondaFinal());
        }
        if((int) modoJuego == (int) ModoJuego.Endless){   
            roundTimer = roundTimerInit;
            roundBetween = roundBetweenInit;
            roundGoing = false;
            StartCoroutine(revisarRonda());
            StartCoroutine(revisarDerrota());
        }
    }

    // Función onEnable
    void eventInit(){
        if(textClimaTitle!=null){
            lenSystemRef.onIdiomaChange += updateTextLanguageVariant;
        }
        
    }

    // Función onDisable 
    void OnDisable(){
        if(textClimaTitle!=null){
            lenSystemRef.onIdiomaChange -= updateTextLanguageVariant;
        }
    }

    // Función que permite obtener y setear el valor del texto asignado
    private void updateTextLanguageVariant(){
        textRonda.text = lenSystemRef.getLanguajeText(clavesTextoRonda[0]) +" "+ rondaActual;
        textClimaTitle.text = lenSystemRef.getLanguajeText(clavesTextoRonda[1]);
        textClimaDescrip.text = lenSystemRef.getLanguajeText(clavesTextoRonda[2]);
    }

    // Función que permite obtener el valor del timer de la ronda
    public float getRondaTimer(){
        return roundTimer;
    }

    // Función que permite obtener el valor del timer entre ronda
    public float getRondaBetweenTimer(){
        return roundBetween;
    }

    // Función que cambia el valor de rondaFinal
    public void setRondaFinal(bool value){
        rondaFinal = value;
    }

    // Función que cambia el valor de inEndless
    public void setInEndless(bool value){
        inEndless = value;
    }

    // Función que setea el valor de la rondaActual del Endless
    public void setRondaGoing(bool value){
        roundGoing = value;
    }

    // Función que actualiza a los jugadores en campo
    public void updateListPlayers(List<GameObject> lista){
        playersActive = lista;
    }

    // Función encargada de actualizar los parámetros de los enemigos para los spawners
    private void updateEnemyStats(){
        for (int i = 0; i < enemiesSpawn.Length; i++)
        {
            enemiesSpawn[i].updateStats(speed,damage,health,maxHealth, (int) dinero);
        }
    }

    // Función encargada de actualizar los parámetros más específicos de los enemigos
    private void updateEnemySpecificStats(){
        for (int i = 0; i < enemiesSpawn.Length; i++)
        {
            enemiesSpawn[i].updateSpecificStats(20f);
        }
    }

    // Función que obtiene los spawners enemigos del mapa
    private void findEnemiesSpawn(){
        // Se detectan los spawns de los distintos enemigos
        GameObject[] spawns = GameObject.FindGameObjectsWithTag("EnemyRespawn");
        enemiesSpawn = new EnemySpawnManager[spawns.Length];
        for (int i = 0; i < spawns.Length; i++)
        {  
            enemiesSpawn[i] = spawns[i].GetComponent<EnemySpawnManager>();
        } 
    }

    // Corrutina encargada de revisar la ejecución de la ronda final
    IEnumerator revisarRondaFinal(){
        while(true){
            if(rondaFinal){
                updateEnemySpecificStats();
                StartCoroutine(spawnEnemiesWOConditions());
                break;
            }
            yield return null;
        }
        yield break;
    }

    // Corrutina encargada de revisar la ronda actual del Endless
    IEnumerator revisarRonda(){
        while(true){
            if(inEndless){
                if(roundGoing){
                    if(!rondaInit){
                        StartCoroutine(showRoundScreen());
                    }
                    checkTimer();

                } else{
                    checkBetweenTimer();
                }
            }
            yield return null;
        }
    }

    // Función encargada de revisar el tiempo entre ronda
    private void checkBetweenTimer(){
        if(!startTimerBetween){
            return;
        }

        roundBetween -= Time.deltaTime;
        timeMang.updateTimerBetweenUI(roundBetween);
        if(roundBetween<=0){
            roundGoing = true;
            timeMang.updateTimerBetweenUI(0f);
            roundBetween = roundBetweenInit;
        }
    }

    // Función encargada de revisar el tiempo de ronda
    private void checkTimer(){
        if(!startTimer){
            return;
        }
        roundTimer -= Time.deltaTime;

        timeMang.updateTimerUI(roundTimer);

        if(roundTimer<=0){

            verificarDificultad();

            rondaActual ++;

            timeMang.updateTimerUI(0f);

            roundTimerInit += roundTimerInit*0.05f;
            roundTimer = roundTimerInit;
            roundBetween = roundBetweenInit;

            roundGoing = false;
            startTimer = false;
            rondaInit = false;
            rondaSpawn = false;
            climaChecked = false;

            respawnArea.setCanRespawn(true);

            BGMMusicaMan.activateBGM(BGMType.Normal);

            BGMMusicaMan.playEffect(0);
            
            Debug.Log("Terminó la ronda");

            StartCoroutine(showRoundEnd());

            nucleo.pulseNucleo();

            timeMang.timerUIMag(false);

            destroyEveryEnemy();

            aumentarDificultad();
        }
    }

    // Función encargada de calcular la dificultad actual de la partida
    private void verificarDificultad(){
        if(dificultadActual!=umbralesDificultad.Length){
            if(rondaActual == umbralesDificultad[dificultadActual]){
                dificultadActual++;
            }
        }
        
    }

    // Corrutina encargada de revisar la condición de derrota por destrucción del núcleo
    IEnumerator revisarPerderNucleo(){
        while(true){
            if(nucleo.getDestroyed() || allPlayersDeath()){
                Debug.Log("Restart partida");
                Debug.Log("Mostrar Pantalla de muerte");
                // No se puede revisar el estado del núcleo hasta un reseteo completo o temporal
                StartCoroutine(showDeathScreen());

                // Se permite accionar botones a los jugadores
                permitirAcciones();

                nucleo.setActivated(false);
                destroyEveryEnemy();
                nucleo.pulseNucleo();
                break;
            }
            yield return null;
        }
        yield break;
    }

    // Corrutina encargada de revisar la condición de derrota para todos los players muertos Endless
    IEnumerator revisarDerrota(){
        while(true){
            if(allPlayersDeath()){
                Debug.Log("Restart partida");
                Debug.Log("Mostrar Pantalla de muerte");

                inEndless = false;
                rondaSpawn = false;

                // Se muestran los scores
                FindObjectOfType<EstadisticasManager>().updateScore(rondaActual);
                FindObjectOfType<EstadisticasManager>().showScoreBoard(true);

                // Se detiene el movimiento de cualquier entidad con aggro de ese momento (evitar colisiones y lag)
                stopEnemies();

                BGMMusicaMan.playEffect(1);
                BGMMusicaMan.activateBGM(BGMType.Muerte);

                // Se permite accionar botones a los jugadores
                //permitirAcciones();

                break;
            }
            yield return null;
        }
        yield break;
    }

    // Función encargada detener a los enemigos que aún tengan aggro para evitar colisiones y lag innecesario posterior a la muerte
    private void stopEnemies(){
        if(enemiesSpawn.Length!=0){
            for (int i = 0; i < enemiesSpawn.Length; i++)
            {
                enemiesSpawn[i].GetComponent<EnemySpawnManager>().stopMovementeEntities();
            }
        }
    }

    // Función que revisa el estado vivo o muerto de todos los jugadores en campo
    private bool allPlayersDeath(){
        int contador = 0;
        List<GameObject> jugadoresActivos = playerMang.getPlayers();

        if(jugadoresActivos.Count == 0){
            return false;
        }

        for (int i = 0; i < jugadoresActivos.Count; i++)
        {
            if(!jugadoresActivos[i].GetComponent<PlayerStats>().isPlayerAlive()){
                contador++;
            }
        }
        if(contador == jugadoresActivos.Count){
            return true;
        } else{
            return false;
        }
    }

    // Corrutina encargada de revisar si se cumple la condición de victoria por núcleo
    IEnumerator revisarVictoriaNucleo(){

        while(true){
            if(nucleo.getIsEnded()){
                StartCoroutine(showWinScreen());
                yield return new WaitForSeconds(5f); 
                break;
            }
            yield return null;  
        }

        endPartida();
        yield break;
    }

    // Función que termina la partida y envía al menú
    private void endPartida(){
        SceneManager.LoadScene(0);
    }


    // Función que habilita a los jugadores a presionar en caso de tener los controles bloqueados
    private void permitirAcciones(){
        foreach (var player in playersActive){
            player.GetComponent<PlayerInput>().actions.Enable();
        }
    }

    // 

    // Corrutina encargada de mostrar la pantalla de muerte
    IEnumerator showDeathScreen(){
        StartCoroutine(changeAlpha(deathScreen,0f,1f));
        yield break;
    }

    // Corrutina encargada de revisar el progreso de la terraformación para aumentar la dificultad de las rondas
    IEnumerator revisionProgreso(){
        float progresoMaximo = nucleo.getMaxProgress();
        while(true){
            if(nucleo.getActivated()){
                
                if(!rondaInit){
                    StartCoroutine(showRoundScreen());
                }
                
                // Se acabó la ronda
                if(nucleo.getProgress()/progresoMaximo == 1){

                    nucleo.setActivated(false); 
                    nucleo.setProgress(0f);   
                    
                    rondaActual ++;

                    BGMMusicaMan.activateBGM(BGMType.Normal);

                    respawnArea.setCanRespawn(true);

                    nucleo.pulseNucleo();
                    destroyEveryEnemy();

                    aumentarDificultad();
                    nucleo.setProgressTime(nucleo.getProgressTime()-nucleo.getProgressTime()*0.1f);
                    activateBoss();

                    climaChecked = false;
                    rondaInit = false;
                    rondaSpawn = false;
                }
            }
            yield return null;  
        }
    }

    // Función que verifica si se cumplen las condiciones para entrar en batalla con el jefe
    private void activateBoss(){
        if(rondaActual == rondasMax){
            
            activationArea.setHaveKey(false);
            bossActive.deleteBlockArea();
            StartCoroutine(showBossScreen());
            weatherChanges[2].aplicarClima(this,textClimaTitle,textClimaDescrip,clavesTextoRonda);
        }
    }

    // Función que verifica una vez por ronda la aplicación de estados climáticos
    private void checkClimaAlter(){
        if(!climaChecked){
            climaChecked = true;
            
            float probInf = rondaActual/rondasMax;
            float probSup = Mathf.Clamp(rondaActual/rondasMax + 0.3f,0f,1f);

            float prob = Random.Range(probInf/2,probSup);
            
            Debug.Log(prob);

            int indexRand = 0;
            // Dia
            if(prob>0f && prob<0.5f){
                indexRand = 0;
            }
            // Soleado
            if(prob>=0.5f && prob<0.10f){
                indexRand = 1;
            }
            // Noche
            if(prob>=0.10f && prob<0.55f){
                indexRand = 2;
            }
            // Oscuridad
            if(prob>=0.55f && prob<0.65f){
                indexRand = 3;
            }
            // Lluvia
            if(prob>=0.65f && prob<0.85f){
                indexRand = 4;
            }
            // Lluvia Tormentosa
            if(prob>=0.85f && prob<1.0f){
                indexRand = 5;
            }

            weatherChanges[indexRand].aplicarClima(this,textClimaTitle,textClimaDescrip,clavesTextoRonda);
        }
    }

    // Función que permite invocar la alerta final desde el exterior
    public void showFinalAlert(){
        StartCoroutine(showAlertFinalScreen());
    }

    // Corrutina encargada de mostrar y ocultar la pantalla de alerta final
    IEnumerator showAlertFinalScreen(){
        StartCoroutine(changeAlpha(bossAlertFinal,0,1));        
        yield return new WaitForSeconds(2f);
        StartCoroutine(changeAlpha(bossAlertFinal,1,0));
        yield break;
    }

    // Corrutina encargada de mostrar y ocultar la pantalla del boss
    IEnumerator showBossScreen(){
        StartCoroutine(changeAlpha(bossCanvas,0,1));        
        yield return new WaitForSeconds(2f);
        StartCoroutine(changeAlpha(bossCanvas,1,0));
        yield break;
    }

    // Corrutina encargada de mostrar y ocultar el fin de la ronda
    IEnumerator showRoundEnd(){

        StartCoroutine(changeAlpha(roundEndCanvas,0,1));        
        yield return new WaitForSeconds(2.5f);
        StartCoroutine(changeAlpha(roundEndCanvas,1,0));
        
        timeMang.timerBetweenUIMag(true);

        startTimerBetween = true;

        yield break;
    }

    // Corrutina encargada de mostrar y ocultar la pantalla por ronda
    IEnumerator showRoundScreen(){
        rondaInit = true;

        Debug.Log("activada ronda");

        StartCoroutine(changeAlpha(canvasNegro,0,1)); 

        yield return new WaitForSeconds(1f);

        if(weatherExist){
            checkClimaAlter();
        }

        updateTextLanguageVariant();

        StartCoroutine(changeAlpha(rondaCanvas,0,1));        
        yield return new WaitForSeconds(2f);

        BGMMusicaMan.activateBGM(BGMType.Batalla);
        
        StartCoroutine(changeAlpha(canvasNegro,1,0)); 
        StartCoroutine(changeAlpha(rondaCanvas,1,0));
        
        rondaSpawn = true;

        if(timeMang!= null){
            timeMang.timerBetweenUIMag(false);
            timeMang.timerUIMag(true);
        }

        startTimer = true;
        startTimerBetween = false;

        yield break;
    }

    // Corrutina encargada de mostrar la pantalla de Victoria
    IEnumerator showWinScreen(){
        StartCoroutine(changeAlpha(winScreen,0,1));
        yield break;
    }

    // Corrutina encargada de controlar cuando y cuantos enemigos spawnear según la ronda actual
    IEnumerator spawnEnemiesAtOrder(){
        int index = 0;
        while(true){
            if(rondaSpawn){
                enemigosActuales = getEnemigosActuales();
                while(enemigosActuales<enemigosMax && rondaSpawn){
                    enemiesSpawn[index].SpawnAPeticionFromPool(dificultadActual);
                    enemigosActuales = getEnemigosActuales();

                    index +=1;                    
                    if(index == enemiesSpawn.Length){
                        index = 0;
                    }
                    yield return null;
                }
                //Debug.Log("spawner Ronda");
            }
            yield return null;
        }
    }

    // Corrutina encargada de controlar cuando y cuantos enemigos spawnear sin condicionante
    IEnumerator spawnEnemiesWOConditions(){
        int index = 0;
        while(true){
            if(!nucleo.getIsEnded()){
                enemigosActuales = getEnemigosActuales();
                while(enemigosActuales<(enemigosMax*10)){
                    enemiesSpawn[index].SpawnAPeticion();
                    enemigosActuales = getEnemigosActuales();

                    index +=1;                    
                    if(index == enemiesSpawn.Length){
                        index = 0;
                    }
                    yield return null;
                Debug.Log("spawner End");
                }
            } else{
                nucleo.pulseNucleo();
                destroyEveryEnemy();
                break;
            }
            yield return null;
        }
        yield break;
    }  

    // Función que elimina a cualquier entidad enemiga spawneada
    private void destroyEveryEnemy(){
        for (int i = 0; i < enemiesSpawn.Length; i++)
        {
            enemiesSpawn[i].killThemAll();
        }
    }

    // Función encargada de determinar la cantidad de enemigos activos 
    private int getEnemigosActuales(){
        int contador = 0;
        for (int i = 0; i < enemiesSpawn.Length; i++)
        {
            contador += enemiesSpawn[i].getEntitiesSpawnerActive();
        }
        return contador;
    }

    // Función que modifica la dificultad para la siguiente ronda
    private void aumentarDificultad(){
        speed += 0.05f*speed;
        damage += 0.05f*damage;
        health += 0.1f*health;
        maxHealth += 0.1f*maxHealth;
        enemigosMax += (0.2f*enemigosMax)*playersActive.Count;
        dinero += 0.10f*dinero;

        if(enemigosMax>MaxLimitEntities){
            enemigosMax = MaxLimitEntities;
        }

        updateEnemyStats();
    }

    // Función que modifica la dificultad en función de un parámetros solicitado
    public void aumentarDificultadCustom(float multiplier){
        speed += multiplier/2*speed;
        damage += multiplier/2*damage;
        health += multiplier/2*health;
        maxHealth += multiplier/2*maxHealth;
        enemigosMax += (multiplier*enemigosMax)*playersActive.Count;

        if(enemigosMax>MaxLimitEntities){
            enemigosMax = MaxLimitEntities;
        }

        updateEnemyStats();
    }

    // Función que maneja en que momento se puede restaurar la partida
    public void restartPartida(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Corrutina encargada de cambiar el alpha de un objeto determinado
    IEnumerator changeAlpha(GameObject target,float initValue,float endValue){
        float alphaActual = target.GetComponent<CanvasGroup>().alpha;
        float porcentaje = 0f;
        float error = 0.02f;
        while(true){

            alphaActual = Mathf.Lerp(initValue,endValue,porcentaje);
            target.GetComponent<CanvasGroup>().alpha = alphaActual;
            
            porcentaje += 0.01f;

            if(Mathf.Abs(target.GetComponent<CanvasGroup>().alpha - endValue) <= error){
                break;
            }

            yield return new WaitForSeconds(0.02f);
        }
        target.GetComponent<CanvasGroup>().alpha = endValue;
        yield break;
    }

}


// Enum encargado de manejar los tipos de modos de juego
public enum ModoJuego {StoryMode,Endless}
