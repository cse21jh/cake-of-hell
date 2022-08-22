using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CheckNewGame : BaseUI
{
    public string nextScene;

    [SerializeField]
    private GameObject yesButton;
    [SerializeField]
    private GameObject noButton;

    void Start()
    {
        yesButton.GetComponent<Button>().onClick.AddListener(StartTheGame);
        noButton.GetComponent<Button>().onClick.AddListener(Close);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartTheGame()
    {
        Close();
        //SaveManager.Instance.JsonSave();
        //UiManager.Instance.alreadyOpenItemList = false;
        //GameManager.Instance.canMove = true;
        //TimeManager.Instance.StartDay();
        GameManager.Instance.LoadScene("StoryScene");
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
