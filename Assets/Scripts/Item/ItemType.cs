using System;

[Flags]
public enum ItemType
{
    Null = 0,
    Base = 1 << 0,
    Topping = 1 << 1,
    Icing = 1 << 2,
    Raw = 1 << 3
}