using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI, ISingleOpenUI
{
    public static bool pauseState = false;
    public OptionUI OptionMenu;

    void Start()
    {
        var panel = gameObject.transform.GetChild(0).transform;
        panel.GetChild(0).GetComponent<Button>().onClick.AddListener(Resume);
        panel.GetChild(1).GetComponent<Button>().onClick.AddListener(Restart);
        panel.GetChild(2).GetComponent<Button>().onClick.AddListener(Option);
        panel.GetChild(3).GetComponent<Button>().onClick.AddListener(Quit);
        panel.GetChild(4).GetComponent<Button>().onClick.AddListener(MainMenu);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseState = false;
        UiManager.Instance.CloseUI(this);
        SoundManager.Instance.PlayEffect("Click");
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pauseState = true;
    }

    public void Restart()
    {
        Resume();
        GameManager.Instance.ReStart();
        SoundManager.Instance.PlayEffect("Click");
    }

    public void Option()
    {
        UiManager.Instance.CloseUI(this);
        UiManager.Instance.OpenUI(OptionMenu);
        SoundManager.Instance.PlayEffect("Click");
    }

    public void Quit()
    {
        SoundManager.Instance.PlayEffect("Click");
        Application.Quit();
    }

    public void MainMenu()
    {
        SoundManager.Instance.PlayEffect("Click");
        GameManager.Instance.LoadScene("MainMenu");
        TimeManager.Instance.breakDay = true;
        UiManager.Instance.alreadyOpenItemList = true;
        GameManager.Instance.canMove = false;
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Menu UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Menu UI Closed!");
    }
}
