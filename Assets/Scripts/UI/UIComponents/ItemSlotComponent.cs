using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    public ItemSlotComponent(Transform parent, _Item item, int itemCount) 
    : base(parent, Resources.Load<GameObject>("Prefabs/ItemSlotPrefab"))
    {
        gameObject.transform.GetChild(0).GetComponent<Image>().sprite = item.SpriteImage;
        gameObject.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
    }
}