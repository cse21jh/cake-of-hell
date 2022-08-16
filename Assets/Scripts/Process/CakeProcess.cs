using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeProcess : Process
{
    public CakeTableUI UI { get; set; }
    public Cake CakeRecipe { get; set; }
    public ProgressCircle Circle { get; set; }

    public CakeProcess(float _totalTime, float _interval) : base(_totalTime, _interval)
    {

    }
}