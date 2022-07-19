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

    public List<int> NumberOfIcing { get; set; } = new List<int>();

    public List<int> NumberOfTopping { get; set; } = new List<int>();

    public List<int> NumberOfBase { get; set; } = new List<int>();

    public List<int> NumberOfRaw { get; set; } = new List<int>();

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
            return NumberOfRaw[order];
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code/10000)
            {
                case (1):
                    return NumberOfBase[order];
                    break;
                case (2):
                    return NumberOfTopping[order];
                    break;
                case (4):
                    return NumberOfIcing[order];
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
            NumberOfRaw[order] = number;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code / 10000)
            {
                case (1):
                    NumberOfBase[order] = number;
                    break;
                case (2):
                    NumberOfTopping[order] = number;
                    break;
                case (4):
                    NumberOfIcing[order] = number;
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
            NumberOfRaw[order] = NumberOfRaw[order]+ 1;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code/10000)
            {
                case (1):
                    NumberOfBase[order] = NumberOfBase[order]+1;
                    break;
                case (2):
                    NumberOfTopping[order] = NumberOfTopping[order]+ 1;
                    break;
                case (4):
                    NumberOfIcing[order] = NumberOfIcing[order]+ 1;
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
            NumberOfRaw[order] = NumberOfRaw[order]- 1;
            return;
        }
        else
        {
            ProcessedItem processedItem = ItemManager.Instance.GetProcessedItem(code);
            int order = processedItem.GetOrder();
            switch (code / 10000)
            {
                case (1):
                    NumberOfBase[order] = NumberOfBase[order]- 1;
                    break;
                case (2):
                    NumberOfTopping[order] = NumberOfTopping[order]- 1;
                    break;
                case (4):
                    NumberOfIcing[order] = NumberOfIcing[order]- 1;
                    break;
            }
            return;
        }
    }
}