using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    private GameObject itemList;
    private Canvas canvas;

    private Player player;

    public bool openItemList = false;
    public bool alreadyOpenItemList = false;
    public bool openMenu = false;

    public bool openByMaking = false;
    public bool openByMagician = false;
    public bool openByHunter = false;

    private GameObject menu;
    private GameObject option;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //canvas = FindObjectOfType<Canvas>();
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !alreadyOpenItemList)
        {
            if (!openItemList)
            {
                OpenItemList();
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(openItemList)
            {
                CloseItemList();
            }
            if (!openMenu)
            {
                Time.timeScale = 0f;
                if (option == null)
                {
                    option = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/Option/OptionMenu"), FindObjectOfType<Canvas>().transform);
                    menu = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/Option/Menu"), FindObjectOfType<Canvas>().transform);
                }
                OpenUI(menu.GetComponent<MenuUI>());
                menu.GetComponent<MenuUI>().OptionMenu = option.GetComponent<OptionUI>();
                CloseUI(option.GetComponent<OptionUI>());
                openMenu = true;
            }
            else
            {
                Time.timeScale = 1f;
                CloseUI(menu.GetComponent<MenuUI>());
                CloseUI(option.GetComponent<OptionUI>());
                openMenu = false;
            }    
        }
    }

    public void OpenItemList()
    {
        openItemList = true;
        itemList = Instantiate(player.itemListPrefab, FindObjectOfType<Canvas>().transform);
    }

    public void CloseItemList()
    {
        openItemList = false;
        Destroy(itemList);
    }

    public void OpenUI(BaseUI ui) 
    {
        if(ui is ISingleOpenUI) {
            if(!ui.IsActive()) 
            {
                GameManager.Instance.canMove = false;
                ui.Open();
            }
        }
        else
        {
            ui.Open();
        }
    }

    public void CloseUI(BaseUI ui) 
    {
        if(ui is ISingleOpenUI) {
            if(ui.IsActive()) 
            {
                GameManager.Instance.canMove = true;
                ui.Close();
            }
        }
        else
        {
            ui.Close();
        }
    }
}
