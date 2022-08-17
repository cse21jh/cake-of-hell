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
            if(!counters[0].HasGuest)
            {
                counters[0].HasGuest = true;
                Debug.Log("Guest in Counter 1");
            }
            else if(!counters[1].HasGuest)
            {
                counters[1].HasGuest = true;
                Debug.Log("Guest in Counter 2");
            }
            else if(!counters[2].HasGuest)
            {
                counters[2].HasGuest = true;
                Debug.Log("Guest in Counter 3");
            }
            yield return new WaitForSeconds(rand.Next
            (
                TimeManager.Instance.GuestEnterTimeStart,
                TimeManager.Instance.GuestEnterTimeEnd
            ));
        }
    }
}
