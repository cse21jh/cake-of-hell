using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianProcess : Process
{
    public int Count { get; set; }
    public Recipe ProcessRecipe { get; set; }
    public ItemSlotComponent Slot { get; set; }
    public ProgressCircle Circle { get; set; }

    public MagicianProcess(float _totalTime, float _interval) : base(_totalTime, _interval)
    {

    }
}