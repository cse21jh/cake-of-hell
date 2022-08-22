using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    //Player
    public float MaxHp;
    public float Hp;
    public float Speed;
    public float AttackDamage;
    public float Money;
    public List<int> ItemCode = new List<int>();
    public List<int> NumberOfItem = new List<int>();
    /*
        public string cake0;
        public string cake1;
        public string cake2;
        public string cake3;
        public string cake4;
    */

    //TimeManager
    public int day;
    public float reputation;


    //GameManager
    public int numberOfSoldCake;
    public int numberOfSatisfiedCustomer;
    public int dieCount;
    public int killMonsterCount;
    public int killSSMonsterCount;
    public int processCount;
    public int processSSCount;
    public int cantAcceptOrderCount;
    public int enterBlackHoleCount;
    public int[] killEachMonsterCount = new int[11];

    public bool unlockMapC;
    public bool unlockMapB;
    public bool unlockMapA;
    public bool unlockMapS;
    public bool unlockMapSS;
    public int orderSystem;
    
    public List<int> unlockBaseCode = new List<int>();
    public List<int> unlockIcingCode = new List<int>();
    public List<int> unlockToppingCode = new List<int>();
    public List<int> unlockRawCode = new List<int>();

    public int numberOfMagicianSlot;
    public int numberOfCakeTable;
    public int addGuestLeaveTime;


    public int magicianSlotUpgradeLevel;
    public int magicianSlotUpgradePrice;
    public int cakeTableNumberUpgradeLevel;
    //public int counterNumberUpgradeLevel;
    public int guestLeaveTimeUpgradeLevel;
    public int unlockMapBUpgradeLevel;
    public int unlockMapAUpgradeLevel;
    public int unlockMapSUpgradeLevel;
    public int unlockMapSSUpgradeLevel;
}



