using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeList : MonoBehaviour
{
    [SerializeField]
    private string category;

    [SerializeField]
    private GameObject informationPrefab;

    void Awake()
    {
        switch(category)
        {
            case "Raw":
                RawList();
                break;
            case "Base":
                BaseList();
                break;
            case "Topping":
                ToppingList();
                break;
            case "Icing":
                IcingList();
                break;
        }
    }

    
    void RawList()
    {
        int j = 1;
        foreach (var pair in ItemManager.Instance.RawItemList)
        { 
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfRaw[j].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
            j++;
        }
    }

    void BaseList()
    {
        int j = 1;
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key)/10000) == 1)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfBase[j].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                j++;
            }
        }
    }

    void ToppingList()
    {
        int j = 1;
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 10000) == 2)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfTopping[j].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                j++;
            }
        }
    }

    void IcingList()
    {
        int j = 1;
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 10000) == 4)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfIcing[j].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                j++;
            }
        }
    }
}
