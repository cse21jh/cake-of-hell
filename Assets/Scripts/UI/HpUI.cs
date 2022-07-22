using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    public Slider HpBar;
    private float MaxHp;
    private float Hp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MaxHp = PlayerManager.Instance.GetMaxHp();
        Hp = PlayerManager.Instance.GetHp();

        HpBar.value = Hp / MaxHp;
    }
}
