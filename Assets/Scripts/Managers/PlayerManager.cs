using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    //Fix me
    public Player player;
    private GameObject imageHit;
    public HpUI hpUI;
    public MoneyUI moneyUI;


    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public IEnumerator DamagedEffect()
    {
        imageHit = GameObject.Find("Canvas").transform.Find("ImageHit").gameObject;
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        imageHit.SetActive(false);
    }

    public void GetDamage(float value)
    {
        StartCoroutine("DamagedEffect");
        SetHp(player.Hp - value);
        if (player.Hp <= 0)
        {
            Debug.Log("Die");
            // 체력 0 되었을 때에 대한 내용 여기에 구현
        }
    }

    public float GetHp()
    {
        return player.Hp;
    }

    public void SetHp(float hp)
    {
        player.Hp = hp;
        if(hpUI!=null)
            hpUI.HpBarUpdate(GetMaxHp(), GetHp());
    }

    public float GetMaxHp()
    {
        return player.MaxHp;
    }

    public void SetMaxHp(float maxHp)
    {
        player.MaxHp = maxHp;
    }

    public float GetMoney()
    {
        return player.Money;
    }

    public void SetMoney(float money)
    {
        player.Money = money;
        if(moneyUI != null)
            moneyUI.MoneyTextUpdate(money);
    }



    public int GetNumberOfItem(int code)
    {
        switch(code/1000)
        {
            case 1:
                return player.NumberOfBase[code];
            case 2:
                return player.NumberOfIcing[code];
            case 3:
                return player.NumberOfTopping[code];
            case 4:
                return player.NumberOfRaw[code];
        }
        return -1;  
    }

    public void SetNumberOfItem(int code, int number)
    {
        switch (code / 1000)
        {
            case 1:
                player.NumberOfBase[code] = number;
                break;
            case 2:
                player.NumberOfIcing[code] = number;
                break;
            case 3:
                player.NumberOfTopping[code] = number;
                break;
            case 4:
                player.NumberOfRaw[code] = number;
                break;
        }
    }

    public void AddCake(Cake inputCake)
    {
        for (int i = 0; i < 5; i++)
        {
            if (player.CakeList[i] == null)
            {
                player.CakeList[i] = inputCake;
                return;
            }
        }
        return;
    }

    public Cake UseCake(int index)
    {
        Cake useCake = player.CakeList[index];
        player.CakeList[index] = null;
        return useCake;
    }

    public bool CanMake()
    {
        for (int i = 0; i < 5; i++)
        {
            if (player.CakeList[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public Cake GetCake(int index)
    {
        return player.CakeList[index];
    }

    public Player GetPlayer()
    {
        return player;
    }
}
