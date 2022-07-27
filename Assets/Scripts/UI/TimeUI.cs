using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeUI : BaseUI
{
    // Start is called before the first frame update
    void Start()
    {
        TimeManager.Instance.timeUI = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
