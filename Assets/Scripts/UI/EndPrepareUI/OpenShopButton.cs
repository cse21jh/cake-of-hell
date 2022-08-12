using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopButton : MonoBehaviour
{
    private EndPrepareUI endPrepareUI;

    private void Awake()
    {
        endPrepareUI = transform.parent.gameObject.GetComponent<EndPrepareUI>();
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        TimeManager.Instance.OpenShop();
        endPrepareUI.Close();
        //GameManager.Instance.LoadScene("JHSampleShop", true);
    }
}
