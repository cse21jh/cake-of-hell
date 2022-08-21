using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogUI : BaseUI, ISingleOpenUI
{
    private TMP_Text dialog;
    private GameObject yesButton, noButton;

    public System.Action OnClickYes { get; set; } = null;
    public System.Action OnClickNo { get; set; } = null;

    void Awake()
    {
        dialog = GameObject.Find("DialogText").GetComponent<TMP_Text>();
        yesButton = GameObject.Find("ButtonYes");
        noButton = GameObject.Find("ButtonNo");
        yesButton.GetComponent<Button>().onClick.AddListener(YesClicked);
        noButton.GetComponent<Button>().onClick.AddListener(NoClicked);
        yesButton.SetActive(false);
        noButton.SetActive(false);
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
        HideYesNoButtons();
        gameObject.SetActive(false);
        Debug.Log("Dialog UI Closed!");
    }

    public void ShowYesNoButtons()
    {
        yesButton.SetActive(true);
        noButton.SetActive(true);
    }

    public void HideYesNoButtons()
    {
        yesButton.SetActive(false);
        noButton.SetActive(false);
    }

    private void YesClicked()
    {
        if(OnClickYes != null) OnClickYes();
    }

    private void NoClicked()
    {
        if(OnClickNo != null) OnClickNo();
    }

    public void StartTyping(string txt, float typingSpeed = 0.05f)
    {
        StartCoroutine(Typing(txt, typingSpeed));
    }

    private IEnumerator Typing(string txt, float typingSpeed)       // 어디선가 startcoroutine
    {
        for(int i=0; i <= txt.Length; i++)
        {
            SetText(txt.Substring(0, i));
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}