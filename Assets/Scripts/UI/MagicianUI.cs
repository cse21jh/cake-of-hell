using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUI : BaseUI
{ 
    private _Item item, item2;
    private List<ItemSlotComponent> iscs;

    void Awake() 
    {
        //item = new _Item(101, "Mud", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "진흙이다.");
        //item2 = new _Item(201, "Cucumber", ItemLevel.B, Resources.Load<Sprite>("Sprites/Cucumber"), "오이다.");
    }

    void Start()
    {
        
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
            PageComponent page = new PageComponent(gameObject.transform, "Base");
            PageComponent page2 = new PageComponent(gameObject.transform, "Icing");
            PageComponent page3 = new PageComponent(gameObject.transform, "Topping");
            PageComponent[] pgs = new PageComponent[3] { page, page2, page3 };
            PaginationComponent pgc = new PaginationComponent(gameObject.transform, pgs);
            ItemSlotComponent isc;
            for(int i=0; i<SaveManager.Instance.NumberOfRBase.Length; i++)
            {
                item = new _Item(100 + i, "Test Base", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "It is Test Base.");
                isc = new ItemSlotComponent(page.Container, item, SaveManager.Instance.NumberOfRBase[i]);
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
            }
        }
        else 
        {
            foreach(var isc in iscs) 
            {
                isc.SetActive(true);
            }
        }
        
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        foreach(var isc in iscs) 
        {
            isc.SetActive(false);
        }
        gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
    }
}
