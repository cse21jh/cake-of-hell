using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RawItem
{
    public int Code { get; }
    public string Name { get; }
    public Sprite SpriteImage { get; }
    public new List<int> OutputCode { get; }
    public List<int> Price { get; }
    public List<float> Duration { get; }

    public RawItem(int code, string name, Sprite spriteImage, List<int> outputCode, List<int> price, List<float> duration )
    {
        Code = code;
        Name = name;
        SpriteImage = spriteImage;
        OutputCode = outputCode.ToList();
        Price = price.ToList();
        Duration = duration.ToList();
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
