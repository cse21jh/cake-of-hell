using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    private Slider hpBar;    // edit needed

    void Start()
    {
        hpBar = gameObject.GetComponent<Slider>();
        PlayerManager.Instance.hpUI = this;
    }

    public void HpBarUpdate(float maxHp, float hp)
    {
        hpBar.value = hp / maxHp;
    }
}
