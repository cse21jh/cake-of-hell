using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeList : MonoBehaviour
{
    [SerializeField]
    private string category;
    // Start is called before the first frame update

    [SerializeField]
    private GameObject informationPrefab;

    void Awake()
    {
        switch(category)
        {
            case "RBase":
                RBaseList();
                break;
            case "RTopping":
                RToppingList();
                break;
            case "RIcing":
                RIcingList();
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

    void RBaseList()
    {
        for(int i=0;i<(int)RBaseIndex.Number;i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfRBase[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.RBaseInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.RBaseInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.RBaseInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.RBaseInformation[i].itemName;
        }
    }

    void RToppingList()
    {
        for (int i = 0; i < (int)RToppingIndex.Number; i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfRTopping[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.RToppingInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.RToppingInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.RToppingInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.RToppingInformation[i].itemName;
        }
    }

    void RIcingList()
    {
        for (int i = 0; i < (int)RIcingIndex.Number; i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfRIcing[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.RIcingInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.RIcingInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.RIcingInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.RIcingInformation[i].itemName;
        }
    }

    void BaseList()
    {
        for (int i = 0; i < (int)BaseIndex.Number; i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfBase[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.BaseInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.BaseInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.BaseInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.BaseInformation[i].itemName;
        }
    }

    void ToppingList()
    {
        for (int i = 0; i < (int)ToppingIndex.Number; i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfTopping[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.ToppingInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.ToppingInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.ToppingInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.ToppingInformation[i].itemName;
        }
    }

    void IcingList()
    {
        for (int i = 0; i < (int)IcingIndex.Number; i++)
        {
            GameObject information = Instantiate(informationPrefab, this.transform);
            string n = "X" + SaveManager.Instance.NumberOfIcing[i].ToString();
            information.transform.Find("Image").gameObject.GetComponent<Image>().sprite = ItemManager.Instance.IcingInformation[i].sprite;
            information.transform.Find("Explanation").gameObject.GetComponent<Text>().text = ItemManager.Instance.IcingInformation[i].explanation;
            information.transform.Find("Number").gameObject.GetComponent<Text>().text = n;
            information.transform.Find("Rank").gameObject.GetComponent<Text>().text = ItemManager.Instance.IcingInformation[i].rank;
            information.transform.Find("Name").gameObject.GetComponent<Text>().text = ItemManager.Instance.IcingInformation[i].itemName;
        }
    }
}
