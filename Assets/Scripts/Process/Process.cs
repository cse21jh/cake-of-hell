using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process
{
    private float totalTime, segment, nowTime;
    
    public List<System.Action> taskList;
    public System.Action OnStart, OnEnd;
    public int LoopCount { get; private set; }

    public Process(float _totalTime, float _segment)
    {
        totalTime = _totalTime;
        segment = _segment;
        taskList = new List<System.Action>();
    }

    public IEnumerator Run()
    {
        LoopCount = 0;
        nowTime = 0.0f;

        if(OnStart != null) OnStart();
        while(nowTime <= totalTime)
        {
            LoopCount += 1;
            nowTime += segment;
            foreach(System.Action func in taskList) 
            {
                func();
            }
            yield return new WaitForSeconds(segment);
        }
        if(OnEnd != null) OnEnd();
    }

    public float GetTime()
    {
        return nowTime;
    }
}
