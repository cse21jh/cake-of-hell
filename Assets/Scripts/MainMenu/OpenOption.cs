using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenOption : MonoBehaviour
{
    private GameObject option;
    private OptionUI optionUI;

    void Start()
    {
        option = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/Option/OptionMenu"), FindObjectOfType<Canvas>().transform);
        optionUI = option.GetComponent<OptionUI>();
        UiManager.Instance.CloseUI(optionUI);
        gameObject.GetComponent<Button>().onClick.AddListener(()=>UiManager.Instance.OpenUI(optionUI));
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            UiManager.Instance.CloseUI(optionUI);
        }
    }

}
