using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int Code { get; set; }
    public string Name { get; set; }
    public Sprite SpriteImage { get; set; }

    public new ItemType GetType()
    {
        return (ItemType)(Code / 10000);
    }

}
