using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    private Sprite[] tutorialBackgroundArr;
    private int num = 0;

    public ClickUI[] ScreenTouch;
    public GameObject TutorialScreen;


    void Awake()
    {
        tutorialBackgroundArr = ResourceLoader.GetPackedSprite("Sprites/Background/tutorial");
        ShowTutorial();
    }

    private void ShowTutorial()
    {
        ScreenTouch[0].AddListenerOnly(() =>
        {
            if (num > 0) num -= 1;
            ShowNextBackground(num);
        });
        ScreenTouch[1].AddListenerOnly(() =>
        {
            if (num == tutorialBackgroundArr.Length - 1)
            {
                SaveManager.Instance.JsonSave();
                UiManager.Instance.alreadyOpenItemList = false;
                GameManager.Instance.canMove = true;
                TimeManager.Instance.StartDay();
            }
            if (num < tutorialBackgroundArr.Length - 1) num += 1;
            ShowNextBackground(num);
        });
    }

    private void ShowNextBackground(int n)
    {
        TutorialScreen.GetComponent<Image>().sprite = tutorialBackgroundArr[n];
        SoundManager.Instance.PlayEffect("Click");
        ShowTutorial();
    }
}