using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMoveToShopNo : MonoBehaviour
{
    private CheckMoveToShopUI checkMoveToShopUI;

    private void Awake()
    {
        checkMoveToShopUI = transform.parent.gameObject.GetComponent<CheckMoveToShopUI>();
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        checkMoveToShopUI.Close();
    }
}
