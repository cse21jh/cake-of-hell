using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberSelectComponent : UIComponent
{
    private TMP_Text countText;
    private Button increaseButton, decreaseButton;
    public int Count { get; set; }

    public NumberSelectComponent(Transform parent, System.Func<bool> atIncrease, System.Func<bool> atDecrease) 
    : base(parent, ResourceLoader.GetPrefab("Prefabs/NumberSelectPrefab"))
    {
        countText = gameObject.transform.GetChild(0).GetComponent<TMP_Text>();
        increaseButton = gameObject.transform.GetChild(1).GetComponent<Button>();
        decreaseButton = gameObject.transform.GetChild(2).GetComponent<Button>();

        SetNumber(1);
        increaseButton.onClick.AddListener(() => {
            if(atIncrease != null && atIncrease())
            {
                SetNumber(Count + 1);
            }
        });
        decreaseButton.onClick.AddListener(() => {
            if(atDecrease != null && atDecrease()) 
            {
                SetNumber(Count - 1);
            }
        });
    }

    public void SetNumber(int number) 
    {
        Count = number;
        countText.text = number.ToString();
    }
}
