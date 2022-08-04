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

    public List<Monster> monsterInMapC = new List<Monster>();
    public List<Monster> monsterInMapB = new List<Monster>();
    public List<Monster> monsterInMapA = new List<Monster>();
    public List<Monster> monsterInMapS = new List<Monster>();
    public List<Monster> monsterInMapSS = new List<Monster>();

    public List<int> UnlockBaseCode = new List<int>();
    public List<int> UnlockIcingCode = new List<int>();
    public List<int> UnlockToppingCode = new List<int>();
    public List<int> UnlockRawCode = new List<int>();


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


    public void CheckEnding()
    {
        Debug.Log("Ending");
    }

    public void CheckUnlock()
    {
        if (!UnlockMapC && (TimeManager.Instance.GetDay() >= 1))
        {
            UnlockMapC = true;
            foreach (var monster in monsterInMapC)
            {
                foreach (var rawItemCode in monster.GetItemCode())
                {
                    UnlockRawCode.Add(rawItemCode);
                    foreach (var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch (processedItemCode / 1000)
                        {
                            case 1:
                                UnlockBaseCode.Add(processedItemCode);
                                break;
                            case 2:
                                UnlockIcingCode.Add(processedItemCode);
                                break;
                            case 3:
                                UnlockToppingCode.Add(processedItemCode);
                                break;
                        }
                    }
                }
            }
        }

        if (!UnlockMapB && (NumberOfSoldCake >= 60 || TimeManager.Instance.GetDay() >= 3))
        {
            UnlockMapB = true;
            foreach (var monster in monsterInMapB)
            {
                foreach(var rawItemCode in monster.GetItemCode())
                {
                    UnlockRawCode.Add(rawItemCode);
                    foreach(var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch(processedItemCode/1000)
                        { 
                            case 1:
                                UnlockBaseCode.Add(processedItemCode);
                                break;
                            case 2:
                                UnlockIcingCode.Add(processedItemCode);
                                break;
                            case 3:
                                UnlockToppingCode.Add(processedItemCode);
                                break;
                        }
                    }
                }
            }
        }
        if (!UnlockMapA && (NumberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            UnlockMapA = true;
            foreach (var monster in monsterInMapA)
            {
                foreach (var rawItemCode in monster.GetItemCode())
                {
                    UnlockRawCode.Add(rawItemCode);
                    foreach (var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch (processedItemCode / 1000)
                        {
                            case 1:
                                UnlockBaseCode.Add(processedItemCode);
                                break;
                            case 2:
                                UnlockIcingCode.Add(processedItemCode);
                                break;
                            case 3:
                                UnlockToppingCode.Add(processedItemCode);
                                break;
                        }
                    }
                }
            }
        }
        if (!OrderWithKeywordOrFlavor && (NumberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            OrderWithKeywordOrFlavor = true;
        }
        if (!UnlockMapS && (NumberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            UnlockMapS = true;
            foreach (var monster in monsterInMapS)
            {
                foreach (var rawItemCode in monster.GetItemCode())
                {
                    UnlockRawCode.Add(rawItemCode);
                    foreach (var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch (processedItemCode / 1000)
                        {
                            case 1:
                                UnlockBaseCode.Add(processedItemCode);
                                break;
                            case 2:
                                UnlockIcingCode.Add(processedItemCode);
                                break;
                            case 3:
                                UnlockToppingCode.Add(processedItemCode);
                                break;
                        }
                    }
                }
            }
        }
        if (!OrderWithKeywordAndFlavor && (NumberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            OrderWithKeywordAndFlavor = true;
        }
        if (!UnlockMapSS && (NumberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            UnlockMapSS = true;
            foreach (var monster in monsterInMapSS)
            {
                foreach (var rawItemCode in monster.GetItemCode())
                {
                    UnlockRawCode.Add(rawItemCode);
                    foreach (var processedItemCode in ItemManager.Instance.GetRawItem(rawItemCode).OutputCode)
                    {
                        switch (processedItemCode / 1000)
                        {
                            case 1:
                                UnlockBaseCode.Add(processedItemCode);
                                break;
                            case 2:
                                UnlockIcingCode.Add(processedItemCode);
                                break;
                            case 3:
                                UnlockToppingCode.Add(processedItemCode);
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
