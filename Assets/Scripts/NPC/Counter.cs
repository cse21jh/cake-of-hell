using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    //private DialogUI dialog; 
    //private CakeListUI cakelist;
    private string order = "";
    private bool hasOrder = false;
    private bool flag = false;

    void Awake()
    {
        //ui = GameObject.Find("Canvas").transform.Find("CounterUI").GetComponent<CounterUI>();
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            if(!hasOrder)
            {
                MakeNewOrder();
                hasOrder = true;
            }
            else
            {
                SellCake();
            }
        }
    }

    public override void EndInteract() 
    {
        if(flag)
        {
            flag = false;
            if(!hasOrder)
            {
                //UIManager.Instance.CloseUI(dialog);
            }
            else
            {
                //UIManager.Instance.CloseUI(cakelist);
            }
        }
    }

    public void MakeNewOrder() 
    {
        //random generate order
        //UIManager.Instance.OpenUI(dialog);
    }

    public void SellCake()
    {
        //UIManager.Instance.OpenUI(cakelist);
    }
}
