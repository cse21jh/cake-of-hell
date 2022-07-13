using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageComponent : UIComponent
{
    public string PageName { get; set; }
    public Transform Container { get; }

    public PageComponent(Transform parent, string pageName) 
    : base(parent, Resources.Load<GameObject>("Prefabs/4col-PagePrefab"))
    {
        PageName = pageName;
        Container = gameObject.transform.GetChild(0).GetChild(0);
    }
}
