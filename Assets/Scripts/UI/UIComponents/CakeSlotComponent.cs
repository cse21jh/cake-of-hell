using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CakeSlotComponent : UIComponent
{
    private int baseCode;
    private int icingCode;
    private int toppingCode;

    public CakeSlotComponent(Transform parent, int _base, int _icing, int _topping) 
    : base(parent, Resources.Load<GameObject>("Prefabs/CakeSlotPrefab"))
    {
        baseCode = _base;
        icingCode = _icing;
        toppingCode = _topping;
    }
}