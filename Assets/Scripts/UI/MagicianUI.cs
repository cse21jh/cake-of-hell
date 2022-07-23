using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicianUI : BaseUI, ISingleOpenUI
{ 
    private ProcessedItem outputItem;
    private ItemSlotComponent input, outputDefault;
    private ItemSlotComponent[] outputOthers;
    private PageComponent page;
    private GameObject processButton;
    private TMP_Text inputName, outputName, outputDesc, money;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    void Start()
    {
        Util.AddItem(81001, 10);
        Util.AddItem(84010, 10);
        Util.AddItem(82013, 10);
        Util.EarnMoney(100);
        itemSlots = new Dictionary<int, ItemSlotComponent>();
        
        processButton = GameObject.Find("ProcessButtonMagician");
        inputName = GameObject.Find("InputItemText").GetComponent<TMP_Text>();
        outputName = GameObject.Find("OutputItemText").GetComponent<TMP_Text>();
        outputDesc = GameObject.Find("MagicianDesc").GetComponent<TMP_Text>();
        money = GameObject.Find("MagicianMoney").GetComponent<TMP_Text>();
        money.text = SaveManager.Instance.Money.ToString();
        processButton.GetComponent<Button>().onClick.AddListener(Process);
        MakeUI();
    }

    void Update()
    {
        
    }

    public void MakeUI() 
    {
        page = new PageComponent(gameObject.transform, "Raw Item", 4, 350);
        page.SetPosition(-230, 0);

        input = new ItemSlotComponent(gameObject.transform, 0, -1);
        outputDefault = new ItemSlotComponent(gameObject.transform, 0, -1);
        input.SetPosition(120, 80);
        outputDefault.SetPosition(280, 80);

        foreach(var pair in Util.GetItemNumbers(ItemType.Raw)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(page.Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    input.LoadItem(pair.Key, -1);
                    outputDefault.LoadItem(Util.GetRecipesFromInput(pair.Key)[0].Output, -1);
                    inputName.text = Util.GetItem(pair.Key).Name;
                    outputItem = Util.GetItem(Util.GetRecipesFromInput(pair.Key)[0].Output) as ProcessedItem;
                    outputName.text = outputItem.Name;
                    outputDesc.text = outputItem.FlavorText;
                });
            }
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        Debug.Log("Magician UI Opened!");
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
    }

    private void Process() 
    {
        if(input.HasItem() && SaveManager.Instance.Money >= outputItem.Price)
        {
            itemSlots[input.ItemCode].UseItem();
            Util.AddItem(outputDefault.ItemCode);
            Util.SpendMoney(outputItem.Price);
            money.text = SaveManager.Instance.Money.ToString();

            if(Util.CountItem(input.ItemCode) == 0) 
            {
                input.Clear();
                outputDefault.Clear();
                inputName.text = "";
                outputName.text = "";
                outputDesc.text = "";
                itemSlots.Remove(input.ItemCode);
            }

            Debug.Log("Magician Trade Success!");
        }
        else
        {
            Debug.Log("Magician Trade Fail.");
        }
    }
}
