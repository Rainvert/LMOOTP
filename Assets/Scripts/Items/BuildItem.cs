using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "New BuildItem",menuName="Tienda/Build Item")]
public class BuildItem : ItemProps
{
    // Variables necesarias
    [SerializeField] private GameObject preFab;
    [SerializeField] private GameObject finalObj;
    [SerializeField] private float modiffierUsage = 0.05f;

    // Función encargada de mostrar el placeholder para las construcciones
    public override void efectoItem(NavigationHandler nav, CustomButton button){
        
        //Debug.Log("Se activó el item "+getName());
        Vector3 posicion = nav.getOwnerUI().transform.position;
        posicion.x = Mathf.Round(posicion.x);
        posicion.y = Mathf.Round(posicion.y);
        GameObject placeHolder = Instantiate(preFab,posicion,nav.getOwnerUI().transform.rotation);

        placeHolder.GetComponent<GridBuild>().setFinalObj(finalObj);
        placeHolder.GetComponent<GridBuild>().setNavUI(nav);
        placeHolder.GetComponent<GridBuild>().setButtonOrigin(button);
        placeHolder.GetComponent<GridBuild>().setCosto(button.getMoneyModCostButton(),modiffierUsage);
        placeHolder.GetComponent<GridBuild>().setPlayerGridMov(nav.getOwnerUI());
        nav.getOwnerUI().GetComponent<ChangeControl>().setPlaceHolderBuilder(placeHolder);
        nav.getOwnerUI().GetComponent<ChangeControl>().activateConstructionMode();

    }
}
