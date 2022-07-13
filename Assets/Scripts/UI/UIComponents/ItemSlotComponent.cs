using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    private GameObject gameObject;

    public ItemSlotComponent(GameObject parent, GameObject prefab, _Item item, int itemCount) : base(parent, prefab)
    {
        gameObject = base.getObject();
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = item.SpriteImage;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
    }

    public void SetActive(bool active) 
    {
        gameObject.SetActive(active);
    }
}