using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    //private float maxHp = 100f;
    public float MaxHp { get; set; } = 100f;

    //private float hp = 100f;
    public float Hp { get; set; } = 100f;

    //private float speed = 5f;
    public float Speed { get; set; } = 5f;

    public float AttackDamage { get; set; } = 10f;

    public float AttackRange { get; set; } = 1.0f;

    public int Money { get; set; } = 0;

    public Dictionary<int,int> NumberOfIcing { get; set; } = new Dictionary<int,int>();

    public Dictionary<int,int> NumberOfTopping { get; set; } = new Dictionary<int,int>();

    public Dictionary<int,int> NumberOfBase { get; set; } = new Dictionary<int,int>();

    public Dictionary<int,int> NumberOfRaw { get; set; } = new Dictionary<int,int>();

    public Cake[] CakeList { get; set; } = new Cake[5];

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetNumberOfItem(int code)
    {
        if (code / 10000 == 8)
        {
            RawItem item = ItemManager.Instance.GetRawItem(code);
            int order = item.GetOrder();
            return NumberOfRaw[code];
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code/10000)
            {
                case (1):
                    return NumberOfBase[code];
                    break;
                case (2):
                    return NumberOfTopping[code];
                    break;
                case (4):
                    return NumberOfIcing[code];
                    break;
            }
        }
        return -1;
    }

    public void SetNumberOfItem(int code, int number)
    {
        if (code / 10000 == 8)
        {
            RawItem item = ItemManager.Instance.GetRawItem(code);
            int order = item.GetOrder();
            NumberOfRaw[code] = number;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code / 10000)
            {
                case (1):
                    NumberOfBase[code] = number;
                    break;
                case (2):
                    NumberOfTopping[code] = number;
                    break;
                case (4):
                    NumberOfIcing[code] = number;
                    break;
            }
            return;
        }
    }

    public void AddNumberOfItem(int code)
    {
        if (code / 10000 == 8)
        {
            RawItem item = ItemManager.Instance.GetRawItem(code);
            int order = item.GetOrder();
            NumberOfRaw[code] = NumberOfRaw[code] + 1;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code/10000)
            {
                case (1):
                    NumberOfBase[code] = NumberOfBase[code] +1;
                    break;
                case (2):
                    NumberOfTopping[code] = NumberOfTopping[code] + 1;
                    break;
                case (4):
                    NumberOfIcing[code] = NumberOfIcing[code] + 1;
                    break;
            }
            return;
        }
    }

    public void UseNumberOfItem(int code)
    {
        if (code / 10000 == 8)
        {
            RawItem item = ItemManager.Instance.GetRawItem(code);
            int order = item.GetOrder();
            if(NumberOfRaw[code]<=0)
            {
                Debug.Log("보유한 아이템이 없습니다.");
                return;
            }
            NumberOfRaw[code] = NumberOfRaw[code] - 1;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code / 10000)
            {
                case (1):
                    if (NumberOfBase[code] <= 0)
                    {
                        Debug.Log("보유한 아이템이 없습니다.");
                        return;
                    }
                    NumberOfBase[code] = NumberOfBase[code] - 1;
                    break;
                case (2):
                    if (NumberOfTopping[code] <= 0)
                    {
                        Debug.Log("보유한 아이템이 없습니다.");
                        return;
                    }
                    NumberOfTopping[code] = NumberOfTopping[code] - 1;
                    break;
                case (4):
                    if (NumberOfIcing[code] <= 0)
                    {
                        Debug.Log("보유한 아이템이 없습니다.");
                        return;
                    }
                    NumberOfIcing[code] = NumberOfIcing[code] - 1;
                    break;
            }
            return;
        }
    }

    public void AddCake(Cake inputCake)
    {
        for(int i= 0;i<5;i++)
        {
            if(CakeList[i]==null)
            {
                CakeList[i] = inputCake;
                return;
            }
        }
        return;
    }

    public Cake UseCake(int index)
    {
        Cake useCake = CakeList[index];
        CakeList[index] = null;
        return useCake;
    }

    public bool CanMake()
    {
        for (int i = 0; i < 5; i++)
        {
            if (CakeList[i] == null)
            {
                 return true;
            }
        }
        return false;
    }
}