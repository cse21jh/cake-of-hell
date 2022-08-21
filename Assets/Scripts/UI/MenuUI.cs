using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : BaseUI
{
    public static bool pauseState = false;
    public GameObject MenuCanvas;

    // Start is called before the first frame update
    void Start()
    {
        MenuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(pauseState){
                Resume();
            } else {
                Pause();
            }
        }
    }

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
        Debug.Log("미구현");
    }

    public void Option(){
        OptionUI.Start;
    }

    public void Quit(){
        Debug.Log("미구현");
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
