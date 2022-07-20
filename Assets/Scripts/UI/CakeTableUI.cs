using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CakeTableUI : BaseUI
{ 
    private ItemSlotComponent baseInput, icingInput, toppingInput;
    private PaginationComponent pagination;
    private PageComponent[] pages;
    private CakeSlotComponent[] cakes;
    private GameObject inventoryPanel, bakeButton, matName, matDesc;

    void Awake() 
    {
        
    }

    void Start()
    {
        inventoryPanel = GameObject.Find("CakeInventoryPanel");
        bakeButton = GameObject.Find("BakeButton");
        matName = GameObject.Find("MaterialName");
        matDesc = GameObject.Find("MaterialDesc");
        bakeButton.GetComponent<Button>().onClick.AddListener(Bake);
        MakeUI();
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

        baseInput = new ItemSlotComponent(gameObject.transform, 0, -1);
        icingInput = new ItemSlotComponent(gameObject.transform, 0, -1);
        toppingInput = new ItemSlotComponent(gameObject.transform, 0, -1);
        baseInput.SetPosition(-275, -50);
        icingInput.SetPosition(-200, -50);
        toppingInput.SetPosition(-125, -50);

        ItemSlotComponent isc;
        foreach(var pair in SaveManager.Instance.NumberOfBase) 
        {
            isc = new ItemSlotComponent(pages[0].Container, pair.Key, pair.Value, true);
            isc.SetOnClick(() => baseInput.LoadItem(pair.Key, -1));
        }
        foreach(var pair in SaveManager.Instance.NumberOfIcing) 
        {
            isc = new ItemSlotComponent(pages[1].Container, pair.Key, pair.Value, true);
            isc.SetOnClick(() => icingInput.LoadItem(pair.Key, -1));
        }
        foreach(var pair in SaveManager.Instance.NumberOfTopping) 
        {
            isc = new ItemSlotComponent(pages[2].Container, pair.Key, pair.Value, true);
            isc.SetOnClick(() => toppingInput.LoadItem(pair.Key, -1));
        }

        cakes = new CakeSlotComponent[5];
        for(int i=0; i<5; i++)
        {
            int ypos = -100 + 50 * i;
            cakes[i] = new CakeSlotComponent(gameObject.transform, 0, 0, 0);
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
        Debug.Log("Cake Baked!");
    }
}
