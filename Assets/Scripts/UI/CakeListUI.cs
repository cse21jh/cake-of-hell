using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakeListUI : BaseUI, ISingleOpenUI
{
    private CakeSlotComponent[] cakes;
    public System.Action<Cake> SellCake { get; set; }

    void Awake()
    {
        cakes = new CakeSlotComponent[5];
        for(int i=0; i<5; i++)
        {
            int xpos = -100 + 50 * i;
            int j = i;
            cakes[i] = new CakeSlotComponent(gameObject.transform, true);
            cakes[i].SetPosition(xpos, 0);
            cakes[i].SetOnClick(() => 
            {
                SellCake(cakes[j].GetCake());
                PlayerManager.Instance.UseCake(j);
            });
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
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
        Debug.Log("Cake List UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Cake List UI Closed!");
    }
}
