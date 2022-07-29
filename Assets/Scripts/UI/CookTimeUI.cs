using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookTimeUI : BaseUI
{
    private Slider cookTimeBar;

    void Start()
    {
        cookTimeBar = gameObject.GetComponent<Slider>();
        TimeManager.Instance.cookTimeUI = this;
        TimeBarUpdate(TimeManager.Instance.GetTime());
    }

    public void TimeBarUpdate(float time)
    {
        cookTimeBar.value = 24 - time;
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
