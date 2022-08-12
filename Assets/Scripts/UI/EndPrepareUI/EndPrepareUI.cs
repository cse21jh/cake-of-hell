using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPrepareUI : BaseUI
{
    void Start()
    {

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
