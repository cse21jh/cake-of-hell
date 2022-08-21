using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    public int penaltyCount;
    public int increaseReputationCount;

    public Vector2 startPoint;
    public bool canMove = true;

    public bool canUsePortal = true;

    public int soldCakeInADay;

    // About Ending Or UnLock
    public int numberOfSoldCake = 0;
    public int numberOfSatisfiedCustomer = 0;
    public int dieCount = 0;
    public int killMonsterCount = 0;
    public int killSSMonsterCount = 0;
    public int processCount = 0;
    public int processSSCount = 0;
    public int cantAcceptOrderCount = 0;
    public int enterBlackHoleCount = 0;

    public bool killMonsterInADay = false;
    

    public int[] killEachMonsterCount= new int[11];


    //About UnLock
    public bool unlockMapC;
    public bool unlockMapB;
    public bool unlockMapA;
    public bool unlockMapS;
    public bool unlockMapSS;

    public int orderSystem = 0;

    public List<Monster> monsterInMapC = new List<Monster>();
    public List<Monster> monsterInMapB = new List<Monster>();
    public List<Monster> monsterInMapA = new List<Monster>();
    public List<Monster> monsterInMapS = new List<Monster>();
    public List<Monster> monsterInMapSS = new List<Monster>();

    public List<int> unlockBaseCode = new List<int>();
    public List<int> unlockIcingCode = new List<int>();
    public List<int> unlockToppingCode = new List<int>();
    public List<int> unlockRawCode = new List<int>();

    //About FadeIn & Out
    private GameObject blackPanel;
    private GameObject canvas;
    private RectTransform panelRect;
    private Image panelImage;

    //About Upgrade
    public int numberOfMagicianSlot = 3;
    public int numberOfCounter = 3;
    public int numberOfCakeTable = 2;
    public int addGuestLeaveTime = 0;

    public List<Upgrade> upgradeList = new List<Upgrade>();

    public Upgrade magicianSlotUpgrade;
    public Upgrade cakeTableNumberUpgrade;
    //public Upgrade counterNumberUpgrade;
    public Upgrade guestLeaveTimeUpgrade;
    public Upgrade unlockMapBUpgrade;
    public Upgrade unlockMapAUpgrade;
    public Upgrade unlockMapSUpgrade;
    public Upgrade unlockMapSSUpgrade;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AddMonsterInMap();
        AddUpgrade();
    }

    void Start()
    {
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
            Util.AddItem(4008);
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
            PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() + 1000);
            Debug.Log("Money" + PlayerManager.Instance.GetMoney().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            TimeManager.Instance.oneHour = 1.0f;
            Debug.Log("One Hour is One Second");
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            PlayerManager.Instance.Die();
            Debug.Log("Die");
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            ReStart();
            Debug.Log("Restart");
        }
    }

    public void LoadScene(string nextScene, bool onStartPoint = false)
    {
        SoundManager.Instance.PlayEffect("MoveScene");
        SceneManager.LoadScene(nextScene);
        canMove = true;
        if (nextScene.Contains("Shop")) // input ShopName
        {
            PlayerManager.Instance.SetPlayerInShop(true);
        }
        else
        {
            PlayerManager.Instance.SetPlayerInShop(false);
        }

        if (onStartPoint)
        {
            StartCoroutine(StartOnPoint());
        }
    }

    private IEnumerator StartOnPoint()
    {
        yield return null;
        GameObject player = GameObject.FindWithTag("Player");
        player.transform.position = GameObject.Find("StartPoint").transform.position;
    }

    public void AddNumberOfSoldCake()
    {
        numberOfSoldCake++;
        soldCakeInADay++;
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
            unlockMapBUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapB);
        }

        if (!unlockMapA && (numberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            unlockMapA = true;
            unlockMapAUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapA);
        }

        if (orderSystem==0 && (numberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            orderSystem = 1;
        }

        if (!unlockMapS && (numberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            unlockMapS = true;
            unlockMapSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapS);
        }

        if (orderSystem == 1 && (numberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            orderSystem = 2;
        }

        if (!unlockMapSS && (numberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            unlockMapSS = true;
            unlockMapSSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapSS);
        }

        if(!magicianSlotUpgrade.IsUnlocked && processCount >=200)
        {
            magicianSlotUpgrade.IsUnlocked = true;
        }

        if (!unlockMapBUpgrade.IsUnlocked && TimeManager.Instance.GetDay() >= 2)
        {
            unlockMapBUpgrade.IsUnlocked = true;
        }

        if (!unlockMapAUpgrade.IsUnlocked && TimeManager.Instance.GetDay() >= 6)
        {
            unlockMapAUpgrade.IsUnlocked = true;
        }

        if (!unlockMapSUpgrade.IsUnlocked && TimeManager.Instance.GetDay() >= 13)
        {
            unlockMapSUpgrade.IsUnlocked = true;
        }

        if (!unlockMapSSUpgrade.IsUnlocked && TimeManager.Instance.GetDay() >= 24)
        {
            unlockMapSSUpgrade.IsUnlocked = true;
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

   

    private IEnumerator FadeOut()
    {
        canvas = GameObject.Find("Canvas");
        panelRect = canvas.GetComponent<RectTransform>();
        blackPanel = Instantiate(Resources.Load<GameObject>("Prefabs/Debug/BlackPanel"), new Vector2(panelRect.rect.width / 2f, panelRect.rect.height / 2f), Quaternion.identity, canvas.transform);
        panelImage = blackPanel.GetComponent<Image>();

        float alpha = 0f;
        panelImage.color = new Color(0f, 0f, 0f, alpha);
        while (alpha < 1f)
        {
            alpha += 0.01f;
            yield return new WaitForSeconds(0.01f);
            panelImage.color = new Color(0f, 0f, 0f, alpha);
        }

        Destroy(blackPanel);
    }

    private IEnumerator FadeIn()
    {
        canvas = GameObject.Find("Canvas");
        panelRect = canvas.GetComponent<RectTransform>();
        blackPanel = Instantiate(Resources.Load<GameObject>("Prefabs/Debug/BlackPanel"), new Vector2(panelRect.rect.width / 2f, panelRect.rect.height / 2f), Quaternion.identity, canvas.transform);
        panelImage = blackPanel.GetComponent<Image>();

        float alpha = 1f;
        panelImage.color = new Color(0f, 0f, 0f, alpha);
        while (alpha > 0f)
        {
            alpha -= 0.01f;
            yield return new WaitForSeconds(0.01f);
            panelImage.color = new Color(0f, 0f, 0f, alpha);
        }

        Destroy(blackPanel);
    }

    public void GivePenalty()
    {
        penaltyCount++;
        if (penaltyCount == 3)
        { 
            Util.DecreaseReputation(1.0f);
            penaltyCount = 0;
        }
        else
            Util.DecreaseReputation();
        cantAcceptOrderCount++;
        if(cantAcceptOrderCount>=100)
        {
            MoveToEndingScene();
        }
    }

    public void MoveToEndingScene()
    {
        LoadScene("EndingScene");
    }

    public void PortalDelay()
    {
        StartCoroutine(PortalDelayTimer());
    }

    public IEnumerator PortalDelayTimer()
    {
        canUsePortal = false;
        yield return new WaitForSeconds(1.0f);
        canUsePortal = true;
    }

    public void ReStart()
    {
        if(TimeManager.Instance.isPrepareTime)
        {
            LoadScene("Cake Shop", true);
            PlayerManager.Instance.SetHp(PlayerManager.Instance.GetMaxHp());
            PlayerManager.Instance.SetBackNumberOfItem();
            PlayerManager.Instance.ResetNumberOfItemInADay();
            killMonsterInADay = false;
            TimeManager.Instance.timer = 0f;
            TimeManager.Instance.restart = true;
            TimeManager.Instance.stopTimer = false;
            soldCakeInADay = 0;
        }
        else
        {
            LoadScene("Cake Shop", true);
            PlayerManager.Instance.SetBackNumberOfItem();
            PlayerManager.Instance.ResetNumberOfItemInADay();
            TimeManager.Instance.timer = 12.0f * TimeManager.Instance.oneHour;
            TimeManager.Instance.stopTimer = false;
            TimeManager.Instance.isPrepareTime = false;
        }

    }




    public IEnumerator UpgradeMagicianSlot()
    {
        if (magicianSlotUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            numberOfMagicianSlot++;
            Util.SpendMoney(magicianSlotUpgrade.Price);
            magicianSlotUpgrade.CurrentLevel++;
            magicianSlotUpgrade.Price = 700 - ((3 - numberOfMagicianSlot) * 200);
        }
        yield return null;
    }

    public IEnumerator UpgradeCakeTable()
    {
        if (cakeTableNumberUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            numberOfCakeTable++;
            cakeTableNumberUpgrade.CurrentLevel++;
            Util.SpendMoney(cakeTableNumberUpgrade.Price);
        }
        yield return null;
    }

  /*  public IEnumerator UpgradeCounter()
    {
        if (counterNumberUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            numberOfCounter++;
            counterNumberUpgrade.CurrentLevel++;
            Util.SpendMoney(counterNumberUpgrade.Price);
        }
        yield return null;
    }*/

    public IEnumerator UpgradeGuestLeaveTime()
    {
        if (guestLeaveTimeUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            addGuestLeaveTime = 2;
            guestLeaveTimeUpgrade.CurrentLevel++;
            Util.SpendMoney(guestLeaveTimeUpgrade.Price);
        }
        yield return null;
    }

    public IEnumerator UpgradeMapB()
    {
        if (unlockMapBUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            unlockMapB = true;
            unlockMapBUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapB);
            Util.SpendMoney(unlockMapBUpgrade.Price);
        }
        yield return null;
    }

    public IEnumerator UpgradeMapA()
    {
        if (unlockMapAUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            unlockMapA = true;
            unlockMapAUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapA);
            Util.SpendMoney(unlockMapAUpgrade.Price);
        }
        yield return null;
    }

    public IEnumerator UpgradeMapS()
    {
        if (unlockMapSUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            unlockMapS = true;
            unlockMapSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapS);
            Util.SpendMoney(unlockMapSUpgrade.Price);
        }
        yield return null;
    }

    public IEnumerator UpgradeMapSS()
    {
        if (unlockMapSSUpgrade.Price <= PlayerManager.Instance.GetMoney())
        {
            unlockMapSS = true;
            unlockMapSSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapSS);
            Util.SpendMoney(unlockMapSSUpgrade.Price);
        }
        yield return null;
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

    private void AddUpgrade()
    {
        magicianSlotUpgrade = new Upgrade(3, 0, 700, "마법사 슬롯 추가", UpgradeMagicianSlot());
        upgradeList.Add(magicianSlotUpgrade);
        cakeTableNumberUpgrade = new Upgrade(1, 0, 6000, "케이크 제작대 추가", UpgradeCakeTable(), true);
        upgradeList.Add(cakeTableNumberUpgrade);
        /*counterNumberUpgrade = new Upgrade(1, 0, 10000, "주문대 추가", UpgradeCakeTable(), true);
        upgradeList.Add(counterNumberUpgrade);*/
        guestLeaveTimeUpgrade = new Upgrade(1, 0,4000, "손님 인내심 증가", UpgradeGuestLeaveTime(), true);
        upgradeList.Add(guestLeaveTimeUpgrade);
        unlockMapBUpgrade = new Upgrade(1, 0, 10000, "B등급 파밍장 해금", UpgradeMapB());
        upgradeList.Add(unlockMapBUpgrade);
        unlockMapAUpgrade = new Upgrade(1, 0, 15000, "A등급 파밍장 해금", UpgradeMapA());
        upgradeList.Add(unlockMapAUpgrade);
        unlockMapSUpgrade = new Upgrade(1, 0, 20000, "S등급 파밍장 해금", UpgradeMapS());
        upgradeList.Add(unlockMapSUpgrade);
        unlockMapSSUpgrade = new Upgrade(1, 0, 25000, "SS등급 파밍장 해금", UpgradeMapSS());
        upgradeList.Add(unlockMapSSUpgrade);
    }
}
