using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade
{
    public int MaxLevel { get; set; }
    public int CurrentLevel { get; set; }
    public int Price { get; set; }
    public string UpgradeText { get; set; }
    public IEnumerator UpgradeFunc { get; set; }
    public bool IsUnlocked { get; set; }

    public Upgrade(int maxLevel, int currentLevel, int price, string upgradeText, IEnumerator upgradeFunc, bool isUnlocked = false)
    {
        MaxLevel = maxLevel;
        CurrentLevel = currentLevel;
        Price = price;
        UpgradeText = upgradeText;
        UpgradeFunc = upgradeFunc;
        IsUnlocked = isUnlocked;
    }

}
