using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    TextMeshProUGUI moneyText;
    private float money;

    // Start is called before the first frame update
    void Start()
    {
        PlayerManager.Instance.moneyUI = this;
        moneyText = this.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public void MoneyTextUpdate(float money)
    {
        moneyText.text = money.ToString();
    }
}
