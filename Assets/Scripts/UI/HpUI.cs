using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public Slider hpBar;    // edit needed

    public void HpBarUpdate(float maxHp, float hp)
    {
        hpBar.value = hp / maxHp;
    }
}
