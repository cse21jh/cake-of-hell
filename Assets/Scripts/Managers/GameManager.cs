using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeSpeed(float value)
    {
        Player.Instance.Speed += value;
        SaveManager.Instance.Speed += value;
    }

    public void ChangeHp(float value)
    {
        Player.Instance.Hp += value;
        SaveManager.Instance.Hp += value;
    }

    public void ChangeMaxHp(float value)
    {
        Player.Instance.MaxHp += value;
        SaveManager.Instance.MaxHp += value;
    }

    public void GetDamage(float value)
    {
        // 만약 피격 효과 넣는다면 여기에 삽입?
        Player.Instance.Hp -= value;
        SaveManager.Instance.Hp += value;
        if(Player.Instance.Hp<=0)
        {
            Debug.Log("Die");
            // 체력 0 되었을 때에 대한 내용 여기에 구현
        }
    }
}
