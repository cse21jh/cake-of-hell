using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : Singleton<ProcessManager>
{
    private int _id = 0;
    private Dictionary<int, Process> ProcessList;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int AddMagicianProcess(Recipe recipe, int count, ItemSlotComponent slot, ProgressCircle circle)
    {
        int procId = AssginNewId();
        float totalTime = recipe.Duration * count;
        float interval = totalTime / 100.0f;
        Process newProc = new Process(totalTime, interval);

        newProc.OnStart = null;
        newProc.taskList.Add(() => 
        {
            if(circle != null) 
            {
                circle.SetProgress(newProc.LoopCount);
            }
        });
        newProc.OnEnd = () => 
        {
            if(slot != null)
            {
                slot.LoadItem(recipe.Output, count);
            }
            if(circle != null)
            {
                circle.SetProgress(0);
            }
        };

        StartCoroutine(newProc.Run());

        return procId;
    }

    private int AssginNewId() 
    {
        return ++_id;
    }
}
