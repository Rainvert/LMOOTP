using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro;

[CreateAssetMenu(fileName="Nuevo Efecto Climatico",menuName="Clima/Efecto")]
public class WeatherEffect : ScriptableObject
{
    
    [Header("Weather Description")]

    [SerializeField] private string climaTitleClave;
    [SerializeField] private string climaDescripClave;

    [SerializeField] private string climaTitle;
    [SerializeField] private Color climaTextColor;
    [TextArea] [SerializeField] private string climaDescrip;

    [Header("Effect Weather Config")]
    [SerializeField] private float multiplierEffect;
    [SerializeField] private Color colorLuz;
    [SerializeField] private float ilumination;
    [SerializeField] private float lluviaRate, velocLluvia, velocSimulacion, ruido;
    [SerializeField] private ClimaEffect tipoEfecto;

    [SerializeField] private GameObject climaSound;

    // Función encargada de aplicar el efecto Correspondiente
    public void aplicarClima(GameplayManager GM, TextMeshProUGUI titulo, TextMeshProUGUI descripcion, string[] clavesText){
        GameObject luzGlobal = GameObject.FindGameObjectWithTag("LuzGlobal");
        GameObject lluvia = GameObject.FindGameObjectWithTag("Lluvia");
        luzGlobal.GetComponent<SunLightControl>().changeCustomIlumination(ilumination);
        luzGlobal.GetComponent<Light2D>().color = colorLuz;

        clavesText[1] = climaTitleClave;
        clavesText[2] = climaDescripClave;

        titulo.color = climaTextColor;
        descripcion.color = climaTextColor;

        GameObject instance = Instantiate(climaSound,GM.transform.position,GM.transform.rotation);
        Destroy(instance,4f);

        Debug.Log(tipoEfecto);

        if((int) tipoEfecto == (int) ClimaEffect.Dia){
            lluvia.GetComponent<RainControl>().setRainSystem(false);
        }
        if((int) tipoEfecto == (int) ClimaEffect.DiaSoleado){
            GM.aumentarDificultadCustom(multiplierEffect);
            lluvia.GetComponent<RainControl>().setRainSystem(false);
        }
        if((int) tipoEfecto == (int) ClimaEffect.Noche){
            lluvia.GetComponent<RainControl>().setRainSystem(false);
        }
        if((int) tipoEfecto == (int) ClimaEffect.NocheOscura){
            GM.aumentarDificultadCustom(multiplierEffect);
            lluvia.GetComponent<RainControl>().setRainSystem(false);
        }
        if((int) tipoEfecto == (int) ClimaEffect.Lluvia){
            GM.aumentarDificultadCustom(multiplierEffect);
            lluvia.GetComponent<RainControl>().setRainSystem(true);
            lluvia.GetComponent<RainControl>().updateRainParams(lluviaRate,velocLluvia,velocSimulacion, ruido);
        }
        if((int) tipoEfecto == (int) ClimaEffect.LluviaTormenta){
            GM.aumentarDificultadCustom(multiplierEffect);
            lluvia.GetComponent<RainControl>().setRainSystem(true);
            lluvia.GetComponent<RainControl>().updateRainParams(lluviaRate,velocLluvia, velocSimulacion,ruido);
        }
    }

}

// Enum encargado de manejar los tipos de Efectos climáticos
public enum ClimaEffect {Dia,DiaSoleado,Noche,NocheOscura,Lluvia,LluviaTormenta}