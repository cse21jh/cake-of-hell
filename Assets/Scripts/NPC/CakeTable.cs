using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeTable : NPC
{
    [SerializeField]
    private int tableNumber;

    private CakeTableUI ui; 

    private bool flag = false;

    void Awake()
    {
        ui = GameObject.Find("Canvas").transform.Find("CakeTableUI").GetComponent<CakeTableUI>();
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            UiManager.Instance.OpenUI(ui);
            ui.SetTableNumber(tableNumber);
        }
    }

    public override void EndInteract() 
    {
        if(flag)
        {
            flag = false;
            UiManager.Instance.CloseUI(ui);
        }
    }
}