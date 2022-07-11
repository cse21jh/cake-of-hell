using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUI : BaseUI
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
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
    }
}
