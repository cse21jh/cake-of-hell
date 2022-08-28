using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlarmUI : BaseUI, ISingleOpenUI
{
    private TMP_Text alarmText;
    private TMP_Text buttonText;
    private GameObject button;

    public System.Action OnClickButton { get; set; } = null;

    void Awake()
    {
        alarmText = GameObject.Find("AlarmText").GetComponent<TMP_Text>();
        buttonText = GameObject.Find("AlarmButtonText").GetComponent<TMP_Text>();
        button = GameObject.Find("AlarmButton");
        button.GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

    public void SetText(string text)
    {
        alarmText.text = text;
    }

    public void SetButtonText(string text)
    {
        buttonText.text = text;
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }

    private void ButtonClicked()
    {
        if (OnClickButton != null) OnClickButton();
    }
}
