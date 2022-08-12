using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMoveToShopYes : MonoBehaviour
{
    public CheckMoveToShopUI checkMoveToShopUI;

    private void Awake()
    {
        checkMoveToShopUI = transform.parent.gameObject.GetComponent<CheckMoveToShopUI>();
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        TimeManager.Instance.endPrepare = true;
        checkMoveToShopUI.Close();
    }
}
