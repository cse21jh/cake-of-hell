using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


// 가공 전
public enum RToppingIndex
{
    Cucumber,
    Number,
    Null
};

public enum RIcingIndex
{
    MintChoco,
    Number,
    Null
};

public enum RBaseIndex
{
    Mud,
    Number,
    Null
};


//가공 후
public enum ToppingIndex 
{ 
    Cucumber, 
    Number,
    Null
};

public enum IcingIndex 
{ 
    MintChoco, 
    Number,
    Null
};

public enum BaseIndex 
{ 
    Mud, 
    Number,
    Null
};




public enum CakeIndex
{ 
    MudMintChocoCucumber,
    Number,
    Null
}


[Serializable]
public struct Item
{
    public string itemName;
    public Sprite sprite;
    public string explanation;
    public string rank;
}


public class ItemManager : Singleton<ItemManager>
{
    public Item[] ToppingInformation = new Item[(int)ToppingIndex.Number];
    public Item[] IcingInformation = new Item[(int)IcingIndex.Number];
    public Item[] BaseInformation = new Item[(int)BaseIndex.Number];
    
    public Item[] RToppingInformation = new Item[(int)RToppingIndex.Number];
    public Item[] RIcingInformation = new Item[(int)RIcingIndex.Number];
    public Item[] RBaseInformation = new Item[(int)RBaseIndex.Number];

    private GameObject check = null;

    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public CakeIndex ReturnCake(BaseIndex baseIndex, IcingIndex icingIndex, ToppingIndex toppingIndex)
    {
        if(baseIndex == BaseIndex.Mud && icingIndex == IcingIndex.MintChoco && toppingIndex == ToppingIndex.Cucumber)
        {
            return CakeIndex.MudMintChocoCucumber;
        }
        else
        {
            return CakeIndex.Null;
        }
        
    }
}
