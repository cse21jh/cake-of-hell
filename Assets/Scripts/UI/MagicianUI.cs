using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUI : BaseUI
{ 
    private _Item item;
    private GameObject itemSlotPrefab; 
    private List<ItemSlotComponent> iscs;

    void Awake() 
    {
        item = new _Item(101, "Mud", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "진흙이다.");
        itemSlotPrefab = Resources.Load<GameObject>("Prefabs/ItemSlotPrefab");
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
        Debug.Log(itemSlotPrefab);
        if(iscs == null)
        {
            iscs = new List<ItemSlotComponent>();
            GameObject page = Object.Instantiate(Resources.Load<GameObject>("Prefabs/4col-PagePrefab"), gameObject.transform);
            ItemSlotComponent isc;
            page.transform.SetParent(gameObject.transform);
            for(int i=1; i<=10; i++) {
                for(int j=1; j<=4; j++) {
                    isc = new ItemSlotComponent(page.transform.GetChild(0).GetChild(0).gameObject, itemSlotPrefab, item, i * j);
                    iscs.Add(isc);
                }
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
