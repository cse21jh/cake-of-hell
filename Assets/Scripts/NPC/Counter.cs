using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : NPC
{
    public GameObject CakeListObject;
    private System.Random rand;
    private DialogUI dialog; 
    private CakeListUI cakelist;
    private int orderBase, orderIcing, orderTopping;
    private bool hasOrder = false;
    private bool flag = false;
    private bool isOrderDialogOn = false;
    private Sprite[] guestSprites;

    public bool HasGuest { get; set; } = true;
    public int SpriteNumber { get; set; }
    public int GuestNumber { get; set; } = 0;
    public GameObject GuestObject { get; set; }
    public SpriteRenderer GuestSprite { get; set; }

    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        rand = new System.Random();
        dialog = canvas.transform.Find("DialogUI").GetComponent<DialogUI>();
        cakelist = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/CakeListUI"), canvas.transform).GetComponent<CakeListUI>();
        cakelist.SellCake = SellCake;
        GuestObject = Instantiate(ResourceLoader.GetPrefab("Prefabs/NPC/Guest"));
        GuestSprite = GuestObject.GetComponent<SpriteRenderer>();
        guestSprites = ResourceLoader.GetPackedSprite("Sprites/Guest/guests");
        SpriteNumber = rand.Next(0, 5);
        GuestSprite.sprite = guestSprites[2 * SpriteNumber + 1];
        if(!TimeManager.Instance.isPrepareTime)
        {
            GuestObject.transform.position = gameObject.transform.position + new Vector3(-2, 0.5f, 0);
            StartCoroutine(GuestLeave(0));
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
                if(HasGuest && !TimeManager.Instance.isPrepareTime)
                {
                    dialog.ShowYesNoButtons();
                    isOrderDialogOn = true;
                    MakeNewOrder();
                    dialog.OnClickYes = () => 
                    {
                        if(HasGuest)
                        {
                            isOrderDialogOn = false;
                            hasOrder = true;
                            EndInteract();
                        }
                    };
                    dialog.OnClickNo = () => 
                    {
                        if(HasGuest)
                        {
                            isOrderDialogOn = false;
                            GameManager.Instance.GivePenalty();
                            StartCoroutine(GuestGo());
                            EndInteract();
                        }
                    };
                }
                else
                {
                    dialog.SetText("손님이 없어요.");
                }
            }
            else
            {
                if(hasOrder)
                {
                    UiManager.Instance.OpenUI(cakelist);
                }
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
        if(hasOrder && cake != null)
        {
            float price = cake.GetPrice(orderBase, orderIcing, orderTopping);
            int satisfaction = cake.GetSatisfaction(orderBase, orderIcing, orderTopping);
            Util.EarnMoney(price);
            UiManager.Instance.CloseUI(cakelist);
            UiManager.Instance.OpenUI(dialog);
            switch(satisfaction)
            {
                case 0:
                    dialog.SetText("형편없군. 이런 것도 케이크라고 파는 건가?");
                    break;
                case 1:
                    dialog.SetText("못 먹을 정도는 아니지만, 기분이 썩 좋은 맛은 아니군.");
                    break;
                case 2:
                    dialog.SetText("어딘가 2% 부족하긴 하지만, 괜찮은 맛이야.");
                    break;
                case 3:
                    dialog.SetText("내가 원했던 딱 그 맛이네. 정말 맛있군!");
                    break;
            }
            StartCoroutine(GuestGo());
            Debug.Log(System.String.Format("현재 돈: {0}", PlayerManager.Instance.GetMoney()));
        }
    }

    public IEnumerator GuestLeave(int num)
    {
        yield return new WaitForSeconds(rand.Next
        (
            TimeManager.Instance.GuestLeaveTimeStart,
            TimeManager.Instance.GuestLeaveTimeEnd
        ));

        if(GuestNumber == num && HasGuest)
        {
            if(flag)
            {
                flag = false;
                UiManager.Instance.CloseUI(dialog);
                UiManager.Instance.CloseUI(cakelist);
            }
            GameManager.Instance.GivePenalty();
            StartCoroutine(GuestGo());
        }
    }

    private IEnumerator GuestGo()
    {
        GuestSprite.sprite = guestSprites[2 * SpriteNumber];
        hasOrder = false;
        yield return StartCoroutine(ProcessManager.Instance.MoveProcess(
            GuestObject, 
            gameObject.transform.position + new Vector3(-14, 0.5f, 0),
            3.0f
        ));
        HasGuest = false;
        GuestObject.SetActive(false);
    }
}
