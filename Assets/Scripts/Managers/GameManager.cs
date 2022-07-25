using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    private Player player;

    public bool canMove = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(string nextScene)
    {
        SoundManager.Instance.PlayEffect("MoveScene");
        SceneManager.LoadScene(nextScene);
        if (nextScene.Contains("Shop")) // input ShopName
        {
            PlayerManager.Instance.SetPlayerInShop(true);
        }
        else
        {
            PlayerManager.Instance.SetPlayerInShop(false);
        }
    }
}
