using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RawItem : Item
{
    public List<int> OutputCode { get; }
    public List<float> Price { get; }
    public List<float> Duration { get; }

    public RawItem(int code, string name, Sprite spriteImage, List<int> outputCode, List<float> price, List<float> duration )
    {
        Code = code;
        Name = name;
        SpriteImage = spriteImage;
        OutputCode = outputCode.ToList();
        Price = price.ToList();
        Duration = duration.ToList();
    }

}
