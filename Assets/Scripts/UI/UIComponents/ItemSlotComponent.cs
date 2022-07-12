using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotComponent : UIComponent
{
    private GameObject obj;

    public ItemSlotComponent(GameObject parent, GameObject prefab, _Item item, int itemCount) 
    {
        obj = Object.Instantiate(prefab, parent.transform);
        obj.transform.GetChild(0).GetComponent<Image>().sprite = item.SpriteImage;
        obj.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
    }

    public void Destroy() 
    {
        Object.Destroy(obj);
    }
}