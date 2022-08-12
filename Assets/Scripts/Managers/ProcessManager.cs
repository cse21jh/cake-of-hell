using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : Singleton<ProcessManager>
{
    private int _id = 0;
    public MagicianProcess[] MagicianProcesses;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MagicianProcesses = new MagicianProcess[8];
    }

    void Update()
    {
        
    }

    public void AddMagicianProcess(int idx, Recipe recipe, int count, ItemSlotComponent slot, ProgressCircle circle)
    {
        //int procId = AssginNewId();
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

        //return procId;
    }

    /*private int AssginNewId() 
    {
        return ++_id;
    }*/
}
