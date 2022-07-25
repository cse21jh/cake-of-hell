using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessedItem : Item
{
    public ItemLevel Level { get; }
    public string Keyword { get; }
    public string FlavorText { get; }
    public float Price { get; }

    public ProcessedItem(int code, string name, ItemLevel level, Sprite spriteImage, string keyword, string flavorText, float price)
    {
        Code = code;
        Name = name;
        Level = level;
        SpriteImage = spriteImage;
        Keyword = keyword;
        FlavorText = flavorText;
        Price = price;
    }
}
