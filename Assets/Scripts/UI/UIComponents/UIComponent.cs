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

    public virtual void SetActive(bool active) 
    {
        gameObject.SetActive(active);
    }

    public void SetPosition(float x, float y)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(x, y, 0.0f);
    }
}
