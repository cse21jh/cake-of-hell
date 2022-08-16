using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : Singleton<ProcessManager>
{
    public MagicianProcess[] MagicianProcesses;
    public CakeProcess[] CakeProcesses;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MagicianProcesses = new MagicianProcess[8];
        CakeProcesses = new CakeProcess[3];
    }

    public void AddMagicianProcess(int idx, Recipe recipe, int count, ItemSlotComponent slot, ProgressCircle circle)
    {
        float totalTime = recipe.Duration * count;
        float interval = totalTime / 100.0f;
        MagicianProcess newProc = new MagicianProcess(totalTime, interval);
        newProc.ProcessRecipe = recipe;
        newProc.Count = count;
        newProc.Slot = slot;
        newProc.Circle = circle;

        newProc.OnStart = null;
        newProc.taskList.Add(() => 
        {
            if(newProc.Circle != null) 
            {
                newProc.Circle.SetProgress(newProc.LoopCount);
            }
        });
        newProc.OnEnd = () => 
        {
            if(newProc.Slot != null)
            {
                newProc.Slot.LoadItem(newProc.ProcessRecipe.Output, newProc.Count);
            }
            if(newProc.Circle != null)
            {
                newProc.Circle.SetProgress(0);
            }
        };
        MagicianProcesses[idx] = newProc;
        StartCoroutine(newProc.Run());
    }

    public void AddCakeProcess(int idx, CakeTableUI ui, Cake cake, ProgressCircle circle)
    {
        float totalTime = 5.0f;
        float interval = totalTime / 100.0f;
        CakeProcess newProc = new CakeProcess(totalTime, interval);
        newProc.UI = ui;
        newProc.CakeRecipe = cake;
        newProc.Circle = circle;

        newProc.OnStart = null;
        newProc.taskList.Add(() => 
        {
            if(newProc.Circle != null) 
            {
                newProc.Circle.SetProgress(newProc.LoopCount);
            }
        });
        newProc.OnEnd = () => 
        {
            PlayerManager.Instance.AddCake(cake);
            if(newProc.UI != null)
            {
                newProc.UI.UpdateSlots();
            }
            if(newProc.Circle != null)
            {
                newProc.Circle.SetProgress(0);
            }
        };
        CakeProcesses[idx] = newProc;
        StartCoroutine(newProc.Run());
    }
}