public class SaveManager : Singleton<SaveManager>
{
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "CakeOfHellData.json");
        DontDestroyOnLoad(gameObject);
    }

    public void JsonLoad()
    {
        SaveData saveData = new SaveData();

        if(!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if(saveData != null)
            {
                Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
                PlayerManager.Instance.SetMaxHp(saveData.MaxHp);
                PlayerManager.Instance.SetHp(saveData.Hp);
                player.Speed = saveData.Speed;
                PlayerManager.Instance.SetAttackDamage(saveData.AttackDamage);
                PlayerManager.Instance.SetMoney(saveData.Money);
                FromJsonList(saveData.ItemCode, saveData.NumberOfItem,player);
                /*
                player.CakeList[0] = JsonUtility.FromJson<Cake>(saveData.cake0);
                player.CakeList[1] = JsonUtility.FromJson<Cake>(saveData.cake1);
                player.CakeList[2] = JsonUtility.FromJson<Cake>(saveData.cake2);
                player.CakeList[3] = JsonUtility.FromJson<Cake>(saveData.cake3);
                player.CakeList[4] = JsonUtility.FromJson<Cake>(saveData.cake4);
*/                //TimeManager
                TimeManager.Instance.day = saveData.day;
                TimeManager.Instance.reputation = saveData.reputation;
                //GameManager
                GameManager.Instance.numberOfSoldCake = saveData.numberOfSoldCake;
                GameManager.Instance.numberOfSatisfiedCustomer = saveData.numberOfSatisfiedCustomer;
                GameManager.Instance.dieCount = saveData.dieCount;
                GameManager.Instance.killMonsterCount = saveData.killMonsterCount;
                GameManager.Instance.killSSMonsterCount = saveData.killSSMonsterCount;
                GameManager.Instance.processCount = saveData.processCount;
                GameManager.Instance.processSSCount = saveData.processSSCount;
                GameManager.Instance.cantAcceptOrderCount = saveData.cantAcceptOrderCount;
                GameManager.Instance.enterBlackHoleCount = saveData.enterBlackHoleCount;
                GameManager.Instance.killEachMonsterCount = saveData.killEachMonsterCount;
                GameManager.Instance.unlockMapC = saveData.unlockMapC; 
                GameManager.Instance.unlockMapB = saveData.unlockMapB;
                GameManager.Instance.unlockMapA = saveData.unlockMapA;
                GameManager.Instance.unlockMapS = saveData.unlockMapS;
                GameManager.Instance.unlockMapSS = saveData.unlockMapSS;
                GameManager.Instance.orderSystem = saveData.orderSystem;
                GameManager.Instance.unlockBaseCode = saveData.unlockBaseCode;
                GameManager.Instance.unlockIcingCode = saveData.unlockIcingCode;
                GameManager.Instance.unlockToppingCode = saveData.unlockToppingCode;
                GameManager.Instance.unlockRawCode = saveData.unlockRawCode;
                GameManager.Instance.numberOfMagicianSlot = saveData.numberOfMagicianSlot;
                GameManager.Instance.numberOfCakeTable = saveData.numberOfCakeTable;
                GameManager.Instance.addGuestLeaveTime = saveData.addGuestLeaveTime;
                GameManager.Instance.magicianSlotUpgrade.CurrentLevel = saveData.magicianSlotUpgradeLevel;
                GameManager.Instance.magicianSlotUpgrade.Price = saveData.magicianSlotUpgradePrice;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.magicianSlotUpgrade);
                GameManager.Instance.cakeTableNumberUpgrade.CurrentLevel = saveData.cakeTableNumberUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.cakeTableNumberUpgrade);
                /*GameManager.Instance.counterNumberUpgrade.CurrentLevel = saveData.counterNumberUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.counterNumberUpgrade);*/
                GameManager.Instance.guestLeaveTimeUpgrade.CurrentLevel = saveData.guestLeaveTimeUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.guestLeaveTimeUpgrade);
                GameManager.Instance.unlockMapBUpgrade.CurrentLevel = saveData.unlockMapBUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.unlockMapBUpgrade);
                GameManager.Instance.unlockMapAUpgrade.CurrentLevel = saveData.unlockMapAUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.unlockMapAUpgrade);
                GameManager.Instance.unlockMapSUpgrade.CurrentLevel = saveData.unlockMapSUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.unlockMapSUpgrade);
                GameManager.Instance.unlockMapSSUpgrade.CurrentLevel = saveData.unlockMapSSUpgradeLevel;
                GameManager.Instance.upgradeList.Add(GameManager.Instance.unlockMapSSUpgrade);
                GameManager.Instance.CheckUnlock();
            }
        }
    }

    public void JsonSave()
    {
        SaveData saveData = new SaveData();
        //player
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        saveData.MaxHp = PlayerManager.Instance.GetMaxHp();
        saveData.Hp = PlayerManager.Instance.GetHp();
        saveData.Speed = PlayerManager.Instance.GetSpeed();
        saveData.AttackDamage = PlayerManager.Instance.GetAttackDamage();
        saveData.Money = PlayerManager.Instance.GetMoney();
        ToJsonList(saveData, player.NumberOfBase);
        ToJsonList(saveData, player.NumberOfIcing);
        ToJsonList(saveData, player.NumberOfTopping);
        ToJsonList(saveData, player.NumberOfRaw);
        /*
            saveData.cake0 = JsonUtility.ToJson(player.CakeList[0], true);
            saveData.cake1 = JsonUtility.ToJson(player.CakeList[1], true);
            saveData.cake2 = JsonUtility.ToJson(player.CakeList[2], true);
            saveData.cake3 = JsonUtility.ToJson(player.CakeList[3], true);
            saveData.cake4 = JsonUtility.ToJson(player.CakeList[4], true);*/
        //TimeManager
        saveData.day = TimeManager.Instance.day;
        saveData.reputation = TimeManager.Instance.reputation;
        //GameManager
        saveData.numberOfSoldCake = GameManager.Instance.numberOfSoldCake;
        saveData.numberOfSatisfiedCustomer = GameManager.Instance.numberOfSatisfiedCustomer;
        saveData.dieCount = GameManager.Instance.dieCount;
        saveData.killMonsterCount = GameManager.Instance.killMonsterCount;
        saveData.killSSMonsterCount = GameManager.Instance.killSSMonsterCount;
        saveData.processCount = GameManager.Instance.processCount;
        saveData.processSSCount = GameManager.Instance.processSSCount;
        saveData.cantAcceptOrderCount = GameManager.Instance.cantAcceptOrderCount;
        saveData.enterBlackHoleCount = GameManager.Instance.enterBlackHoleCount;
        saveData.killEachMonsterCount = GameManager.Instance.killEachMonsterCount;
        saveData.unlockMapC = GameManager.Instance.unlockMapC;
        saveData.unlockMapB = GameManager.Instance.unlockMapB;
        saveData.unlockMapA = GameManager.Instance.unlockMapA;
        saveData.unlockMapS = GameManager.Instance.unlockMapS;
        saveData.unlockMapSS = GameManager.Instance.unlockMapSS;
        saveData.orderSystem = GameManager.Instance.orderSystem;
        saveData.unlockBaseCode = GameManager.Instance.unlockBaseCode;
        saveData.unlockIcingCode = GameManager.Instance.unlockIcingCode;
        saveData.unlockToppingCode = GameManager.Instance.unlockToppingCode;
        saveData.unlockRawCode = GameManager.Instance.unlockRawCode;
        saveData.numberOfMagicianSlot = GameManager.Instance.numberOfMagicianSlot;
        saveData.numberOfCakeTable = GameManager.Instance.numberOfCakeTable;
        saveData.addGuestLeaveTime = GameManager.Instance.addGuestLeaveTime;
        saveData.magicianSlotUpgradeLevel = GameManager.Instance.magicianSlotUpgrade.CurrentLevel;
        saveData.magicianSlotUpgradePrice = GameManager.Instance.magicianSlotUpgrade.Price;
        saveData.cakeTableNumberUpgradeLevel = GameManager.Instance.cakeTableNumberUpgrade.CurrentLevel;
        //saveData.counterNumberUpgradeLevel = GameManager.Instance.counterNumberUpgrade.CurrentLevel;
        saveData.guestLeaveTimeUpgradeLevel = GameManager.Instance.guestLeaveTimeUpgrade.CurrentLevel;
        saveData.unlockMapBUpgradeLevel = GameManager.Instance.unlockMapBUpgrade.CurrentLevel;
        saveData.unlockMapAUpgradeLevel = GameManager.Instance.unlockMapAUpgrade.CurrentLevel;
        saveData.unlockMapSUpgradeLevel = GameManager.Instance.unlockMapSUpgrade.CurrentLevel;
        saveData.unlockMapSSUpgradeLevel = GameManager.Instance.unlockMapSSUpgrade.CurrentLevel;

        string json = JsonUtility.ToJson(saveData, true);

        if (path != null)
            File.WriteAllText(path, json);
    }

    private void ToJsonList(SaveData saveData,Dictionary<int,int> itemDic)
    {
        foreach (var pair in itemDic)
        {
            saveData.ItemCode.Add(pair.Key);
            saveData.NumberOfItem.Add(pair.Value);
        }
    }

    private void FromJsonList(List<int> itemCode,List<int> numberOfItem,Player player)
    {
        for(int i=0;i<itemCode.Count;i++)
        {
            switch(itemCode[i]/1000)
            {
                case 1:
                    player.NumberOfBase[itemCode[i]] = numberOfItem[i];
                    break;
                case 2:
                    player.NumberOfIcing[itemCode[i]] = numberOfItem[i];
                    break;
                case 3:
                    player.NumberOfTopping[itemCode[i]] = numberOfItem[i];
                    break;
                case 4:
                    player.NumberOfRaw[itemCode[i]] = numberOfItem[i];
                    break;
            }
        }
    }

    public bool CheckSaveData()
    {
        if(File.Exists(path))
        {
            return true;
        }
        return false;
    }
}