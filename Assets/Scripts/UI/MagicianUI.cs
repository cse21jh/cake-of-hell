using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MagicianUI : BaseUI, ISingleOpenUI
{ 
    private int inputCount;
    private ProcessedItem outputItem;
    private ItemSlotComponent input, outputDefault;
    private ItemSlotComponent[] outputOthers, processItems;
    private IEnumerator[] processTimes;
    private Recipe recipeDefault;
    private Recipe[] recipeOthers;
    private PageComponent inventoryPage;
    private NumberSelectComponent numberSelect;
    private GameObject processButton;
    private GameObject[] progressCircles;
    private TMP_Text inputName, outputName, outputDesc, totalCost, totalTime;
    private Dictionary<int, ItemSlotComponent> itemSlots;

    public int UnlockedSlots { get; set; }

    void Start()
    {
        Debug.Log("Started Magician UI");
        Util.AddItem(4001, 10);
        Util.AddItem(4009, 10);
        Util.AddItem(4012, 10);
        Util.EarnMoney(1000);
        UnlockedSlots = 3;
        outputOthers = new ItemSlotComponent[3];
        recipeOthers = new Recipe[3];
        processItems = new ItemSlotComponent[8];
        processTimes = new IEnumerator[8];
        progressCircles = new GameObject[8];
        itemSlots = new Dictionary<int, ItemSlotComponent>();
        
        processButton = GameObject.Find("ProcessButtonMagician");
        inputName = GameObject.Find("InputItemText").GetComponent<TMP_Text>();
        outputName = GameObject.Find("OutputItemText").GetComponent<TMP_Text>();
        outputDesc = GameObject.Find("MagicianDesc").GetComponent<TMP_Text>();
        totalCost = GameObject.Find("MagicianMoneyText").GetComponent<TMP_Text>();
        totalTime = GameObject.Find("MagicianTimeText").GetComponent<TMP_Text>();
        processButton.GetComponent<Button>().onClick.AddListener(() => Process());
        MakeUI();
    }

    void Update()
    {
        
    }

    public void MakeUI() 
    {
        GameObject circ = Resources.Load<GameObject>("Prefabs/ProgressCirclePrefab");
        inventoryPage = new PageComponent(gameObject.transform, "Raw Item", 4, 350);
        inventoryPage.SetPosition(-170, 0);

        numberSelect = new NumberSelectComponent(gameObject.transform, IncreaseCount, DecreaseCount);
        numberSelect.SetPosition(115, -130);

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
                itemSlots[pair.Key].SetOnClick(() => 
                {
                    input.LoadItem(pair.Key, -1);
                    inputName.text = Util.GetItem(pair.Key).Name;
                    inputCount = 1;
                    LoadOutput(-1);
                });
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
            int j = i;
            processItems[j] = new ItemSlotComponent(gameObject.transform, 0, -1, true);
            processItems[j].SetPosition(-375, 175 - 50 * j);
            processItems[j].SetOnClick(() => 
            {
                if(Util.GetItem(processItems[j].ItemCode) is ProcessedItem)
                {
                    Util.AddItem(processItems[j].ItemCode * processItems[j].ItemCount);
                    processItems[j].Clear();
                    ProcessManager.Instance.MagicianProcesses[j] = null;
                }
            });
            
            progressCircles[i] = Object.Instantiate(circ, processItems[i].gameObject.transform);
        }

        for(int i=0; i<UnlockedSlots; i++) 
        {
            MagicianProcess proc = ProcessManager.Instance.MagicianProcesses[i];
            if(proc == null) continue;

            if(proc.IsEnded) 
            {
                processItems[i].LoadItem(proc.ProcessRecipe.Output, proc.Count);
            }
            else 
            {
                processItems[i].LoadItem(proc.ProcessRecipe.Input, proc.Count);
                proc.Slot = processItems[i];
                proc.Circle = progressCircles[i].GetComponent<ProgressCircle>();
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
        HoverItemName.Instance.gameObject.SetActive(false);
        Debug.Log("Magician UI Closed!");
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

    private bool IncreaseCount()
    {
        if(inputCount < Util.CountItem(input.ItemCode))
        {
            inputCount++;
            totalCost.text = (recipeDefault.Price * inputCount).ToString();
            totalTime.text = (recipeDefault.Duration * inputCount).ToString();
            return true;
        }
        return false;
    }

    private bool DecreaseCount()
    {
        if(inputCount > 1)
        {
            inputCount--;
            totalCost.text = (recipeDefault.Price * inputCount).ToString();
            totalTime.text = (recipeDefault.Duration * inputCount).ToString();
            return true;
        }
        return false;
    }

    private void Process() 
    {
        int idx = GetAvailableIndex();
        if(input.HasItem() && PlayerManager.Instance.GetMoney() >= System.Convert.ToSingle(totalCost.text) && idx != -1)
        {
            int inputCode = input.ItemCode;
            int outputCode = outputDefault.ItemCode;
            int outputCount = inputCount;
            float neededCost = System.Convert.ToSingle(totalCost.text);
            float neededTime = System.Convert.ToSingle(totalTime.text);
            
            itemSlots[inputCode].UseItem(inputCount);
            processItems[idx].LoadItem(inputCode, inputCount);
            GameManager.Instance.processCount += inputCount;
            if(ItemManager.Instance.GetProcessedItem(outputCode).Level == ItemLevel.SS)
            {
                GameManager.Instance.processSSCount ++;

            }
            ProcessManager.Instance.AddMagicianProcess
            (
                idx,
                recipeDefault, 
                inputCount, 
                processItems[idx],
                progressCircles[idx].GetComponent<ProgressCircle>()
            );
            Util.SpendMoney(neededCost);
            
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
            inputCount = 1;
            numberSelect.SetNumber(1);

            Debug.Log("Magician Trade Success!");
        }
        else
        {
            Debug.Log("Magician Trade Fail.");
        }
    }
}
