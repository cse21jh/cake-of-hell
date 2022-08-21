using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake 
{
    public int BaseCode { get; }
    public int IcingCode { get; }
    public int ToppingCode { get; }

    public Sprite BaseImage { get; }
    public Sprite IcingImage { get; }
    public Sprite ToppingImage { get; }

    public Cake(int baseCode, int icingCode, int toppingCode, Sprite baseImage, Sprite icingImage, Sprite toppingImage)
    {
        BaseCode = baseCode;
        IcingCode = icingCode;
        ToppingCode = toppingCode;

        BaseImage = baseImage;
        IcingImage = icingImage;
        ToppingImage = toppingImage;
    }

    public float GetPrice(int orderBaseCode, int orderIcingCode, int orderToppingCode)
    {
        float price = 0f;
        int correct = 0;
        if (orderBaseCode == BaseCode)
        { 
            price += ItemManager.Instance.GetPriceOfProcessedItem(BaseCode);
            correct++;
        }
        if (orderToppingCode == ToppingCode)
        { 
            price += ItemManager.Instance.GetPriceOfProcessedItem(ToppingCode);
            correct++;
        }
        if (orderIcingCode == IcingCode)
        { 
            price += ItemManager.Instance.GetPriceOfProcessedItem(IcingCode);
            correct++;
        }

        if(correct==3)
        {
            GameManager.Instance.AddNumberOfSatisfiedCustomer();
            if((GameManager.Instance.increaseReputationCount+=1) == 5)
            {
                Util.IncreaseReputation();
                GameManager.Instance.increaseReputationCount = 0;
            }
        }
        else
        {
            GameManager.Instance.increaseReputationCount = 0;
        }
        GameManager.Instance.AddNumberOfSoldCake();

        return price*((correct*0.2f)+1);
    }

    public int GetSatisfaction(int orderBaseCode, int orderIcingCode, int orderToppingCode)
    {
        int correct = 0;
        if (orderBaseCode == BaseCode)
            correct++;
        if (orderToppingCode == ToppingCode)
            correct++;
        if (orderIcingCode == IcingCode)
            correct++;
        return correct;
    }
}
