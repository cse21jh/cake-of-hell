using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    private System.Random rand;
    private DialogUI dialog; 
    private CakeListUI cakelist;
    private int orderBase, orderIcing, orderTopping;
    private bool hasOrder = false;
    private bool flag = false;

    void Start()
    {
        rand = new System.Random();
        dialog = GameObject.Find("Canvas").transform.Find("DialogUI").GetComponent<DialogUI>();
        cakelist = GameObject.Find("Canvas").transform.Find("CakeListUI").GetComponent<CakeListUI>();
        cakelist.SellCake = SellCake;
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            if(!hasOrder)
            {
                UiManager.Instance.OpenUI(dialog);
                hasOrder = true;
                MakeNewOrder();
            }
            else
            {
                UiManager.Instance.OpenUI(cakelist);
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
        orderBase = rand.Next(1001, 1007);
        orderIcing = rand.Next(2001, 2008);
        orderTopping = rand.Next(3001, 3008);
        dialog.SetText(
            (Util.GetItem(orderTopping) as ProcessedItem).Keyword + " " +
            (Util.GetItem(orderIcing) as ProcessedItem).Keyword + " " +
            (Util.GetItem(orderBase) as ProcessedItem).Keyword + " 주세요."
        );
    }

    public void SellCake(Cake cake)
    {
        if(cake != null)
        {
            Util.EarnMoney(cake.GetPrice(orderBase, orderIcing, orderTopping));
            Debug.Log(cake.GetPrice(orderBase, orderIcing, orderTopping));
            hasOrder = false;
            UiManager.Instance.CloseUI(cakelist);
            UiManager.Instance.OpenUI(dialog);
            dialog.SetText("잘 먹겠습니다~");
            Debug.Log(PlayerManager.Instance.GetMoney());
        }
    }
}
