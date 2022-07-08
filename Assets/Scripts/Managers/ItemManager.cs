using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


// 가공 전
public enum RToppingIndex
{
    Cucumber,
    Number
};

public enum RIcingIndex
{
    MintChoco,
    Number
};

public enum RBaseIndex
{
    Mud,
    Number
};


//가공 후
public enum ToppingIndex 
{ 
    Cucumber, 
    Number 
};

public enum IcingIndex 
{ 
    MintChoco, 
    Number 
};

public enum BaseIndex 
{ 
    Mud, 
    Number 
};



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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
