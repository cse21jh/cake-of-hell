using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private bool invincible;
    private GameObject imageHit;

    public bool[] isReserved = new bool[5];
    public Player player;
    public HpUI hpUI;
    public MoneyUI moneyUI;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        invincible = false;
    }

    public IEnumerator DamagedEffect()
    {
        imageHit = GameObject.Find("Canvas").transform.Find("ImageHit").gameObject;
        imageHit.SetActive(true);
        invincible = true;
        for (int i = 1; i <= 10; i++)
        {
            float f = i % 2 == 0 ? 1f : 0.5f; 
            Color c = player.spriteRenderer.material.color;
            c.a = f;
            player.spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
            if(i == 4)
            {
                imageHit.SetActive(false);
            }
        }
        imageHit.SetActive(false);
        invincible = false;
    }

    public void GetDamage(float value)
    {
        if(value<=0)
        {
            return;
        }
        SetHp(player.Hp - value);
        if (player.Hp <= 0)
        {
            Die();
        }
        if (!invincible && value != 0 && player.Hp > 0)
        { 
            StartCoroutine("DamagedEffect");
            SoundManager.Instance.PlayEffect("PlayerHit");
        }
    }

    public float GetHp()
    {
        return player.Hp;
    }

    public void SetHp(float hp)
    {
        player.Hp = hp;
        if(hpUI != null)
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

    public float GetSpeed()
    {
        return player.Speed;
    }

    public float GetRealSpeed()
    {
        return player.RealSpeed;
    }

    public void SetSpeed(float speed)
    {
        player.Speed = speed;
    }

    public void SetRealSpeed(float speed)
    {
        player.RealSpeed = speed;
    }

    public void SetMoney(float money)
    {
        if(money < 0)
        {
            player.Money = 0;
            return;
        }
        player.Money = money;
        if(moneyUI != null)
            moneyUI.MoneyTextUpdate(money);
    }

    public float GetAttackDamage()
    {
        return player.AttackDamage;
    }

    public void SetAttackDamage(float attackDamage)
    {
        player.AttackDamage = attackDamage;
    }


    public int GetNumberOfItem(int code)
    {
        switch(code / 1000)
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
    public int GetNumberOfItemInADay(int code)
    {
        switch (code / 1000)
        {
            case 1:
                return player.NumberOfBaseInADay[code];
            case 2:
                return player.NumberOfIcingInADay[code];
            case 3:
                return player.NumberOfToppingInADay[code];
            case 4:
                return player.NumberOfRawInADay[code];
        }
        return -1;
    }
    public void SetNumberOfItemInADay(int code, int number)
    {
        switch (code / 1000)
        {
            case 1:
                player.NumberOfBaseInADay[code] = number;
                break;
            case 2:
                player.NumberOfIcingInADay[code] = number;
                break;
            case 3:
                player.NumberOfToppingInADay[code] = number;
                break;
            case 4:
                player.NumberOfRawInADay[code] = number;
                break;
        }
    }

    public void SetBackNumberOfItem()
    {
        foreach (var code in ItemManager.Instance.ItemCodeList)
        {
            SetNumberOfItem(code, GetNumberOfItem(code) - GetNumberOfItemInADay(code));
        }
    }

    public void ResetNumberOfItemInADay()
    {
        foreach (var code in ItemManager.Instance.ItemCodeList)
        {
            SetNumberOfItemInADay(code, 0);
        }
    }

    public bool GetPlayerInShop()
    {
        return player.inShop;
    }

    public void SetPlayerInShop(bool inShop)
    {
        player.inShop = inShop;
    }

    public Cake GetCake(int index)
    {
        return player.CakeList[index];
    }

    public void SetCake(int index, Cake cake)
    {
        player.CakeList[index] = cake;
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
            if (player.CakeList[i] == null && !isReserved[i])
            {
                return true;
            }
        }
        return false;
    }

    public int GetAvailableCakeIndex()
    {
        for (int i = 0; i < 5; i++)
        {
            if (player.CakeList[i] == null && !isReserved[i])
            {
                return i;
            }
        }
        return -1;
    }

    public Player GetPlayer()
    {
        return player;
    }

    public void Die()
    {
        GameManager.Instance.AddDieCount();
        SetBackNumberOfItem();
        ResetNumberOfItemInADay();
        TimeManager.Instance.timer = 0;
        TimeManager.Instance.restart = true;
        StartCoroutine(GameManager.Instance.DieLoadScene("Cake Shop"));
        if(GameManager.Instance.dieCount >= 3)
        {
            GameManager.Instance.MoveToEndingScene();
        }
        Debug.Log("Player Die");
        invincible = false;
    }

    public void DiePenalty()
    {
        SetMoney(GetMoney() - 100);
    }

    public void SetPlayerImage(int index)
    {
        player.spriteRenderer.sprite = player.playerImage[index];
    }
}
