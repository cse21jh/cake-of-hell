using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    private Player player;
    public Vector3 startPoint;
    public bool canMove = true;

    // About Ending Or UnLock
    private int numberOfSoldCake = 0;
    private int numberOfSatisfiedCustomer = 0;

    private int dieCount;
    private int killMonsterCount;
    private int killSSMonsterCount; 

    //About UnLock
    public bool unlockMapC;
    public bool unlockMapB;
    public bool unlockMapA;
    public bool unlockMapS;
    public bool unlockMapSS;

    public int orderSystem=0;

    public List<Monster> monsterInMapC = new List<Monster>();
    public List<Monster> monsterInMapB = new List<Monster>();
    public List<Monster> monsterInMapA = new List<Monster>();
    public List<Monster> monsterInMapS = new List<Monster>();
    public List<Monster> monsterInMapSS = new List<Monster>();

    public List<int> unlockBaseCode = new List<int>();
    public List<int> unlockIcingCode = new List<int>();
    public List<int> unlockToppingCode = new List<int>();
    public List<int> unlockRawCode = new List<int>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AddMonsterInMap();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

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
                PlayerManager.Instance.SetNumberOfItem(pair.Key, PlayerManager.Instance.GetNumberOfItem(pair.Key) + 99);
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

    public void LoadScene(string nextScene, bool checkStartPoint = false)
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
        numberOfSoldCake++;
    }

    public void AddNumberOfSatisfiedCustomer()
    {
        numberOfSatisfiedCustomer++;
    }

    public int GetNumberOfSatisfiedCustomer()
    {
        return numberOfSatisfiedCustomer;
    }

    public void AddDieCount()
    {
        dieCount++;
        if(dieCount == 3)
        {
            CheckEnding();
        }
    }

    public void AddKillMonsterCount()
    {
        killMonsterCount++;
    }

    public void AddKillSSMonsterCount()
    {
        killSSMonsterCount++;
    }

    public void CheckEnding()
    {
        Debug.Log("Ending");
    }

    public void CheckUnlock()
    {
        if (!unlockMapC && (TimeManager.Instance.GetDay() >= 1))
        {
            unlockMapC = true;
            UnlockItemsOfMonsters(monsterInMapC);
        }

        if (!unlockMapB && (numberOfSoldCake >= 60 || TimeManager.Instance.GetDay() >= 3))
        {
            unlockMapB = true;
            UnlockItemsOfMonsters(monsterInMapB);
        }

        if (!unlockMapA && (numberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            unlockMapA = true;
            UnlockItemsOfMonsters(monsterInMapA);
        }

        if (orderSystem==0 && (numberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            orderSystem = 1;
        }

        if (!unlockMapS && (numberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            unlockMapS = true;
            UnlockItemsOfMonsters(monsterInMapS);
        }

        if (orderSystem == 1 && (numberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            orderSystem = 2;
        }

        if (!unlockMapSS && (numberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            unlockMapSS = true;
            UnlockItemsOfMonsters(monsterInMapSS);
        }
    }

    private void UnlockItemsOfMonsters(List<Monster> monsters) 
    {
        foreach (var monster in monsters)
        {
            foreach (var rawItemCode in monster.GetItemCode())
            {
                unlockRawCode.Add(rawItemCode);
                foreach (var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                {
                    switch (processedItemCode / 1000)
                    {
                        case 1:
                            unlockBaseCode.Add(processedItemCode);
                            break;
                        case 2:
                            unlockIcingCode.Add(processedItemCode);
                            break;
                        case 3:
                            unlockToppingCode.Add(processedItemCode);
                            break;
                    }
                }
            }
        }
    }

    private void AddMonsterInMap()
    {
        monsterInMapC.Add(Resources.Load<GameObject>("Prefabs/Monster/MapC/Mermaid").GetComponent<Mermaid>());
        monsterInMapC.Add(Resources.Load<GameObject>("Prefabs/Monster/MapC/MudTower").GetComponent<MudTower>());

        monsterInMapB.Add(Resources.Load<GameObject>("Prefabs/Monster/MapB/Mushroom").GetComponent<Mushroom>());
        monsterInMapB.Add(Resources.Load<GameObject>("Prefabs/Monster/MapB/Tornado").GetComponent<Tornado>());


        monsterInMapA.Add(Resources.Load<GameObject>("Prefabs/Monster/MapA/Rhino").GetComponent<Rhino>());
        monsterInMapA.Add(Resources.Load<GameObject>("Prefabs/Monster/MapA/Mirror").GetComponent<Mirror>());
        monsterInMapA.Add(Resources.Load<GameObject>("Prefabs/Monster/MapA/Snake").GetComponent<Snake>());

        monsterInMapS.Add(Resources.Load<GameObject>("Prefabs/Monster/MapS/Devil").GetComponent<Devil>());
        monsterInMapS.Add(Resources.Load<GameObject>("Prefabs/Monster/MapS/Ghost").GetComponent<Ghost>());

        monsterInMapSS.Add(Resources.Load<GameObject>("Prefabs/Monster/MapSS/Spider").GetComponent<Spider>());
        monsterInMapSS.Add(Resources.Load<GameObject>("Prefabs/Monster/MapSS/Dragon").GetComponent<Dragon>());
    }
}
