using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    private Sprite nullSprite;
    private GameObject hoverItemName;
    public int ItemCode { get; set; }
    public int ItemCount { get; set; }
    public bool IsClickable { get; set; }
    public GameObject ItemSlotButton { get; set; }

    public ItemSlotComponent(Transform parent, int itemCode, int itemCount, bool isClickable = false) 
    : base(parent, Resources.Load<GameObject>("Prefabs/ItemSlotPrefab"))
    {
        nullSprite = Resources.Load<Sprite>("Sprites/Nothing");
        LoadItem(itemCode, itemCount);
        IsClickable = isClickable;
        if(isClickable) {
            ItemSlotButton = Object.Instantiate(Resources.Load<GameObject>("Prefabs/ItemSlotButtonPrefab"), gameObject.transform);
            ItemSlotButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.0f, 0.0f, 0.0f);
            ItemSlotButton.transform.SetParent(gameObject.transform);
        }

        gameObject.GetComponent<HoverableComponent>().OnMouseEnter = (data) => OnHover(data);
        gameObject.GetComponent<HoverableComponent>().OnMouseExit = (data) => OnExit(data);
        hoverItemName = HoverItemName.Instance.gameObject;
        hoverItemName.gameObject.SetActive(false);
    }

    public void LoadItem(int itemCode, int itemCount = -1) 
    {
        ItemCode = itemCode;
        ItemCount = itemCount;
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = Util.GetItem(itemCode).SpriteImage ?? nullSprite;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = ItemCount == -1 ? "" : ItemCount.ToString();
    }

    public bool HasItem()
    {
        return ItemCode > 0;
    }

    public void Clear()
    {
        LoadItem(0);
    }

    public void AddItem(int addCount = 1) 
    {
        Util.AddItem(ItemCode, addCount);
        ItemCount += addCount;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = ItemCount.ToString();
    }

    public void UseItem(int useCount = 1)
    {
        Util.UseItem(ItemCode, useCount);
        ItemCount -= useCount;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = ItemCount.ToString();
        if(ItemCount <= 0) 
        {
            Object.Destroy(gameObject);
        }
    }

    public void SetOnClick(System.Action onClick) 
    {
        ItemSlotButton.GetComponent<Button>().onClick.AddListener(() => onClick());
    }

    public void OnHover(PointerEventData data)
    {
        if(!hoverItemName.activeSelf && HasItem())
        {
            hoverItemName.SetActive(true);
            hoverItemName.transform.GetChild(0).GetComponent<TMP_Text>().text = Util.GetItem(ItemCode).Name;
            hoverItemName.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 40);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)hoverItemName.GetComponent<ContentSizeFitter>().transform);
        }
    }

    public void OnExit(PointerEventData data)
    {
        if(hoverItemName.activeSelf)
        {
            hoverItemName.SetActive(false);
        }
    }

    public virtual void SetActive(bool active) 
    {
        gameObject.SetActive(active);
        hoverItemName.SetActive(active);
    }
}