using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    public GameObject CakeListObject;
    public GameObject GuestObject { get; set; }
    private System.Random rand;
    private DialogUI dialog; 
    private CakeListUI cakelist;
    private int orderBase, orderIcing, orderTopping;
    private bool hasOrder = false;
    private bool flag = false;
    private bool isOrderDialogOn = false;

    public bool HasGuest { get; set; } = true;

    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        rand = new System.Random();
        dialog = canvas.transform.Find("DialogUI").GetComponent<DialogUI>();
        //cakelist = GameObject.Find("Canvas").transform.Find("CakeListUI").GetComponent<CakeListUI>();
        cakelist = Instantiate(ResourceLoader.Instance.GetPrefab("CakeListUI"), canvas.transform).GetComponent<CakeListUI>();
        cakelist.SellCake = SellCake;
        GuestObject = Instantiate(ResourceLoader.Instance.GetPrefab("Guest"));
        if(!TimeManager.Instance.isPrepareTime)
        {
            GuestObject.transform.position = gameObject.transform.position + new Vector3(-2, 0, 0);
        }
    }

    public override void StartInteract() 
    {
        if(!flag) 
        {
            flag = true;
            if(!hasOrder)
            {
                UiManager.Instance.OpenUI(dialog);
                if(HasGuest)
                {
                    dialog.ShowYesNoButtons();
                    isOrderDialogOn = true;
                    MakeNewOrder();
                    dialog.OnClickYes = () => 
                    {
                        isOrderDialogOn = false;
                        hasOrder = true;
                        StartCoroutine(GuestLeave());
                        EndInteract();
                    };
                    dialog.OnClickNo = () => 
                    {
                        isOrderDialogOn = false;
                        GameManager.Instance.GivePenalty();
                        StartCoroutine(GuestGo());
                        EndInteract();
                    };
                }
                else
                {
                    dialog.SetText("손님이 없어요.");
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
        if(isOrderDialogOn) return;
        if(flag)
        {
            flag = false;
            UiManager.Instance.CloseUI(dialog);
            UiManager.Instance.CloseUI(cakelist);
        }
    }

    public void MakeNewOrder() 
    {
        orderBase = GameManager.Instance.unlockBaseCode[rand.Next(0, GameManager.Instance.unlockBaseCode.Count)];
        orderIcing = GameManager.Instance.unlockIcingCode[rand.Next(0, GameManager.Instance.unlockIcingCode.Count)];
        orderTopping = GameManager.Instance.unlockToppingCode[rand.Next(0, GameManager.Instance.unlockToppingCode.Count)];

        System.String orderText = "";
        switch(GameManager.Instance.orderSystem)
        {
            case 0:
                orderText = NewKeywordOrder();
                break;
            case 1:
                orderText = (rand.Next(0, 2) == 0 ? NewKeywordOrder() : NewFlavorTextOrder());
                break;
            case 2:
                orderText = NewMixedOrder();
                break;
        }
        dialog.SetText(orderText);
    }

    private System.String NewKeywordOrder()
    {
        System.String ret =
        (Util.GetItem(orderTopping) as ProcessedItem).Keyword + " " +
        (Util.GetItem(orderIcing) as ProcessedItem).Keyword + " " +
        (Util.GetItem(orderBase) as ProcessedItem).Keyword + " 주세요.";
        return ret;
    }

    private System.String NewFlavorTextOrder()
    {
        System.String ret =
        (Util.GetItem(orderTopping) as ProcessedItem).FlavorWord + " " +
        (Util.GetItem(orderIcing) as ProcessedItem).FlavorWord + " " +
        (Util.GetItem(orderBase) as ProcessedItem).FlavorWord + " 주세요.";
        return ret;
    }

    private System.String NewMixedOrder()
    {
        ProcessedItem t = Util.GetItem(orderTopping) as ProcessedItem;
        ProcessedItem i = Util.GetItem(orderIcing) as ProcessedItem;
        ProcessedItem b = Util.GetItem(orderBase) as ProcessedItem;
        int dice = rand.Next(1, 7);
        System.String ret =
        (((dice & 1 << 0) == 0) ? t.Keyword : t.FlavorWord) + " " +
        (((dice & 1 << 1) == 0) ? i.Keyword : i.FlavorWord) + " " +
        (((dice & 1 << 2) == 0) ? b.Keyword : b.FlavorWord) + " 주세요.";
        return ret;
    }

    public void SellCake(Cake cake)
    {
        if(cake != null)
        {
            float price = cake.GetPrice(orderBase, orderIcing, orderTopping);
            Util.EarnMoney(price);
            UiManager.Instance.CloseUI(cakelist);
            UiManager.Instance.OpenUI(dialog);
            dialog.SetText("잘 먹겠습니다~");
            StartCoroutine(GuestGo());
            Debug.Log(System.String.Format("현재 돈: {0}", PlayerManager.Instance.GetMoney()));
        }
    }

    private IEnumerator GuestLeave()
    {
        yield return new WaitForSeconds(rand.Next
        (
            TimeManager.Instance.GuestLeaveTimeStart,
            TimeManager.Instance.GuestLeaveTimeEnd
        ));

        if(HasGuest)
        {
            GameManager.Instance.GivePenalty();
            StartCoroutine(GuestGo());
        }
    }

    private IEnumerator GuestGo()
    {
        if(hasOrder)
        {
            hasOrder = false;
            HasGuest = false;
            yield return StartCoroutine(ProcessManager.Instance.MoveProcess(
                GuestObject, 
                gameObject.transform.position + new Vector3(-14, 0, 0),
                3.0f
            ));
            GuestObject.SetActive(false);
        }
    }
}
