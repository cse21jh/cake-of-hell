using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PageComponent : UIComponent
{
    public string PageName { get; set; }
    public int SlotCount { get; set; }
    public Transform Container { get; }

    public PageComponent(Transform parent, string pageName, int slotCount = 4, float height = 300.0f) 
    : base(parent, Resources.Load<GameObject>("Prefabs/4col-PagePrefab"))
    {
        PageName = pageName;
        SlotCount = slotCount;
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(15 + 65 * slotCount, height);
        Container = gameObject.transform.GetChild(0).GetChild(0);
        Container.GetComponent<GridLayoutGroup>().constraintCount = slotCount;
    }
}
