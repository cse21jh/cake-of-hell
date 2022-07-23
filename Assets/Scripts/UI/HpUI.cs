using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : BaseUI
{
    private Slider hpBar;    // edit needed

    void Start()
    {
        hpBar = gameObject.GetComponent<Slider>();
        PlayerManager.Instance.hpUI = this;
        HpBarUpdate(PlayerManager.Instance.GetMaxHp(), PlayerManager.Instance.GetHp());
    }

    public void HpBarUpdate(float maxHp, float hp)
    {
        hpBar.value = hp / maxHp;
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
