using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    
    void Start()
    {
        UiManager.Instance.alreadyOpenItemList = true;
        GameManager.Instance.canMove = false;
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        StartTheGame();
    }

    private void StartTheGame()
    {
        UiManager.Instance.alreadyOpenItemList = false;
        GameManager.Instance.canMove = true;
        SceneManager.LoadScene(nextScene);
    }
}
