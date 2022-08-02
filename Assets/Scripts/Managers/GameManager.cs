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
        if (Input.GetKeyDown(KeyCode.F1))
        {
            TimeManager.Instance.SetDay(TimeManager.Instance.GetDay() + 1);
            Debug.Log("Day" + TimeManager.Instance.GetDay().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            foreach (var pair in ItemManager.Instance.RawItemList)
            {
                PlayerManager.Instance.SetNumberOfItem(pair.Key, PlayerManager.Instance.GetNumberOfItem(pair.Key)+99);
            }
            foreach (var pair in ItemManager.Instance.ProcessedItemList)
            {
                PlayerManager.Instance.SetNumberOfItem(pair.Key, PlayerManager.Instance.GetNumberOfItem(pair.Key) + 99);
            }
            Debug.Log("All Item +99");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            PlayerManager.Instance.SetAttackDamage(PlayerManager.Instance.GetAttackDamage() + 5);
            Debug.Log("AttackDamage" + PlayerManager.Instance.GetAttackDamage().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            PlayerManager.Instance.SetAttackDamage(PlayerManager.Instance.GetAttackDamage() - 5);
            Debug.Log("AttackDamage" + PlayerManager.Instance.GetAttackDamage().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            PlayerManager.Instance.SetHp(PlayerManager.Instance.GetHp() + 5);
            Debug.Log("Hp" + PlayerManager.Instance.GetHp().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            PlayerManager.Instance.SetHp(PlayerManager.Instance.GetHp() - 5);
            Debug.Log("Hp" + PlayerManager.Instance.GetHp().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() + 100);
            Debug.Log("Money" + PlayerManager.Instance.GetMoney().ToString());
        }
    }

    public void LoadScene(string nextScene, bool checkStartPoint = false )
    {
        SoundManager.Instance.PlayEffect("MoveScene");
        SceneManager.LoadScene(nextScene);
        canMove = true;
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

    public void CheckEnding()
    {
        Debug.Log("Ending");
    }
}
