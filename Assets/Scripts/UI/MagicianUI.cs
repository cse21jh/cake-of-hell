using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUI : BaseUI
{ 
    private _Item item;
    private GameObject itemSlotPrefab; 
    private ItemSlotComponent isc;

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
        isc = new ItemSlotComponent(gameObject, itemSlotPrefab, item, 1);
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        isc.Destroy();
        Debug.Log("Magician UI Closed!");
    }
}
