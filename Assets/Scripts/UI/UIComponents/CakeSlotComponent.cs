using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CakeSlotComponent : UIComponent
{
    private int baseCode, icingCode, toppingCode;

    public CakeSlotComponent(Transform parent) 
    : base(parent, Resources.Load<GameObject>("Prefabs/CakeSlotPrefab"))
    {
        
    }

    /*public void SetCake(int _base, int _icing, int _topping)
    {
        baseCode = _base;
        icingCode = _icing;
        toppingCode = _topping;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = Util.GetItem(baseCode).SpriteImage;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = Util.GetItem(icingCode).SpriteImage;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = Util.GetItem(toppingCode).SpriteImage;
    }*/

    public void SetCake(Cake cake)
    {
        baseCode = cake.BaseCode;
        icingCode = cake.IcingCode;
        toppingCode = cake.ToppingCode;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = cake.BaseImage;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = cake.IcingImage;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = cake.ToppingImage;
    }
}