using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMoveToShopUI : BaseUI
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        GameManager.Instance.canMove = false;
    }

    public override void Close()
    {
        gameObject.SetActive(false);
        GameManager.Instance.canMove = true;
    }
}
