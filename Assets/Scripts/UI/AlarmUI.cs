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
    private Queue<string> alarmQueue = new Queue<string>();


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

    public void SetLongText(List<string> texts)
    {
        foreach (string text in texts)
        {
            alarmQueue.Enqueue(text);
        }
        button.GetComponent<Button>().onClick.AddListener(ShowNext);
        ShowNext();
    }

    private void ShowNext()
    {
        string nextAlarm;

        if (alarmQueue.TryDequeue(out nextAlarm))
        {
            SetText(nextAlarm);
            SetButtonText("확인");
        }
        else
        {
            UiManager.Instance.CloseUI(this);
        }
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
