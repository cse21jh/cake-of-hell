using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayUI : BaseUI
{
    TextMeshProUGUI dayText;

    void Start()
    {
        TimeManager.Instance.dayUI = this;
        dayText = this.GetComponent<TextMeshProUGUI>();
        DayTextUpdate(TimeManager.Instance.GetDay());
    }

    public void DayTextUpdate(int day)
    {
        dayText.text = "Day " + day.ToString();
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
