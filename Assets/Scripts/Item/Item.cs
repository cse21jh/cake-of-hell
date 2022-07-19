using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Code { get; set; }
    public string Name { get; set; }
    public Sprite SpriteImage { get; set; }

    public ItemType GetType()
    {
        return ((ItemType)((Code / 1000) % 10));
    }

    public int GetOrder()
    {
        return Code % 10;
    }
}
