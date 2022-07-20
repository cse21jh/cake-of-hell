using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cake 
{
    public int BaseCode { get; }
    public int ToppingCode { get; }
    public int IcingCode { get; }

    public Sprite BaseImage { get; }
    public Sprite ToppingImage { get; }
    public Sprite IcingImage { get; }

    public Cake(int baseCode, int toppingCode, int icingCode, Sprite baseImage, Sprite toppingImage, Sprite icingImage)
    {
        BaseCode = baseCode;
        ToppingCode = toppingCode;
        IcingCode = icingCode;
        BaseImage = baseImage;
        ToppingImage = toppingImage;
        IcingImage = icingImage;
    }

    public int GetPrice(int orderBaseCode, int orderToppingCode, int orderIcingCode)
    {
        int price = 0;
        if (orderBaseCode == BaseCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(BaseCode);
        if (orderToppingCode == ToppingCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(ToppingCode);
        if (orderIcingCode == IcingCode)
            price += ItemManager.Instance.GetPriceOfProcessedItem(IcingCode);

        return price;
    }
}
