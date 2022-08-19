using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private System.Random rand;
    private Counter[] counters;
    private DialogUI dialog;
    private CakeListUI cakelist;

    void Start()
    {
        rand = new System.Random();
        counters = new Counter[3];
        counters[0] = GameObject.Find("Counter0").GetComponent<Counter>();
        counters[1] = GameObject.Find("Counter1").GetComponent<Counter>();
        counters[2] = GameObject.Find("Counter2").GetComponent<Counter>();
        StartCoroutine(GuestCome());
    }

    private IEnumerator GuestCome() 
    {
        while(!TimeManager.Instance.isPrepareTime)
        {
            int counterNumber = GetAvailableCounter();
            if(counterNumber != -1 && counters[counterNumber].GuestObject)
            {
                counters[counterNumber].GuestObject.SetActive(true);
                yield return StartCoroutine(ProcessManager.Instance.MoveProcess(
                    counters[counterNumber].GuestObject, 
                    counters[counterNumber].gameObject.transform.position + new Vector3(-2, 0, 0),
                    3.0f
                ));
                counters[counterNumber].HasGuest = true;
            }
            yield return new WaitForSeconds(rand.Next
            (
                TimeManager.Instance.GuestEnterTimeStart - 3,
                TimeManager.Instance.GuestEnterTimeEnd - 3
            ));
        }
    }

    private int GetAvailableCounter()
    {
        if(!counters[0].HasGuest) return 0;
        else if(!counters[1].HasGuest) return 1;
        else if(!counters[2].HasGuest) return 2;
        else return -1;
    }
}
