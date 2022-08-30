using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : Singleton<GameManager>
{
    public int penaltyCount;
    public int increaseReputationCount;

    public string currentSceneName;

    public Vector2 startPoint;
    public bool canMove = true;

    public bool canUsePortal = true;

    public int soldCakeInADay;

    public string currentBgmName;

    public float EarnedMoney { get; set; } = 0;
    public int WaveLevel { get; set; } = 0;
    public bool IsWave { get; set; } = false;
    public int WaveFailCount { get; set; } = 0;
    public int WaveSuccessCount { get; set; } = 0;

    private AlarmUI unlockMapAlarmUI;
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

    public bool[] shownEnding = new bool[16];

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
        currentSceneName = "MainMenu";
        AddMonsterInMap();
        AddUpgrade();
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        SceneManager.LoadScene("MainMenu");
        currentBgmName = currentSceneName;
        SoundManager.Instance.PlayBgm(currentBgmName);
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.F1))
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
            PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() + 1000);
            Debug.Log("Money" + PlayerManager.Instance.GetMoney().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            TimeManager.Instance.oneHour = 1.0f;
            Debug.Log("One Hour is One Second");
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            PlayerManager.Instance.Die();
            Debug.Log("Die");
        }
        if (Input.GetKeyDown(KeyCode.F9))
        {
            ReStart();
            Debug.Log("Restart");
        }
        if (Input.GetKeyDown(KeyCode.F10))
        {
            unlockMapB = true;
            unlockMapBUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapB);
            unlockMapA = true;
            unlockMapAUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapA);
            unlockMapS = true;
            unlockMapSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapS);
            unlockMapSS = true;
            unlockMapSSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapSS);
            Debug.Log("Unlock Map");
        }
        if (Input.GetKeyDown(KeyCode.F11))
        {
            PlayerManager.Instance.SetRealSpeed(PlayerManager.Instance.GetRealSpeed() + 1);
            Debug.Log("PlayerSpeed" + PlayerManager.Instance.GetRealSpeed().ToString());
        }
        if (Input.GetKeyDown(KeyCode.F12))
        {
            PlayerManager.Instance.SetRealSpeed(PlayerManager.Instance.GetRealSpeed() - 1);
            Debug.Log("PlayerSpeed" + PlayerManager.Instance.GetRealSpeed().ToString());
        }*/
    }

    public void LoadScene(string nextScene, bool onStartPoint = false)
    {
        SoundManager.Instance.PlayEffect("MoveScene");
        SceneManager.LoadScene(nextScene);
        currentSceneName = nextScene;
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
        UiManager.Instance.openItemList = false;
        UiManager.Instance.openMenu = false;

        CheckBgm(nextScene);
    }

    public void CheckBgm(string nextScene)
    {
        if(nextScene == "EndingScene" || nextScene == "StoryScene")
        {
            SoundManager.Instance.StopBgm();
            return;
        }

        if(nextScene == "TutorialScene")
        {
            currentBgmName = nextScene;
            SoundManager.Instance.PlayBgm(currentBgmName);
            return;
        }

        if(nextScene.Contains("Map"))
        {
            if(currentBgmName == "Farming")
            {
                return;
            }
            else
            {
                currentBgmName = "Farming";
                SoundManager.Instance.PlayBgm(currentBgmName);
                return;
            }
        }

        if(nextScene == "Cake Shop")
        {
            currentBgmName = nextScene;
            SoundManager.Instance.PlayBgm(currentBgmName);
            return;
        }


        if(currentBgmName == nextScene)
        {
            return;
        }
        else
        {
            currentBgmName = nextScene;
            SoundManager.Instance.PlayBgm(currentBgmName);
            return;
        }
    }

    public IEnumerator DieLoadScene(string nextScene)
    {
        PlayerManager.Instance.SetPlayerInShop(true);
        yield return StartCoroutine(FadeOut());
        SceneManager.LoadScene(nextScene);
        StartCoroutine(StartOnPoint());
        PlayerManager.Instance.SetHp(PlayerManager.Instance.GetMaxHp());
        PlayerManager.Instance.SetPlayerImage(0);
        yield return StartCoroutine(FadeIn());
        canMove = true;
        UiManager.Instance.openItemList = false;
        UiManager.Instance.openMenu = false;
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
        List <string> AlarmTexts = new List<string>();
        if (!unlockMapC && (TimeManager.Instance.GetDay() >= 1))
        {
            unlockMapC = true;
            UnlockItemsOfMonsters(monsterInMapC);
            AlarmTexts.Add("C 등급의 맵이 해금되었습니다!");
        }

        if (!unlockMapB && (numberOfSoldCake >= 60 || TimeManager.Instance.GetDay() >= 3))
        {
            unlockMapB = true;
            unlockMapBUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapB);
            AlarmTexts.Add("B 등급의 맵이 해금되었습니다!");
        }

        if (!unlockMapA && (numberOfSoldCake >= 140 || TimeManager.Instance.GetDay() >= 7))
        {
            unlockMapA = true;
            unlockMapAUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapA);
            AlarmTexts.Add("A 등급의 맵이 해금되었습니다!");
        }

        if (orderSystem==0 && (numberOfSoldCake >= 200 || TimeManager.Instance.GetDay() >= 10))
        {
            orderSystem = 1;
            AlarmTexts.Add("이제부터는 키워드를 사용한 주문과 플레이버를 사용한 주문이 모두 등장합니다.");
            AlarmTexts.Add("플레이버 텍스트에 관한 힌트는 재료에 대한 문장에 숨어있으니 잘 찾아보세요");
        }

        if (!unlockMapS && (numberOfSoldCake >= 300 || TimeManager.Instance.GetDay() >= 14))
        {
            unlockMapS = true;
            unlockMapSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapS);
            AlarmTexts.Add("S 등급의 맵이 해금되었습니다!");
        }

        if (orderSystem == 1 && (numberOfSoldCake >= 400 || TimeManager.Instance.GetDay() >= 20))
        {
            orderSystem = 2;
            AlarmTexts.Add("이제부터는 하나의 주문 안에 키워드와 플레이버 텍스트가 함께 숨어있습니다.\n" +
                "헷갈리지 않게 주의하세요");
        }

        if (!unlockMapSS && (numberOfSoldCake >= 560 || TimeManager.Instance.GetDay() >= 25))
        {
            unlockMapSS = true;
            unlockMapSSUpgrade.CurrentLevel++;
            UnlockItemsOfMonsters(monsterInMapSS);
            AlarmTexts.Add("SS 등급의 맵이 해금되었습니다!");
        }

        if (!magicianSlotUpgrade.IsUnlocked && processCount >=200)
        {
            magicianSlotUpgrade.IsUnlocked = true;
        }
        magicianSlotUpgrade.UpgradeFunc = UpgradeMagicianSlot();

        if (!unlockMapBUpgrade.IsUnlocked && TimeManager.Instance.GetDay() == 1)
        {
            unlockMapBUpgrade.IsUnlocked = true;
        }
        else
        {
            unlockMapBUpgrade.IsUnlocked = false;
        }

        if (!unlockMapAUpgrade.IsUnlocked && TimeManager.Instance.GetDay() == 5)
        {
            unlockMapAUpgrade.IsUnlocked = true;
        }
        else
        {
            unlockMapAUpgrade.IsUnlocked = false;
        }

        if (!unlockMapSUpgrade.IsUnlocked && TimeManager.Instance.GetDay() == 12)
        {
            unlockMapSUpgrade.IsUnlocked = true;
        }
        else
        {
            unlockMapSUpgrade.IsUnlocked = false;
        }

        if (!unlockMapSSUpgrade.IsUnlocked && TimeManager.Instance.GetDay() == 23)
        {
            unlockMapSSUpgrade.IsUnlocked = true;
        }
        else
        {
            unlockMapSSUpgrade.IsUnlocked = false;
        }
        if(AlarmTexts.Count != 0)
        {
            OpenAlarmUI(AlarmTexts);
        }
    }

    private void OpenAlarmUI(List<string> alarmText)
    {
        var unlockMapAlarm = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/AlarmUI"), FindObjectOfType<Canvas>().transform);
        unlockMapAlarmUI = unlockMapAlarm.GetComponent<AlarmUI>();
        UiManager.Instance.OpenUI(unlockMapAlarmUI);
        unlockMapAlarmUI.SetLongText(alarmText);
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
            Util.DecreaseReputation(IsWave ? 0.1f : 1.0f);
            penaltyCount = 0;
        }
        else
        {
            Util.DecreaseReputation(IsWave ? 0.1f : 0.5f);
        }
        cantAcceptOrderCount++;
        if(cantAcceptOrderCount >= 100 && !shownEnding[6])
        {
            //MoveToEndingScene();
        }
    }

    public void MoveToEndingScene()
    {
        LoadScene("EndingScene");
        UiManager.Instance.alreadyOpenItemList = true;
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
        if(currentSceneName == "EndingScene" || currentSceneName == "StoryScene")
        {
            return;
        }
        if(TimeManager.Instance.isPrepareTime)
        {
            LoadScene("Cake Shop", true);
            PlayerManager.Instance.SetHp(PlayerManager.Instance.GetMaxHp());
            PlayerManager.Instance.SetBackNumberOfItem();
            PlayerManager.Instance.ResetNumberOfItemInADay();
            PlayerManager.Instance.SetBackMoney();
            PlayerManager.Instance.ResetMoneyInADay();
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
            PlayerManager.Instance.SetBackMoney();
            PlayerManager.Instance.ResetMoneyInADay();
            TimeManager.Instance.timer = 12.0f * TimeManager.Instance.oneHour;
            TimeManager.Instance.stopTimer = false;
            TimeManager.Instance.isPrepareTime = false;
            TimeManager.Instance.restart = true;
        }
        canMove = true;
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
        cakeTableNumberUpgrade = new Upgrade(1, 0, 600, "케이크 제작대 추가", UpgradeCakeTable(), true);
        upgradeList.Add(cakeTableNumberUpgrade);
        /*counterNumberUpgrade = new Upgrade(1, 0, 10000, "주문대 추가", UpgradeCakeTable(), true);
        upgradeList.Add(counterNumberUpgrade);*/
        guestLeaveTimeUpgrade = new Upgrade(1, 0,400, "손님 인내심 증가", UpgradeGuestLeaveTime(), true);
        upgradeList.Add(guestLeaveTimeUpgrade);
        unlockMapBUpgrade = new Upgrade(1, 0, 1000, "B등급 파밍장 해금", UpgradeMapB());
        upgradeList.Add(unlockMapBUpgrade);
        unlockMapAUpgrade = new Upgrade(1, 0, 1500, "A등급 파밍장 해금", UpgradeMapA());
        upgradeList.Add(unlockMapAUpgrade);
        unlockMapSUpgrade = new Upgrade(1, 0, 2000, "S등급 파밍장 해금", UpgradeMapS());
        upgradeList.Add(unlockMapSUpgrade);
        unlockMapSSUpgrade = new Upgrade(1, 0, 2500, "SS등급 파밍장 해금", UpgradeMapSS());
        upgradeList.Add(unlockMapSSUpgrade);
    }
}
