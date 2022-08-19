using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDayUI : BaseUI, ISingleOpenUI
{
    [SerializeField]
    private GameObject soldCakeScroll;

    void Start()
    {
        for(int i = 0; i<GameManager.Instance.soldCakeInADay; i++)
        {
            Instantiate(ResourceLoader.Instance.GetPrefab("CakeImage"), soldCakeScroll.transform);
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
