using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavHandlerMod : MonoBehaviour
{
    // Variables necesarias
    [SerializeField] private List<CustomModButton> botonesInterfaz;
    [SerializeField] private int grupoBotones;

    void Start(){
        defineButtonInteraction();
    }

    // Función encargada de tomar el listado de botones que posee y asignarles su grupo, parent y adyacentes
    public void defineButtonInteraction(){
        for (int i = 0; i < botonesInterfaz.Count; i++)
        {
            botonesInterfaz[i].setGroup(grupoBotones);
            botonesInterfaz[i].setBotonesGrupo(botonesInterfaz);
        }
    }

    // Función que permite modificar el grupo al cual pertenecen los botones, está vinculado al Player
    public void setGroupNum(int value){
        grupoBotones = value;
    }
}
