using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterUI : BaseUI, ISingleOpenUI
{ 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Hunter UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Hunter UI Closed!");
    }
}
