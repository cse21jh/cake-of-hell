using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    public int ItemCode { get; set; }
    public int ItemCount { get; set; }
    public bool IsClickable { get; set; }
    public GameObject ItemSlotButton { get; set; }

    public ItemSlotComponent(Transform parent, int itemCode, int itemCount, bool isClickable = false) 
    : base(parent, Resources.Load<GameObject>("Prefabs/ItemSlotPrefab"))
    {
        LoadItem(itemCode, itemCount);
        IsClickable = isClickable;
        if(isClickable) {
            ItemSlotButton = Object.Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlotButtonPrefab"), gameObject.transform);
            ItemSlotButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ItemSlotButton.transform.SetParent(gameObject.transform);
        }
    }

    public void LoadItem(int itemCode, int itemCount) 
    {
        ItemCode = itemCode;
        ItemCount = itemCount;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Util.GetItem(itemCode).SpriteImage;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = ItemCount == -1 ? "" : ItemCount.ToString();
    }

    public void SetOnClick(System.Action onClick) 
    {
        ItemSlotButton.GetComponent<Button>().onClick.AddListener(() => onClick());
    }
}