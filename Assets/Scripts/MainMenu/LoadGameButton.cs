using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadGameButton : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        LoadGame();
    }

    private void LoadGame()
    {
        if (!SaveManager.Instance.CheckSaveData())
            return;
        SaveManager.Instance.JsonLoad();
        UiManager.Instance.alreadyOpenItemList = false;
        GameManager.Instance.canMove = true;
        GameManager.Instance.LoadScene(nextScene, true);
        TimeManager.Instance.StartDay();
    }
}
