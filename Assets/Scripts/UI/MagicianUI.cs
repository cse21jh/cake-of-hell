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
    private Recipe recipeDefault;
    private Recipe[] recipeOthers;
    private PageComponent inventoryPage;
    private GameObject processButton;
    private TMP_Text inputName, outputName, outputDesc, totalCost, totalTime;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    public int UnlockedSlots { get; set; }

    void Start()
    {
        Util.AddItem(4001, 10);
        Util.AddItem(4009, 10);
        Util.AddItem(4012, 10);
        Util.EarnMoney(1000);
        UnlockedSlots = 3;
        outputOthers = new ItemSlotComponent[3];
        recipeOthers = new Recipe[3];
        processItems = new ItemSlotComponent[8];
        processTimes = new IEnumerator[8];
        itemSlots = new Dictionary<int, ItemSlotComponent>();
        
        processButton = GameObject.Find("ProcessButtonMagician");
        inputName = GameObject.Find("InputItemText").GetComponent<TMP_Text>();
        outputName = GameObject.Find("OutputItemText").GetComponent<TMP_Text>();
        outputDesc = GameObject.Find("MagicianDesc").GetComponent<TMP_Text>();
        totalCost = GameObject.Find("MagicianMoneyText").GetComponent<TMP_Text>();
        totalTime = GameObject.Find("MagicianTimeText").GetComponent<TMP_Text>();
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
        outputDefault = new ItemSlotComponent(gameObject.transform, 0, -1, true);
        input.SetPosition(120, 80);
        outputDefault.SetPosition(280, 80);
        outputDefault.SetOnClick(() => ToggleOutputList());

        foreach(var pair in Util.GetItemNumbers(ItemType.Raw)) 
        {
            if(pair.Value > 0)
            {
                itemSlots.Add(pair.Key, new ItemSlotComponent(inventoryPage.Container, pair.Key, pair.Value, true));
                itemSlots[pair.Key].SetOnClick(() => SetRecipe(pair.Key));
            }
        }

        for(int i=0; i<3; i++) 
        {
            int j = i;
            outputOthers[j] = new ItemSlotComponent(gameObject.transform, 0, -1, true);
            outputOthers[j].SetPosition(280, 80 - 50 * (i + 1));
            outputOthers[j].SetOnClick(() => 
            {
                LoadOutput(j);
                ToggleOutputList();
            });
            outputOthers[j].SetActive(false);
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

    private void SetRecipe(int codeToSet) 
    {
        input.LoadItem(codeToSet, -1);
        inputName.text = Util.GetItem(codeToSet).Name;
        LoadOutput(-1);
    }

    private void LoadOutput(int idx)
    {
        var outputRecipes = Util.GetRecipesFromInput(input.ItemCode);
        recipeDefault = idx == -1 ? outputRecipes[0] : recipeOthers[idx];
        outputDefault.LoadItem(recipeDefault.Output, -1);
        outputItem = Util.GetItem(recipeDefault.Output) as ProcessedItem;
        outputName.text = outputItem.Name;
        outputDesc.text = outputItem.FlavorText;
        totalCost.text = recipeDefault.Price.ToString();
        totalTime.text = recipeDefault.Duration.ToString();
    }

    private void ToggleOutputList() 
    {
        if(!input.HasItem()) return;

        int idx = 0;
        foreach(var outputRecipe in Util.GetRecipesFromInput(input.ItemCode))
        {
            if(outputRecipe.Output != outputDefault.ItemCode)
            {
                if(!outputOthers[idx].IsActive()) 
                {
                    recipeOthers[idx] = outputRecipe;
                    outputOthers[idx].LoadItem(outputRecipe.Output);
                    outputOthers[idx].SetActive(true);
                }
                else 
                {
                    outputOthers[idx].SetActive(false);
                }
                idx++;
            }
        }
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
            processItems[idx].LoadItem(outputCode);
            Util.SpendMoney(System.Convert.ToSingle(totalCost.text));
            if(Util.CountItem(inputCode) == 0) 
            {
                input.Clear();
                outputDefault.Clear();
                inputName.text = "";
                outputName.text = "";
                outputDesc.text = "";
                totalCost.text = "0";
                totalTime.text = "0";
                itemSlots.Remove(inputCode);
            }

            yield return new WaitForSeconds(System.Convert.ToSingle(totalTime.text));

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
