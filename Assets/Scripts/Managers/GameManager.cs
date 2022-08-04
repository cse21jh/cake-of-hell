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

    public bool orderWithKeyword;
    public bool orderWithKeywordOrFlavor;
    public bool orderWithKeywordAndFlavor;

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
        numberOfSoldCake += 1;
        CheckUnlock();
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
        dieCount ++ ;
        if(dieCount ==3)
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
            foreach (var monster in monsterInMapC)
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

        if (!unlockMapB && (numberOfSoldCake >= 60 || TimeManager.Instance.GetDay() >= 3))
        {
            unlockMapB = true;
            foreach (var monster in monsterInMapB)
            {
                foreach(var rawItemCode in monster.GetItemCode())
                {
                    unlockRawCode.Add(rawItemCode);
                    foreach(var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch(processedItemCode/1000)
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
        if (!unlockMapA && (numberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            unlockMapA = true;
            foreach (var monster in monsterInMapA)
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
        if (!orderWithKeywordOrFlavor && (numberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            orderWithKeywordOrFlavor = true;
        }
        if (!unlockMapS && (numberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            unlockMapS = true;
            foreach (var monster in monsterInMapS)
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
        if (!orderWithKeywordAndFlavor && (numberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            orderWithKeywordAndFlavor = true;
        }
        if (!unlockMapSS && (numberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            unlockMapSS = true;
            foreach (var monster in monsterInMapSS)
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
    }

    private void AddMonsterInMap()
    {
        monsterInMapC.Add(Resources.Load<GameObject>("Prefabs/Monster/MapC/Mermaid").GetComponent<Mermaid>());

        monsterInMapA.Add(Resources.Load<GameObject>("Prefabs/Monster/MapA/Rhino").GetComponent<Rhino>());
    }
}
