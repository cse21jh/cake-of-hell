using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    private Player player;
    public Vector3 startPoint;

    // About Ending Or UnLock
    private int NumberOfSoldCake = 0;
    private int NumberOfSatisfiedCustomer = 0;

    public bool canMove = true;

    //About UnLock
    public bool UnlockMapC;
    public bool UnlockMapB;
    public bool UnlockMapA;
    public bool UnlockMapS;
    public bool UnlockMapSS;

    public bool OrderWithKeyword;
    public bool OrderWithKeywordOrFlavor;
    public bool OrderWithKeywordAndFlavor;

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

    public void LoadScene(string nextScene, bool checkStartPoint = false )
    {
        SoundManager.Instance.PlayEffect("MoveScene");
        SceneManager.LoadScene(nextScene);
        if (nextScene.Contains("Shop")) // input ShopName
        {
            PlayerManager.Instance.SetPlayerInShop(true);
            if(checkStartPoint)
            {
                player.transform.position = startPoint;
            }
        }
        else
        {
            PlayerManager.Instance.SetPlayerInShop(false);
        }
    }

    public void AddNumberOfSoldCake()
    {
        NumberOfSoldCake += 1;
        CheckUnlock();
    }

    public void AddNumberOfSatisfiedCustomer()
    {
        NumberOfSatisfiedCustomer++;
    }

    public int GetNumberOfSatisfiedCustomer()
    {
        return NumberOfSatisfiedCustomer;
    }

    public void CheckUnlock()
    {
        if (!UnlockMapB && (NumberOfSoldCake >= 60 || TimeManager.Instance.GetDay() >= 3))
        {
            UnlockMapB = true;
        }
        if (!UnlockMapA && (NumberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            UnlockMapA = true;
        }
        if (!OrderWithKeywordOrFlavor && (NumberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            OrderWithKeywordOrFlavor = true;
        }
        if (!UnlockMapS && (NumberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            UnlockMapS = true;
        }
        if (!OrderWithKeywordAndFlavor && (NumberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            OrderWithKeywordAndFlavor = true;
        }
        if (!UnlockMapSS && (NumberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            UnlockMapSS = true;
        }
    }

}
