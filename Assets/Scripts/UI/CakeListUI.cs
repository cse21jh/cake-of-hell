using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeListUI : BaseUI, ISingleOpenUI
{
    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Cake List UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Cake List UI Closed!");
    }
}
