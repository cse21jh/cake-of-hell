using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    public ItemSlotComponent(GameObject parent, _Item item, int itemCount) 
    {
        GameObject componentPrefab = Resources.Load<GameObject>("Prefabs/ItemSlotPrefab");
        GameObject obj = Object.Instantiate(componentPrefab, parent.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = item.SpriteImage;
        obj.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
    }
}