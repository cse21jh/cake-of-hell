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
        
        foreach (var pair in ItemManager.Instance.RawItemList)
        {
            if(PlayerManager.Instance.GetNumberOfItem(pair.Key)>0)
            { 
                if (pair.Key == 0)
                    continue;
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + PlayerManager.Instance.GetNumberOfItem(pair.Key).ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
            }
        }
    }

    void BaseList()
    {
        
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key)/1000) == 1)
            {
                if (PlayerManager.Instance.GetNumberOfItem(pair.Key) > 0)
                {
                    GameObject information = Instantiate(informationPrefab, this.transform);
                    string n = "X" + PlayerManager.Instance.GetNumberOfItem(pair.Key).ToString();
                    information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                    information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.Keyword + "\n\n" + pair.Value.FlavorText;
                    information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                    information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                    information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                }
            }
        }
    }

    void IcingList()
    {

        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 1000) == 2)
            {
                if (PlayerManager.Instance.GetNumberOfItem(pair.Key) > 0)
                {
                    GameObject information = Instantiate(informationPrefab, this.transform);
                    string n = "X" + PlayerManager.Instance.GetNumberOfItem(pair.Key).ToString();
                    information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                    information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.Keyword + "\n\n" + pair.Value.FlavorText;
                    information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                    information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                    information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                }
            }
        }
    }

    void ToppingList()
    {
        
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 1000) == 3)
            {
                if (PlayerManager.Instance.GetNumberOfItem(pair.Key) > 0)
                {
                    GameObject information = Instantiate(informationPrefab, this.transform);
                    string n = "X" + PlayerManager.Instance.GetNumberOfItem(pair.Key).ToString();
                    information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                    information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.Keyword + "\n\n" + pair.Value.FlavorText;
                    information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                    information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                    information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                }
            }
        }
    }

}
