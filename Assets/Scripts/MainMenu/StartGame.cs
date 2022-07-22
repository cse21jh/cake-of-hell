using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private string nextScene;
    
    public void OnClickExit()
    {
        StartTheGame();
    }

    private void StartTheGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}
