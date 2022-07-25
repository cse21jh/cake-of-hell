using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HoverableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool exited = true;
    public System.Action<PointerEventData> OnMouseEnter { get; set; }
    public System.Action<PointerEventData> OnMouseExit { get; set; }

    public void OnPointerEnter(PointerEventData eventData)
    {
        exited = false;
        OnMouseEnter(eventData);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!exited)
        {
            exited = true;
            OnMouseExit(eventData);
        }
    }
}
