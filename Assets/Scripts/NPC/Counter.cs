using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    private DialogUI dialog; 
    private CakeListUI cakelist;
    private string order = "";
    private bool hasOrder = false;
    private bool flag = false;

    void Start()
    {
        dialog = GameObject.Find("Canvas").transform.Find("DialogUI").GetComponent<DialogUI>();
        cakelist = GameObject.Find("Canvas").transform.Find("CakeListUI").GetComponent<CakeListUI>();
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
            UiManager.Instance.CloseUI(dialog);
            UiManager.Instance.CloseUI(cakelist);
        }
    }

    public void MakeNewOrder() 
    {
        //random generate order
        UiManager.Instance.OpenUI(dialog);
    }

    public void SellCake()
    {
        UiManager.Instance.OpenUI(cakelist);
    }
}
