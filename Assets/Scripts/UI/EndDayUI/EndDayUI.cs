using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayUI : BaseUI
{
    [SerializeField]
    private GameObject soldCakeScroll;
    void Start()
    {
        for(int i = 0;i<GameManager.Instance.soldCakeInADay;i++)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/UI/CakeImage"), soldCakeScroll.transform);
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.canMove = false;
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.canMove = true;
    }
}
