using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CakeTableUI : BaseUI
{ 
    private ItemSlotComponent baseInput, icingInput, toppingInput;
    private PaginationComponent pagination;
    private PageComponent[] pages;
    private CakeSlotComponent[] cakes;
    private GameObject inventoryPanel, bakeButton;
    private TMP_Text matName, matDesc;
    private Sprite spriteBase, spriteIcing, spriteTopping, spriteNull;
    private Image bigImgBase, bigImgIcing, bigImgTopping;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    void Awake() 
    {
        
    }

    void Start()
    {
        Util.AddItem(10001, 10);
        Util.AddItem(20003, 10);
        Util.AddItem(40006, 10);
        itemSlots = new Dictionary<int, ItemSlotComponent>();

        inventoryPanel = GameObject.Find("CakeInventoryPanel");
        bakeButton = GameObject.Find("BakeButton");
        matName = GameObject.Find("MaterialName").GetComponent<TMP_Text>();
        matDesc = GameObject.Find("MaterialDesc").GetComponent<TMP_Text>();
        bigImgBase = GameObject.Find("BaseBigImage").GetComponent<Image>();
        bigImgIcing = GameObject.Find("IcingBigImage").GetComponent<Image>();
        bigImgTopping = GameObject.Find("ToppingBigImage").GetComponent<Image>();
        bakeButton.GetComponent<Button>().onClick.AddListener(Bake);
        MakeUI();

        spriteBase = Resources.Load<Sprite>("Sprites/Cake/Base/Base_mud");
        spriteIcing = Resources.Load<Sprite>("Sprites/Cake/Icing/Icing_poison");
        spriteTopping = Resources.Load<Sprite>("Sprites/Cake/Topping/Topping_redcone");

        spriteNull = Resources.Load<Sprite>("Sprites/Nothing");
    }

    void Update()
    {
        
    }

    public void MakeUI() 
    {
        pages = new PageComponent[3];
        pages[0] = new PageComponent(inventoryPanel.transform, "Base", 4, 250);
        pages[1] = new PageComponent(inventoryPanel.transform, "Icing", 4, 250);
        pages[2] = new PageComponent(inventoryPanel.transform, "Topping", 4, 250);
        for(int i=0; i<3; i++) 
        {
            pages[i].SetPosition(0, 25);
        }

        pagination = new PaginationComponent(inventoryPanel.transform, pages);
        pagination.SetPosition(0, 165);

        baseInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        icingInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        toppingInput = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        baseInput.SetOnClick(() => { baseInput.Clear(); bigImgBase.sprite = spriteNull; });
        icingInput.SetOnClick(() => { icingInput.Clear(); bigImgIcing.sprite = spriteNull; });
        toppingInput.SetOnClick(() => { toppingInput.Clear(); bigImgTopping.sprite = spriteNull; });
        baseInput.SetPosition(-275, -50);
        icingInput.SetPosition(-200, -50);
        toppingInput.SetPosition(-125, -50);

        foreach(var pair in SaveManager.Instance.NumberOfBase) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[0].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    baseInput.LoadItem(pair.Key, -1);
                    bigImgBase.sprite = spriteBase;
                    matName.text = Util.GetItem(pair.Key).Name;
                    matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                });
            }
        }
        foreach(var pair in SaveManager.Instance.NumberOfIcing) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[1].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    icingInput.LoadItem(pair.Key, -1);
                    bigImgIcing.sprite = spriteIcing;
                    matName.text = Util.GetItem(pair.Key).Name;
                    matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                });
            }
        }
        foreach(var pair in SaveManager.Instance.NumberOfTopping) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(pages[2].Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    toppingInput.LoadItem(pair.Key, -1);
                    bigImgTopping.sprite = spriteTopping;
                    matName.text = Util.GetItem(pair.Key).Name;
                    matDesc.text = (Util.GetItem(pair.Key) as ProcessedItem).FlavorText;
                });
            }
        }

        cakes = new CakeSlotComponent[5];
        for(int i=0; i<5; i++)
        {
            int ypos = 100 - 50 * i;
            cakes[i] = new CakeSlotComponent(gameObject.transform);
            cakes[i].SetPosition(-375, ypos);
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Cake Table UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Cake Table UI Closed!");
    }

    private void Bake() 
    {
        if(SaveManager.Instance.CanMake() && baseInput.ItemCode > 0 && icingInput.ItemCode > 0 && toppingInput.ItemCode > 0) 
        {
            Cake cake = new Cake(baseInput.ItemCode, toppingInput.ItemCode, icingInput.ItemCode, spriteBase, spriteTopping, spriteIcing);
            for(int i=0; i<5; i++) 
            {
                if(SaveManager.Instance.CakeList[i] == null) 
                {
                    cakes[i].SetCake(cake);
                    break;
                }
            }

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

            SaveManager.Instance.AddCake(cake);
            Debug.Log("Cake Baked!");
        }
        else 
        {
            Debug.Log("Cannot bake a cake.");
        }
    }
}
