using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AddUpgradeButton : MonoBehaviour
{
    private GameObject upgradeButton;

    void Start()
    {
        upgradeButton = Resources.Load<GameObject>("Prefabs/UI/UpgradeButton");
        foreach (var i in GameManager.Instance.upgradeList)
        {
            if (i.MaxLevel != i.CurrentLevel && i.IsUnlocked)
            {
                GameObject upgrade = Instantiate(upgradeButton, this.transform);
                string explain = i.UpgradeText + " (" + i.CurrentLevel.ToString() + "/" + i.MaxLevel.ToString()+
                    ")\n" + i.Price.ToString();
                upgrade.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = explain;
                upgrade.gameObject.GetComponent<Button>().onClick.AddListener(()=>StartFunc(i.UpgradeFunc));
            }

        }
    }

    private void StartFunc(IEnumerator upgradeFunc)
    {
        StartCoroutine(upgradeFunc);
    }
}
