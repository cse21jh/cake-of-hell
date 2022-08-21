using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToShop : MonoBehaviour
{
    private GameObject checkMoveToShopUI;
    private Canvas canvas;

    void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
        checkMoveToShopUI = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/CheckMoveToShopUI"), canvas.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.canUsePortal)
        {
            if (other.GetComponent<Player>() != null)
            {
                checkMoveToShopUI.GetComponent<CheckMoveToShopUI>().Open();
            }
        }
        GameManager.Instance.PortalDelay();
    }

}
