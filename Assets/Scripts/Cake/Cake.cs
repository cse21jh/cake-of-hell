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
        if (orderBaseCode == BaseCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(BaseCode);
        if (orderToppingCode == ToppingCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(ToppingCode);
        if (orderIcingCode == IcingCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(IcingCode);

        return price;
    }
}
