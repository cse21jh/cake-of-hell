using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI, ISingleOpenUI
{
    public static bool pauseState = false;
    public GameObject MenuCanvas;
    public OptionUI OptionMenu;

    
    public void Resume(){
        MenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        pauseState = false;
    }

    public void Pause(){
        MenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        pauseState = true;
    }

    public void Restart(){
        Close();
        GameManager.Instance.ReStart();
    }

    public void Option(){
        OptionMenu.Open();
        Close();
    }

    public void Quit(){
        Application.Quit();
    }

    public override void Open()
    {
        MenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Menu UI Opened!");
    }

    public override void Close()
    {
        MenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Menu UI Closed!");
    }
}
