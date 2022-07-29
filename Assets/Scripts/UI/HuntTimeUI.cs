using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HuntTimeUI : BaseUI
{
    private Slider huntTimeBar;

    void Start()
    {
        huntTimeBar = gameObject.GetComponent<Slider>();
        TimeManager.Instance.huntTimeUI = this;
        TimeBarUpdate(TimeManager.Instance.GetTime());
    }

    public void TimeBarUpdate(float time)
    {
        huntTimeBar.value = 24 - time;
    }

    public void Update()
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
