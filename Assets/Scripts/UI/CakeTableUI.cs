using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CakeTableUI : BaseUI, ISingleOpenUI
{
    private int tableNumber = 0;
    private ItemSlotComponent baseInput, icingInput, toppingInput;
    private PaginationComponent pagination;
    private PageComponent[] pages;
    private CakeSlotComponent[] cakes;
    private GameObject inventoryPanel, bakeButton;
    private GameObject[] progressCircles;
    private TMP_Text matName, matDesc;
    private Sprite spriteNull;
    private Image bigImgBase, bigImgIcing, bigImgTopping;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    void Awake()
    {
        spriteNull = Resources.Load<Sprite>("Sprites/Nothing");
        itemSlots = new Dictionary<int, ItemSlotComponent>();

        inventoryPanel = GameObject.Find("CakeInventoryPanel");
        bakeButton = GameObject.Find("BakeButton");
        matName = GameObject.Find("MaterialName").GetComponent<TMP_Text>();
        matDesc = GameObject.Find("MaterialDesc").GetComponent<TMP_Text>();
        bigImgBase = GameObject.Find("BaseBigImage").GetComponent<Image>();
        bigImgIcing = GameObject.Find("IcingBigImage").GetComponent<Image>();
        bigImgTopping = GameObject.Find("ToppingBigImage").GetComponent<Image>();
        bigImgBase.sprite = spriteNull;
        bigImgIcing.sprite = spriteNull;
        bigImgTopping.sprite = spriteNull;
        progressCircles = new GameObject[3];
        progressCircles[0] = GameObject.Find("ProgressCircleCake0");
        progressCircles[1] = GameObject.Find("ProgressCircleCake1");
        progressCircles[2] = GameObject.Find("ProgressCircleCake2");
        bakeButton.GetComponent<Button>().onClick.AddListener(Bake);

        MakeUI();
    }

    void Update()
    {
        
    }

    public void MakeUI() 
    {
        pages = new PageComponent[3];
        pages[0] = new PageComponent(inventoryPanel.transform, "베이스", 4, 250);
        pages[1] = new PageComponent(inventoryPanel.transform, "아이싱", 4, 250);
        pages[2] = new PageComponent(inventoryPanel.transform, "토핑", 4, 250);
        for(int i=0; i<3; i++) 
        {
            pages[i].SetPosition(0, 25);
        }

        pagination = new PaginationComponent(inventoryPanel.transform, pages, () => ClearText());
        pagination.SetPosition(0, 165);

        baseInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        icingInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        toppingInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        baseInput.SetOnClick(() => 
        { 
            if(!toppingInput.HasItem() && !icingInput.HasItem()) 
            { 
                baseInput.Clear(); 
                bigImgBase.sprite = spriteNull; 
            }
        });
        icingInput.SetOnClick(() => 
        { 
            if(!toppingInput.HasItem()) 
            { 
                icingInput.Clear(); 
                bigImgIcing.sprite = spriteNull; 
            } 
        });
        toppingInput.SetOnClick(() =>
        { 
            toppingInput.Clear(); 
            bigImgTopping.sprite = spriteNull; 
        });
        baseInput.SetPosition(-275, -50);
        icingInput.SetPosition(-200, -50);
        toppingInput.SetPosition(-125, -50);

        foreach(var pair in Util.GetItemNumbers(ItemType.Base)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[0].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    if(IsTableIdle()) 
                    {
                        baseInput.LoadItem(pair.Key);
                        bigImgBase.sprite = ItemManager.Instance.GetCakeSprite(pair.Key);
                        matName.text = Util.GetItem(pair.Key).Name;
                        matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                    }
                });
            }
        }
        foreach(var pair in Util.GetItemNumbers(ItemType.Icing)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[1].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    if(baseInput.HasItem() && IsTableIdle()) 
                    {
                        icingInput.LoadItem(pair.Key);
                        bigImgIcing.sprite = ItemManager.Instance.GetCakeSprite(pair.Key);
                        matName.text = Util.GetItem(pair.Key).Name;
                        matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                    }
                });
            }
        }
        foreach(var pair in Util.GetItemNumbers(ItemType.Topping)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[2].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    if(baseInput.HasItem() && icingInput.HasItem() && IsTableIdle()) 
                    {
                        toppingInput.LoadItem(pair.Key);
                        bigImgTopping.sprite = ItemManager.Instance.GetCakeSprite(pair.Key);
                        matName.text = Util.GetItem(pair.Key).Name;
                        matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                    }
                });
            }
        }

        cakes = new CakeSlotComponent[5];
        for(int i=0; i<5; i++)
        {
            int ypos = 100 - 50 * i;
            cakes[i] = new CakeSlotComponent(gameObject.transform);
            cakes[i].SetPosition(-370, ypos);
            if(PlayerManager.Instance.GetCake(i) != null) 
            {
                cakes[i].SetCake(PlayerManager.Instance.GetCake(i));
            }
        }

        CakeProcess proc = ProcessManager.Instance.CakeProcesses[tableNumber];
        if(proc != null) 
        {
            proc.Circle = progressCircles[tableNumber].GetComponent<ProgressCircle>();
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        UpdateSlots();
        pagination.ShowPage(0);
        matName.text = "";
        matDesc.text = "";
        Debug.Log("Cake Table UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        HoverItemName.Instance.gameObject.SetActive(false);
        Debug.Log("Cake Table UI Closed!");
    }

    public void SetTableNumber(int num)
    {
        tableNumber = num;
        for(int i=0; i<3; i++) 
        {
            if(i == tableNumber) progressCircles[i].SetActive(true);
            else progressCircles[i].SetActive(false);
        }
    }

    public void UpdateSlots()
    {
        for(int i=0; i<5; i++)
        {
            if(PlayerManager.Instance.GetCake(i) != null) 
            {
                cakes[i].SetCake(PlayerManager.Instance.GetCake(i));
            }
            else 
            {
                cakes[i].Clear();
            }
        }
    }

    private bool IsTableIdle()
    {
        return ProcessManager.Instance.CakeProcesses[tableNumber] == null || ProcessManager.Instance.CakeProcesses[tableNumber].IsEnded;
    }

    private void Bake() 
    {
        if(PlayerManager.Instance.CanMake() && baseInput.HasItem() && icingInput.HasItem() && toppingInput.HasItem() && IsTableIdle()) 
        {
            Cake cake = new Cake(baseInput.ItemCode, icingInput.ItemCode, toppingInput.ItemCode);

            itemSlots[baseInput.ItemCode].UseItem();
            itemSlots[icingInput.ItemCode].UseItem();
            itemSlots[toppingInput.ItemCode].UseItem();

            if(Util.CountItem(baseInput.ItemCode) == 0) 
            {
                itemSlots.Remove(baseInput.ItemCode);
            }
            if(Util.CountItem(icingInput.ItemCode) == 0) 
            {
                itemSlots.Remove(icingInput.ItemCode);
            }
            if(Util.CountItem(toppingInput.ItemCode) == 0) 
            {
                itemSlots.Remove(toppingInput.ItemCode);
            }

            baseInput.Clear();
            icingInput.Clear();
            toppingInput.Clear();

            bigImgBase.sprite = spriteNull;
            bigImgIcing.sprite = spriteNull;
            bigImgTopping.sprite = spriteNull;

            ProcessManager.Instance.AddCakeProcess
            (
                tableNumber,
                this,
                cake, 
                progressCircles[tableNumber].GetComponent<ProgressCircle>()
            );
            Debug.Log("Cake Baked!");
        }
        else 
        {
            Debug.Log("Cannot bake a cake.");
        }
    }

    public void ClearText()
    {
        matName.text = "";
        matDesc.text = "";
    }
}
