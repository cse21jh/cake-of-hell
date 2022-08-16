using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    private int counterNumber = 0;
    private int guestCount = 0;
    private System.Random rand;
    private DialogUI[] dialogs; 
    private CakeListUI cakelist;
    private int orderBase, orderIcing, orderTopping;
    private bool hasOrder = false;
    private bool flag = false;

    void Start()
    {
        rand = new System.Random();
        dialogs = new DialogUI[3];
        dialogs[0] = GameObject.Find("Canvas").transform.Find("DialogUI0").GetComponent<DialogUI>();
        dialogs[1] = GameObject.Find("Canvas").transform.Find("DialogUI1").GetComponent<DialogUI>();
        dialogs[2] = GameObject.Find("Canvas").transform.Find("DialogUI2").GetComponent<DialogUI>();
        cakelist = GameObject.Find("Canvas").transform.Find("CakeListUI").GetComponent<CakeListUI>();
        cakelist.SellCake = SellCake;
        StartCoroutine(GuestCome());
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            if(!hasOrder)
            {
                UiManager.Instance.OpenUI(dialogs[counterNumber]);
                if(guestCount > 0)
                {
                    hasOrder = true;
                    MakeNewOrder();
                    StartCoroutine(GuestLeave());
                }
                else
                {
                    dialogs[counterNumber].SetText("손님이 없어요.");
                }
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
            UiManager.Instance.CloseUI(dialogs[counterNumber]);
            UiManager.Instance.CloseUI(cakelist);
        }
    }

    public void MakeNewOrder() 
    {
        orderBase = rand.Next(1001, 1007);
        orderIcing = rand.Next(2001, 2008);
        orderTopping = rand.Next(3001, 3008);
        dialogs[counterNumber].SetText(
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
            flag = true;
            UiManager.Instance.CloseUI(cakelist);
            UiManager.Instance.OpenUI(dialogs[counterNumber]);
            dialogs[counterNumber].SetText("잘 먹겠습니다~");
            Debug.Log(System.String.Format("현재 돈: {0}", PlayerManager.Instance.GetMoney()));
        }
    }

    private IEnumerator GuestCome() 
    {
        //while(TimeManager.Instance.isOpenTime)
        while(true)
        {
            if(guestCount < 3)
            {
                guestCount++;
                Debug.Log(System.String.Format("손님 수: {0}", guestCount));
            }
            yield return new WaitForSeconds(rand.Next
            (
                TimeManager.Instance.GuestEnterTimeStart,
                TimeManager.Instance.GuestEnterTimeEnd
            ));
        }
    }

    private IEnumerator GuestLeave()
    {
        yield return new WaitForSeconds(rand.Next
        (
            TimeManager.Instance.GuestLeaveTimeStart,
            TimeManager.Instance.GuestLeaveTimeEnd
        ));

        if(hasOrder)
        {
            guestCount--;
            hasOrder = false;
            //Penalty
            Debug.Log(System.String.Format("손님 수: {0}", guestCount));
        }
    }
}
