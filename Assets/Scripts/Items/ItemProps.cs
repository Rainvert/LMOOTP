using UnityEngine;

[CreateAssetMenu(fileName="Nuevo Item",menuName="Tienda/Item")]
public class ItemProps : ScriptableObject
{
    // Variables que definen las propiedades de un item
    [SerializeField] private string itemName;
    [SerializeField] private string claveDescripcion;
    [TextArea] [SerializeField] private string description;
    [SerializeField] private Sprite icono;
    [SerializeField] private int precioTienda;
    [SerializeField] private float effectModifier;

    // Función virtual que define el uso que tendrá un item
    public virtual void efectoItem(NavigationHandler nav, CustomButton buttons){
        Debug.Log("Item creado: "+ itemName);
    }

    // Función que permite obtener el nombre del Item
    public string getName(){
        return itemName;
    }

    // Función que permite obtener la descripción del Item
    public string getDescription(){
        return description;
    }

    // Función que permite obtener la clave para el texto dependiente de idioma
    public string getClaveTextItem(){
        return claveDescripcion;
    }

    // Función que permite obtener el icono del item
    public Sprite getIcono(){
        return icono;
    }

    // Función que permite obtener el precio del item
    public int getPrecio(){
        return precioTienda;
    }

    // Función que permite obtener el modificador del item
    public float getModifier(){
        return effectModifier;
    }

}
