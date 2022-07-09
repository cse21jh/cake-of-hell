using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakingCake : MonoBehaviour
{
    private bool canMake = false;

    [SerializeField]
    private GameObject makingPanelPrefab;

    private GameObject makingPanel;

    void Update()
    {
        if (canMake && Input.GetKeyDown(KeyCode.G))
        {
            if (!UiManager.Instance.alreadyOpenItemList)
            {
                OpenMakingPanels();
            }
            else
            {
                CloseMakingPanels();
            }
        }
        else if(canMake && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseMakingPanels();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            canMake = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canMake = false;
        }
    }

    void OpenMakingPanels()
    {
        if (!UiManager.Instance.openItemList)
        {
            UiManager.Instance.OpenItemList();
        }
        GameManager.Instance.canMove = false;
        UiManager.Instance.alreadyOpenItemList = true;
        UiManager.Instance.openByMaking = true;
        makingPanel = Instantiate(makingPanelPrefab, FindObjectOfType<Canvas>().transform);
    }

    void CloseMakingPanels()
    {
        UiManager.Instance.CloseItemList();
        UiManager.Instance.alreadyOpenItemList = false;
        UiManager.Instance.openByMaking = false;
        GameManager.Instance.canMove = true;
        Destroy(makingPanel);

        GameManager.Instance.inputBase = BaseIndex.Null;
        GameManager.Instance.inputIcing = IcingIndex.Null;
        GameManager.Instance.inputTopping = ToppingIndex.Null;
    }
}
