using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PaginationComponent : UIComponent
{
    private GameObject[] buttons;
    private PageComponent[] pages;
    private int nowPage = 0;

    public PaginationComponent(Transform parent, PageComponent[] _pages, System.Action additionalFunction = null) 
    : base(parent, ResourceLoader.GetPrefab("Prefabs/PaginationPrefab"))
    {
        int pageCount = _pages.Length;
        buttons = new GameObject[pageCount];
        pages = new PageComponent[pageCount];
        GameObject buttonPrefab = ResourceLoader.GetPrefab("Prefabs/PaginationButtonPrefab");
        for(int i=0; i<pageCount; i++) 
        {
            int j = i;
            pages[j] = _pages[j];
            buttons[j] = Object.Instantiate(buttonPrefab, gameObject.transform);
            buttons[j].transform.GetChild(0).GetComponent<TMP_Text>().text = pages[j].PageName;
            buttons[j].GetComponent<Button>().onClick.AddListener(() => 
            {
                ShowPage(j);
                if(additionalFunction != null) 
                {
                    additionalFunction();
                }
            });
            if(i > 0) pages[i].SetActive(false);
        }
    }

    public void ShowPage(int index)
    {
        if(index != nowPage)
        {
            pages[nowPage].SetActive(false);
            nowPage = index;
            pages[index].SetActive(true);
        }
    }
}
