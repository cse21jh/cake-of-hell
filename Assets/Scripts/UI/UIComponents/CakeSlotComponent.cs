using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CakeSlotComponent : UIComponent
{
    private Sprite nullSprite;
    private int baseCode, icingCode, toppingCode;
    private Cake _cake;

    public bool IsClickable { get; set; }
    public GameObject CakeSlotButton { get; set; }

    public CakeSlotComponent(Transform parent, bool isClickable = false) 
    : base(parent, ResourceLoader.GetPrefab("Prefabs/CakeSlotPrefab"))
    {
        nullSprite = ResourceLoader.GetSprite("Sprites/Nothing");
        IsClickable = isClickable;
        if(isClickable) {
            CakeSlotButton = Object.Instantiate(ResourceLoader.GetPrefab("Prefabs/ItemSlotButtonPrefab"), gameObject.transform);
            CakeSlotButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            CakeSlotButton.transform.SetParent(gameObject.transform);
        }
    }

    public Cake GetCake()
    {
        return _cake;
    }

    public void SetCake(Cake cake)
    {
        _cake = cake;
        baseCode = cake.BaseCode;
        icingCode = cake.IcingCode;
        toppingCode = cake.ToppingCode;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = cake.BaseImage ?? nullSprite;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = cake.IcingImage ?? nullSprite;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = cake.ToppingImage ?? nullSprite;
    }

    public void Clear()
    {
        _cake = null;
        baseCode = 0;
        icingCode = 0;
        toppingCode = 0;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = nullSprite;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = nullSprite;
        gameObject.transform.GetChild(2).gameObject.GetComponent<Image>().sprite = nullSprite;
    }

    public void SetOnClick(System.Action onClick) 
    {
        CakeSlotButton.GetComponent<Button>().onClick.AddListener(() => onClick());
    }
}