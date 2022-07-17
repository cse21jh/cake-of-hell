using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    public bool IsClickable { get; set; }
    public _Item SlotItem { get; set; }
    public GameObject ItemSlotButton { get; set; }

    public ItemSlotComponent(Transform parent, _Item item, int itemCount, bool isClickable = false) 
    : base(parent, Resources.Load<GameObject>("Prefabs/ItemSlotPrefab"))
    {
        SlotItem = item;
        IsClickable = isClickable;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = SlotItem.SpriteImage;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
        if(isClickable) {
            ItemSlotButton = Object.Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlotButtonPrefab"), gameObject.transform);
            ItemSlotButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ItemSlotButton.transform.SetParent(gameObject.transform);
        }
    }

    public void LoadItem(_Item item, int itemCount) 
    {
        Debug.Log("Loaded!");
        SlotItem = item;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = SlotItem.SpriteImage;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
    }

    public void SetOnClick(System.Action onClick) 
    {
        ItemSlotButton.GetComponent<Button>().onClick.AddListener(() => onClick());
    }
}