using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Util
{
    public static Item GetItem(int code)
    {
        if(code / 1000 == 4) return ItemManager.Instance.GetRawItem(code);
        else return ItemManager.Instance.GetProcessedItem(code);
    }

    public static int CountItem(int code) 
    {
        return PlayerManager.Instance.GetNumberOfItem(code);
    }

    public static void UseItem(int code, int count = 1)
    {
        if(CountItem(code) >= count) 
        {
            PlayerManager.Instance.SetNumberOfItem(code, CountItem(code) - count);
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("count", System.String.Format("현재 아이템 개수인 {0}개보다 많은 {1}개의 아이템을 소모할 수 없습니다.", CountItem(code), count));
        }
    }

    public static void AddItem(int code, int count = 1)
    {
        PlayerManager.Instance.SetNumberOfItem(code, CountItem(code) + count);
        PlayerManager.Instance.SetNumberOfItemInADay(code, CountItem(code) + count);
    }

    public static Dictionary<int, int> GetItemNumbers(ItemType type) => type switch
    {
        ItemType.Raw => PlayerManager.Instance.GetPlayer().NumberOfRaw,
        ItemType.Base => PlayerManager.Instance.GetPlayer().NumberOfBase,
        ItemType.Icing => PlayerManager.Instance.GetPlayer().NumberOfIcing,
        ItemType.Topping => PlayerManager.Instance.GetPlayer().NumberOfTopping,
        _ => null
    };

    public static List<Recipe> GetRecipesFromInput(int code)
    {
        RawItem input = GetItem(code) as RawItem;
        if(input == null) return null;
        List<Recipe> lr = new List<Recipe>();
        for(int i=0; i<input.OutputCode.Count; i++) 
        {
            lr.Add(new Recipe(code, input.OutputCode[i], input.Price[i], input.Duration[i]));
        }
        return lr;
    }

    public static void EarnMoney(float amount)
    {
        PlayerManager.Instance.SetMoneyInADay(PlayerManager.Instance.GetMoneyInADay() + amount);
        PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney()+amount);
    }

    public static void SpendMoney(float amount)
    {
        if(PlayerManager.Instance.GetMoney() >= amount) 
        {
            PlayerManager.Instance.SetMoneyInADay(PlayerManager.Instance.GetMoneyInADay() - amount);
            PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() - amount);
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("amount", System.String.Format("현재 보유한 돈인 {0}보다 많은 {1}의 돈을 쓸 수 없습니다.", PlayerManager.Instance.GetMoney(), amount));
        }
    }

    public static float GetPlayerSpeed()
    {
        return PlayerManager.Instance.GetSpeed();
    }

    public static void IncreaseReputation(float amount = 0.5f)
    {
        if(TimeManager.Instance.GetReputation() + amount > 5.0f)
        {
            return;
        }
        TimeManager.Instance.SetReputation(TimeManager.Instance.GetReputation() + amount);
    }

    public static void DecreaseReputation(float amount = 0.5f)
    {
        TimeManager.Instance.SetReputation(TimeManager.Instance.GetReputation() - amount);
    }

    public static BattleMapList GetNowMap() => GameManager.Instance.currentSceneName switch 
    {
        "Field Map" => BattleMapList.MapHome,
        "Magician Cave" => BattleMapList.MapMagi,
        "Deep Sea Map" => BattleMapList.MapC,
        "Desert Map" => BattleMapList.MapB,
        "City Map" => BattleMapList.MapA,
        "Village Map" => BattleMapList.MapS,
        "Cloud Map" => BattleMapList.MapSS,
        _ => BattleMapList.None
    };

    public static string[] LongSentenceToArray(string longSentence)
    {
        return longSentence.Split('.').Select(str => str + ".").ToArray();
    }

    public static bool IsSceneUnlocked(string sceneRank) => sceneRank switch 
    {
        "C" => GameManager.Instance.unlockMapC,
        "B" => GameManager.Instance.unlockMapB,
        "A" => GameManager.Instance.unlockMapA,
        "S" => GameManager.Instance.unlockMapS,
        "SS" => GameManager.Instance.unlockMapSS,
        _ => false
    };
}
