using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUI : BaseUI, ISingleOpenUI
{
    private TMP_Text dialog;

    void Awake()
    {
        dialog = GameObject.Find("DialogText").GetComponent<TMP_Text>();
    }

    public void SetText(string text)
    {
        dialog.text = text;
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
