using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private string nextScene;

    private GameObject CheckNewGame;
    private Canvas canvas;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>();
        CheckNewGame = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/CheckNewGame"), canvas.transform);
        CheckNewGame.GetComponent<CheckNewGame>().nextScene = nextScene;
        UiManager.Instance.alreadyOpenItemList = true;
        GameManager.Instance.canMove = false;
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        if (SaveManager.Instance.CheckSaveData())
        {
            CheckNewGame.GetComponent<CheckNewGame>().Open();
            
        }
        else
        {
            StartTheGame();
        }
    }

    private void StartTheGame()
    {
        //UiManager.Instance.alreadyOpenItemList = false;
        //GameManager.Instance.canMove = true;
        GameManager.Instance.LoadScene("StoryScene");
    }
}
