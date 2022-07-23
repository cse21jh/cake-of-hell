using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : BaseUI
{
    TextMeshProUGUI moneyText;

    void Start()
    {
        PlayerManager.Instance.moneyUI = this;
        moneyText = this.GetComponent<TextMeshProUGUI>();
        MoneyTextUpdate(PlayerManager.Instance.GetMoney());
    }

    public void MoneyTextUpdate(float money)
    {
        moneyText.text = money.ToString();
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
