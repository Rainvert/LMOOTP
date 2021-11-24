using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class csvMasterLoader
{
    // Varibles que definen la separación de campos
    private TextAsset csvFile;
    private char lineaSep = '\n';
    private char initCampo = '"';
    private string[] separadorCampo = { "\",\""};

    // Función que carga el archivo csv
    public void cargarCSV(){
        csvFile = Resources.Load<TextAsset>("localisation");
    }

    // Función que separa los valores del diccionario
    public Dictionary<string,string> getValueDictionary(string idAttrib){
        
        // Se crea el diccionario
        Dictionary<string,string> diccionario = new Dictionary<string, string>();

        string[] lineas = csvFile.text.Split(lineaSep);

        int attribIndex = -1;

        // Se obtienen las cabeceras
        string[] headers = lineas[0].Split(separadorCampo,System.StringSplitOptions.None);

        // Se busca el lenguaje solicitado 
        for (int i = 0; i < headers.Length; i++){
            if(headers[i].Contains(idAttrib)){
                attribIndex = i;
                break;
            }
        }

        Regex csvRegularExp = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

        // Se obtienen los campos de cada línea
        for (int i = 1; i < lineas.Length; i++) {
            string lineaActual = lineas[i];
            string[] campos = csvRegularExp.Split(lineaActual);

            for (int j = 0; j < campos.Length; j++){
                campos[j] = campos[j].TrimStart(' ',initCampo);
                campos[j] = campos[j].TrimEnd(' ',initCampo);
            }

            if(campos.Length > attribIndex){
                var clave = campos[0];

                if(diccionario.ContainsKey(clave)){
                    continue;
                }

                var value = campos[attribIndex];
                
                diccionario.Add(clave,value);
            }
        }
        
        return diccionario;
    }
}
