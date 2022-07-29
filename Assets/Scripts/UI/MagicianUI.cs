using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicianUI : BaseUI, ISingleOpenUI
{ 
    private ProcessedItem outputItem;
    private ItemSlotComponent input, outputDefault;
    private ItemSlotComponent[] outputOthers, processItems;
    private IEnumerator[] processTimes;
    private PageComponent inventoryPage;
    private GameObject processButton;
    private TMP_Text inputName, outputName, outputDesc, money;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    public int UnlockedSlots { get; set; }

    void Start()
    {
        Util.AddItem(4001, 10);
        Util.AddItem(4009, 10);
        Util.AddItem(4012, 10);
        Util.EarnMoney(1000);
        UnlockedSlots = 3;
        processItems = new ItemSlotComponent[8];
        processTimes = new IEnumerator[8];
        itemSlots = new Dictionary<int, ItemSlotComponent>();
        
        processButton = GameObject.Find("ProcessButtonMagician");
        inputName = GameObject.Find("InputItemText").GetComponent<TMP_Text>();
        outputName = GameObject.Find("OutputItemText").GetComponent<TMP_Text>();
        outputDesc = GameObject.Find("MagicianDesc").GetComponent<TMP_Text>();
        money = GameObject.Find("MagicianMoney").GetComponent<TMP_Text>();
        money.text = PlayerManager.Instance.GetMoney().ToString();
        processButton.GetComponent<Button>().onClick.AddListener(() => StartCoroutine(Process()));
        MakeUI();
    }

    void Update()
    {
        
    }

    public void MakeUI() 
    {
        inventoryPage = new PageComponent(gameObject.transform, "Raw Item", 4, 350);
        inventoryPage.SetPosition(-170, 0);

        input = new ItemSlotComponent(gameObject.transform, 0, -1);
        outputDefault = new ItemSlotComponent(gameObject.transform, 0, -1);
        input.SetPosition(120, 80);
        outputDefault.SetPosition(280, 80);

        foreach(var pair in Util.GetItemNumbers(ItemType.Raw)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(inventoryPage.Container, pair.Key, pair.Value, true));
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

        for(int i=0; i<8; i++) 
        {
            processItems[i] = new ItemSlotComponent(gameObject.transform, 0, -1);
            processItems[i].SetPosition(-375, 175 - 50 * i);
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

    private int GetAvailableIndex()
    {
        for(int i=0; i<UnlockedSlots; i++) 
        {
            if(!processItems[i].HasItem())
            {
                return i;
            }
        }
        return -1;
    }

    private IEnumerator Process() 
    {
        int idx = GetAvailableIndex();
        if(input.HasItem() && PlayerManager.Instance.GetMoney() >= outputItem.Price && idx != -1)
        {
            int inputCode = input.ItemCode;
            int outputCode = outputDefault.ItemCode;
            
            itemSlots[inputCode].UseItem();
            processItems[idx].LoadItem(inputCode);
            Util.SpendMoney(outputItem.Price);
            money.text = PlayerManager.Instance.GetMoney().ToString();
            if(Util.CountItem(inputCode) == 0) 
            {
                input.Clear();
                outputDefault.Clear();
                inputName.text = "";
                outputName.text = "";
                outputDesc.text = "";
                itemSlots.Remove(inputCode);
            }

            yield return new WaitForSeconds(Util.GetRecipesFromInput(inputCode)[0].Duration);

            Util.AddItem(outputCode);
            processItems[idx].Clear();

            Debug.Log("Magician Trade Success!");
        }
        else
        {
            Debug.Log("Magician Trade Fail.");
        }
    }
}
