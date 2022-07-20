using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicianUI : BaseUI
{ 
    private ItemSlotComponent input, output;
    private List<ItemSlotComponent> iscs;
    private GameObject processButton;

    void Awake() 
    {
        //item = new _Item(101, "Mud", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "진흙이다.");
        //item2 = new _Item(201, "Cucumber", ItemLevel.B, Resources.Load<Sprite>("Sprites/Cucumber"), "오이다.");
    }

    void Start()
    {
        processButton = GameObject.Find("ProcessButtonMagician");
        processButton.GetComponent<Button>().onClick.AddListener(Process);
    }

    void Update()
    {
        
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        if(iscs == null)
        {
            iscs = new List<ItemSlotComponent>();
            PageComponent page = new PageComponent(gameObject.transform, "Base", 4);
            PageComponent page2 = new PageComponent(gameObject.transform, "Icing");
            PageComponent page3 = new PageComponent(gameObject.transform, "Topping");
            PageComponent[] pgs = new PageComponent[3] { page, page2, page3 };
            PaginationComponent pgc = new PaginationComponent(gameObject.transform, pgs);
            pgc.SetPosition(-230, 145);
            ItemSlotComponent isc;

            //item = new _Item(304, "Test Topping", ItemLevel.A, Resources.Load<Sprite>("Sprites/Cucumber"), "It is Test Topping.");

            input = new ItemSlotComponent(gameObject.transform, 10001, 1);
            input.SetPosition(120.0f, 80.0f);
            output = new ItemSlotComponent(gameObject.transform, 10002, 2);
            output.SetPosition(280.0f, 80.0f);

            isc = new ItemSlotComponent(page.Container, 81001, 1, true);
            isc = new ItemSlotComponent(page.Container, 81001, 2, true);
            isc = new ItemSlotComponent(page.Container, 81001, 3, true);
            isc = new ItemSlotComponent(page.Container, 81001, 4, true);
            isc = new ItemSlotComponent(page.Container, 81001, 5, true);
            isc = new ItemSlotComponent(page.Container, 81001, 6, true);

            /*for(int i=0; i<SaveManager.Instance.NumberOfRBase.Length; i++)
            {
                item = new _Item(100 + i, "Test Base", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "It is Test Base.");
                isc = new ItemSlotComponent(page.Container, item, SaveManager.Instance.NumberOfRBase[i], true);
                isc.SetOnClick(() => input.LoadItem(new _Item(100 + i, "Test Base", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "It is Test Base."), 1));
                iscs.Add(isc);
            }
            for(int i=0; i<SaveManager.Instance.NumberOfRIcing.Length; i++)
            {
                item = new _Item(200 + i, "Test Icing", ItemLevel.B, Resources.Load<Sprite>("Sprites/Cucumber"), "It is Test Icing.");
                isc = new ItemSlotComponent(page2.Container, item, SaveManager.Instance.NumberOfRIcing[i]);
                iscs.Add(isc);
            }
            for(int i=0; i<SaveManager.Instance.NumberOfRTopping.Length; i++)
            {
                item = new _Item(300 + i, "Test Topping", ItemLevel.A, Resources.Load<Sprite>("Sprites/Cucumber"), "It is Test Topping.");
                isc = new ItemSlotComponent(page3.Container, item, SaveManager.Instance.NumberOfRTopping[i]);
                iscs.Add(isc);
            }*/
        }
        
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
    }

    private void Process() 
    {
        if(Util.CountItem(input.ItemCode) >= input.ItemCount) 
        {
            Util.UseItem(input.ItemCode, input.ItemCount);
            Util.AddItem(output.ItemCode, output.ItemCount);
        }
    }
}
