using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<int, ProcessedItem> ProcessedItemList = new Dictionary<int, ProcessedItem>();
    public Dictionary<int, RawItem>RawItemList = new Dictionary<int, RawItem>();

    public List<int> ItemCodeList = new List<int>();

    private GameObject check = null;


    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        AddProcessedItem(0, "민 초 좋 아", ItemLevel.SS, null, "민 초 좋 아", "민 초 좋 아", 9999);
        AddRawItem(0, "민 초 좋 아", null, new List<int> { 0 }, new List<int> { 9999 }, new List<float> { 9999.0f });
        AddBases();
        AddToppings();
        AddIcings();
        AddRawItems();
    }


   public void AddBases()
    {
        AddProcessedItem(10001, "저주받은 흙", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "촉촉한 빵", "흙이 부드럽다니... 좀 이상하긴 해", 10);
        AddProcessedItem(10002, "진화한 흙", ItemLevel.B, Resources.Load<Sprite>("Sprites/Mud"), "쫄깃한 빵", "쫀드기 아닙니다. 구워 먹지 말 것", 20);
    }

    public void AddToppings()
    {
       
    }

    public void AddIcings()
    {
        
    }

    public void AddRawItems()
    {
        AddRawItem(81001, "진흙", Resources.Load<Sprite>("Sprites/Mud"), new List<int> { 10001, 10002 }, new List<int> { 2, 3 }, new List<float> { 1.0f, 2.0f }  );
    }



    public void AddProcessedItem(int code, string name, ItemLevel level, Sprite spriteimage, string keyword, string flavorText, int price)
    {
        ProcessedItem processedItem = new ProcessedItem(code, name, level, spriteimage, keyword, flavorText, price);
        ProcessedItemList.Add(code, processedItem);
        ItemCodeList.Add(code);
        int itemType = (code / 10000);
        if (itemType == 1)
        {
            SaveManager.Instance.NumberOfBase.Add(code,0);
        }
        else if (itemType == 2)
        { 
            SaveManager.Instance.NumberOfTopping.Add(code, 0);
        }
        else if (itemType == 4)
        { 
            SaveManager.Instance.NumberOfIcing.Add(code, 0);
        }

    }

    public void AddRawItem(int code, string name, Sprite spriteimage, List<int> outputCode, List<int> price, List<float> duration)
    {
        ItemCodeList.Add(code);
        RawItem rawItem = new RawItem(code, name, spriteimage, outputCode, price, duration);
        RawItemList.Add(code, rawItem);
        SaveManager.Instance.NumberOfRaw.Add(code, 0);
    }


    //Fix me. 
    public string ReturnCake(int baseCode, int toppinCode, int icingcode)
    {
        string a = "민초케이크";
        return a;
    }

    public ProcessedItem GetProcessedItem(int code)
    {
        return ProcessedItemList[code];
    }

    public RawItem GetRawItem(int code)
    {
        return RawItemList[code];
    }

    public int GetOrder(int code)
    {
        return code % 10;
    }

    public int GetPriceOfProcessedItem(int code)
    {
        return ProcessedItemList[code].Price;
    }

    public int GetPriceOfRawItem(int code, int n)
    {
        return RawItemList[code].Price[n];
    }


}
