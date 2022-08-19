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
        checkMoveToShopUI = Instantiate(ResourceLoader.Instance.GetPrefab("CheckMoveToShopUI"), canvas.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            checkMoveToShopUI.GetComponent<CheckMoveToShopUI>().Open();
        }
    }

}
