using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndDayUI : BaseUI, ISingleOpenUI
{
    private TMP_Text numberOfSoldCake;
    private TMP_Text reputation;

    void Awake()
    {
        numberOfSoldCake = GameObject.Find("NumberOfSoldCake").GetComponent<TMP_Text>();
        numberOfSoldCake.text = "X" + GameManager.Instance.soldCakeInADay.ToString();
        reputation = GameObject.Find("Reputation").GetComponent<TMP_Text>();
        reputation.text = "가게 평판 : " + TimeManager.Instance.reputation.ToString("0.0") + " / 5";
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
