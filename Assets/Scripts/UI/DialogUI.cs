using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : BaseUI, ISingleOpenUI
{
    void Start()
    {

    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Dialog UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Dialog UI Closed!");
    }
}
