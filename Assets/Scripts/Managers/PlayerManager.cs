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

    private float invincibleTimer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        invincibleTimer = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }




    public IEnumerator DamagedEffect()
    {
        imageHit = GameObject.Find("Canvas").transform.Find("ImageHit").gameObject;
        imageHit.SetActive(true);
        for (int i = 1; i <= 10; i++)
        {
            float f = i % 2 == 0 ? 1f : 0.5f; 
            Color c = player.spriteRenderer.material.color;
            c.a = f;
            player.spriteRenderer.material.color = c;
            yield return new WaitForSeconds(0.1f);
            if(i==4)
            {
                imageHit.SetActive(false);
            }
            invincibleTimer += 0.1f;
        }
        imageHit.SetActive(false);
        invincibleTimer = 0f;
    }

    public void GetDamage(float value)
    {
        if (invincibleTimer == 0f && value!=0)
        { 
            StartCoroutine("DamagedEffect");
            SoundManager.Instance.PlayEffect("PlayerHit");
            SetHp(player.Hp - value);
            if (player.Hp <= 0)
            {
                Die();
            }
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

    public float GetSpeed()
    {
        return player.Speed;
    }

    public void SetMoney(float money)
    {
        if(money<0)
        {
            Debug.Log("Have no money");
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


    public Player GetPlayer()
    {
        return player;
    }

    public void Die()
    {
        GameManager.Instance.AddDieCount();
        Debug.Log("Player Die");
    }
}
