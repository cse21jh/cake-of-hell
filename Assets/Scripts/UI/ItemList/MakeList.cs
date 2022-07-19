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
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfRaw[pair.Key].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
            
        }
    }

    void BaseList()
    {
        
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key)/10000) == 1)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfBase[pair.Key].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                
            }
        }
    }

    void ToppingList()
    {
        
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 10000) == 2)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfTopping[pair.Key].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                
            }
        }
    }

    void IcingList()
    {
        
        foreach (var pair in ItemManager.Instance.ProcessedItemList)
        {
            if (((pair.Key) / 10000) == 4)
            {
                GameObject information = Instantiate(informationPrefab, this.transform);
                string n = "X" + SaveManager.Instance.NumberOfIcing[pair.Key].ToString();
                information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = pair.Value.SpriteImage;
                information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = pair.Value.FlavorText;
                information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
                information.transform.Find("Rank").gameObject.GetComponent<Text>().text = pair.Value.Level.ToString();
                information.transform.Find("Name").gameObject.GetComponent<Text>().text = pair.Value.Name;
                
            }
        }
    }
}
