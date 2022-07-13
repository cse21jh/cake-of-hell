using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent
{
    public GameObject gameObject { get; set; }

    public UIComponent(Transform parent, GameObject prefab)
    {
        gameObject = Object.Instantiate(prefab, parent);
        gameObject.transform.SetParent(parent);
    }

    public void SetActive(bool active) 
    {
        gameObject.SetActive(active);
    }
}
