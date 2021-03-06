using System.Collections;
using System.Collections.Generic;
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
        Recipe recipe = new Recipe(code, input.OutputCode[0], input.Price[0], input.Duration[0]);
        List<Recipe> lr = new List<Recipe> { recipe };
        return lr;
    }

    public static void EarnMoney(float amount)
    {
        PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney()+amount);
    }

    public static void SpendMoney(float amount)
    {
        if(PlayerManager.Instance.GetMoney() >= amount) 
        {
            PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() - amount);
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("amount", System.String.Format("현재 보유한 돈인 {0}보다 많은 {1}의 돈을 쓸 수 없습니다.", PlayerManager.Instance.GetMoney(), amount));
        }
    }
}
