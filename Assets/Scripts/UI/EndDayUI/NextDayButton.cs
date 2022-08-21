using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextDayButton : MonoBehaviour
{
    private EndDayUI endDayUI;

    private void Awake()
    {
        endDayUI = transform.parent.gameObject.GetComponent<EndDayUI>();
    }

    public void OnClickExit()
    {
        SoundManager.Instance.PlayEffect("Click");
        endDayUI.Close();
        TimeManager.Instance.StartDay();
    }
}
