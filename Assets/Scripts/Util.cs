using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util
{
    public static Item GetItem(int code)
    {
        if(code / 10000 == 8) return ItemManager.Instance.GetRawItem(code);
        else return ItemManager.Instance.GetProcessedItem(code);
    }

    public static int CountItem(int code) 
    {
        return SaveManager.Instance.GetNumberOfItem(code);
    }

    public static void UseItem(int code, int count = 1)
    {
        if(CountItem(code) >= count) 
        {
            SaveManager.Instance.SetNumberOfItem(code, CountItem(code) - count);
        }
        else
        {
            throw new System.ArgumentOutOfRangeException("count", System.String.Format("현재 아이템 개수인 {0}개보다 많은 {1}개의 아이템을 소모할 수 없습니다.", CountItem(code), count));
        }
    }

    public static void AddItem(int code, int count = 1)
    {
        SaveManager.Instance.SetNumberOfItem(code, CountItem(code) + count);
    }

    public static Dictionary<int, int> GetItemNumbers(ItemType type) => type switch
    {
        ItemType.Raw => SaveManager.Instance.NumberOfRaw,
        ItemType.Base => SaveManager.Instance.NumberOfBase,
        ItemType.Icing => SaveManager.Instance.NumberOfIcing,
        ItemType.Topping => SaveManager.Instance.NumberOfTopping
    };
}
