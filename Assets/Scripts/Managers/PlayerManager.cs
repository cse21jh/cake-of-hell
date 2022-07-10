using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Player player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
        player.Speed += value;
        SaveManager.Instance.Speed += value;
    }

    public void ChangeHp(float value)
    {
        player.Hp += value;
        SaveManager.Instance.Hp += value;
    }

    public void ChangeMaxHp(float value)
    {
        player.MaxHp += value;
        SaveManager.Instance.MaxHp += value;
    }

    public void GetDamage(float value)
    {
        // ���� �ǰ� ȿ�� �ִ´ٸ� ���⿡ ����
        player.Hp -= value;
        SaveManager.Instance.Hp -= value;
        if (player.Hp <= 0)
        {
            Debug.Log("Die");
            // ü�� 0 �Ǿ��� ���� ���� ���� ���⿡ ����
        }
    }

}
