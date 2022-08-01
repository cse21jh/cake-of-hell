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

        if(TimeManager.Instance.GetDay() == 30)
        {
            
            GameManager.Instance.CheckEnding();
            endDayUI.Close();
            return;
        }

        TimeManager.Instance.StartDay();
        endDayUI.Close();
    }
}
