using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Code { get; }
    public string Name { get; }
    public ItemLevel Level { get; }
    public Sprite SpriteImage { get; }
    public string Keyword { get; }
    public string FlavorText { get; }
    public int Price { get; }

    public Item(int code, string name, ItemLevel level, Sprite spriteImage, string keyword, string flavorText, int price)
    {
        Code = code;
        Name = name;
        SpriteImage = spriteImage;
        Keyword = keyword;
        FlavorText = flavorText;
        Price = price;
    }

    public ItemType GetType()
    {
        return ((ItemType)((Code / 1000) % 10));
    }

    public int GetOrder()
    {
        return Code % 10;
    }
}
