using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Item
{
    public int ID { get; }
    public string Name { get; }
    public ItemLevel Level { get; }
    public Sprite SpriteImage { get; }
    public string Description { get; }

    public _Item(int id, string name, ItemLevel level, Sprite spriteImage, string description) {
        ID = id;
        Name = name;
        Level = level;
        SpriteImage = spriteImage;
        Description = description;
    }

    public ItemType getType()
    {
        return (ItemType)(ID / 100);
    }
}
