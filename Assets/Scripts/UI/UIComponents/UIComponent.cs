using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIComponent
{
    private GameObject gameObject;

    public UIComponent(GameObject parent, GameObject prefab)
    {
        gameObject = Object.Instantiate(prefab, parent.transform);
        gameObject.transform.SetParent(parent.transform);
    }

    public GameObject getObject() 
    {
        return gameObject;
    }
}
