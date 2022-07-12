using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianUI : BaseUI
{ 
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        _Item item = new _Item(101, "Mud", ItemLevel.C, Resources.Load<Sprite>("Sprites/Mud"), "진흙이다.");
        ItemSlotComponent isc = new ItemSlotComponent(gameObject, item, 1);
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
    }
}
