using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (fileName = "New ShopItem",menuName="Tienda/Shop Item")]
public class ShopItem : ItemProps
{
    // Variables necesarias
    private Color colorMaxLvl = new Color(0.5f,0.3f,0.3f,1f);
    [SerializeField] private ItemType typeItem;
    [SerializeField] private GameObject powerUpNot;

    [SerializeField] private GameObject errorBuying;

    [SerializeField] private float modiffierUsage;

    [SerializeField] private bool haveMaxLvl = false;

    // Función encargada de manejar los distintos items y sus efectos
    public override void efectoItem(NavigationHandler nav, CustomButton button)
    {

        bool efectoAplicado = false;
        Vector3 posPower = nav.getOwnerUI().transform.position;
        Quaternion rotPower = nav.getOwnerUI().transform.rotation;

        //base.efectoItem();
        // Health Items
        if(button.getMoneyModCostButton()<=nav.getOwnerUI().GetComponent<PlayerStats>().getCashPlayer()){

            if((int)typeItem == (int) ItemType.Health){
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().changeCtdaBotiquines((int) getModifier());
            }
            // Speed Items
            if((int)typeItem == (int) ItemType.Speed){
                haveMaxLvl = true;
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().plusSpeed(getModifier());
            }
            // Damage Items
            if((int)typeItem == (int) ItemType.Damage1stShoot){
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().plusDamage(getModifier());
            }
            // Shoot Delay Items
            if((int)typeItem == (int) ItemType.CoolDown1stShoot){
                haveMaxLvl = true;
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().lessBulletCoolDown(getModifier());
            }
            // Repair Nucleo Items
            if((int)typeItem == (int) ItemType.RepairNucleo){
                GameObject nucleo = GameObject.FindGameObjectWithTag("Nucleo");
                efectoAplicado = nucleo.GetComponent<NucleoStats>().plusHealth(getModifier());

                posPower = nucleo.transform.position;
                rotPower = nucleo.transform.rotation;
            }
            // Expand Shop
            if((int)typeItem == (int) ItemType.ExpandShop){
                GameObject tienda = GameObject.FindGameObjectWithTag("Shop");
                efectoAplicado = tienda.GetComponent<ShopArea>().changeSize(getModifier());
            }

            // CoolDown 2nd Disparo
            if((int)typeItem == (int) ItemType.CoolDown2ndShoot){
                haveMaxLvl = true;
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().lessBulletCoolDown2nd(getModifier());
            }

            // Radio de Empuje 2nd Disparo
            if((int)typeItem == (int) ItemType.Radio2ndShoot){
                efectoAplicado = nav.getOwnerUI().GetComponent<PlayerStats>().addRadioEmpuje(getModifier());
            }


            if(efectoAplicado){
                nav.getOwnerUI().GetComponent<PlayerStats>().lessCashPlayer((int) button.getMoneyModCostButton());
                nav.updateMoneyUI();

                GameObject instancePowerUp = Instantiate(powerUpNot,posPower,rotPower);
                instancePowerUp.GetComponent<AudioSource>().Play();
                instancePowerUp.GetComponent<SpriteRenderer>().sprite = getIcono();

                button.changeMoneyCostButton(modiffierUsage);

                Debug.Log("Efecto aplicado");

            } else{
                Debug.Log("Nivel Máximo Alcanzado");
                if(haveMaxLvl){
                    button.GetComponent<Image>().color = colorMaxLvl;
                }
                GameObject instanceError = Instantiate(errorBuying,posPower,rotPower);
                instanceError.GetComponent<AudioSource>().Play();
            }

        }else{
            Debug.Log("no hay suficiente dinero para comprarlo");
            GameObject instanceError = Instantiate(errorBuying,posPower,rotPower);
            instanceError.GetComponent<AudioSource>().Play();
        }

    }

}

// Enum encargado de manejar los tipos de items
public enum ItemType {Health,Speed,Damage1stShoot,Radio2ndShoot,CoolDown1stShoot,CoolDown2ndShoot,RepairNucleo,ExpandShop}
