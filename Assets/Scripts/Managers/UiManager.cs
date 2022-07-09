using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    private GameObject itemList;
    private Canvas canvas;

    private Player player;

    public bool openItemList = false;
    public bool alreadyOpenItemList = false; // ��ɲ� ���� ���� ������ ������ i������ ��â �� ��������

    public bool openByMaking = false;
    public bool openByMagician = false;
    public bool openByHunter = false;

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
            else if (openItemList)
            {
                CloseItemList();
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
}
