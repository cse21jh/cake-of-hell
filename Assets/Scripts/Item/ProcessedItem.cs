using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessedItem : Item
{
    public ItemLevel Level { get; }
    public string Keyword { get; }
    public string FlavorText { get; }
    public int Price { get; }

    public ProcessedItem(int code, string name, ItemLevel level, Sprite spriteImage, string keyword, string flavorText, int price)
    {
        Code = code;
        Name = name;
        SpriteImage = spriteImage;
        Keyword = keyword;
        FlavorText = flavorText;
        Price = price;
    }
}
