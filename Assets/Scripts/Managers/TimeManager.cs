using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    public int day = 0;

    public float oneHour = 30f;
    public float timer = 0f;

    public bool stopTimer = true;
    
    public int GuestEnterTimeStart { get; private set; }
    public int GuestEnterTimeEnd { get; private set; }
    public int GuestLeaveTimeStart { get; private set; }
    public int GuestLeaveTimeEnd { get; private set; }

    public bool isPrepareTime;
    public bool endPrepare = false;
    public bool restart = false;

    public DayUI dayUI;
    public HuntTimeUI huntTimeUI;
    public CookTimeUI cookTimeUI;
    public GameObject endDayUI;
    public GameObject endPrepareUI;

    private Canvas canvas;
    private Player player;

    public float reputation = 5.0f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if(!stopTimer)
        { 
            timer += Time.deltaTime;
            if(huntTimeUI != null && cookTimeUI != null)
            {
                huntTimeUI.TimeBarUpdate(timer/oneHour);
                cookTimeUI.TimeBarUpdate(timer / oneHour);
            }
        }
    }

    public void StartDay()
    {
        PlayerManager.Instance.SetHp(PlayerManager.Instance.GetMaxHp());
        PlayerManager.Instance.ResetNumberOfItemInADay();
        PlayerManager.Instance.ResetMoneyInADay();
        GameManager.Instance.LoadScene("Cake Shop", true);
        SaveManager.Instance.JsonSave();
        SetDay(day + 1);
        if (day <= 30)
        {
            GameManager.Instance.killMonsterInADay = false;
            timer = 0f;
            isPrepareTime = true;
            stopTimer = false;
            GameManager.Instance.soldCakeInADay = 0;
            UpdateGuestTimes();
            StartCoroutine(StartDayCoroutine());
        }
    }

    public void OpenShop()
    {
        GameManager.Instance.LoadScene("Cake Shop", true);
        PlayerManager.Instance.SetPlayerImage(0);
        Debug.Log("Time to Open");
        timer = 12.0f * oneHour;
        stopTimer = false;
        isPrepareTime = false;
        StartCoroutine(OpenShopCoroutine());
    }

    public IEnumerator StartDayCoroutine()
    {
        yield return null;
        PrepareOpenShop();

        for(int i=0; i<oneHour*12*10; i++)
        {
            if(restart)
            {
                i = 0;
                restart = false;
            }
            if (endPrepare)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
        }

        if(endPrepare)
        {
            EndPrepare();
        }
        else
        {
            Penalty();
            EndPrepare();
        }
        endPrepare = false;
        yield return null;
    }

    public IEnumerator OpenShopCoroutine()
    {
        for (int i=0; i<oneHour*12*10; i++)
        {
            if (restart)
            {
                i = 0;
                restart = false;
            }
            yield return new WaitForSeconds(0.1f);
        }
        CloseShop();
        yield return null;
    }

    public void PrepareOpenShop()
    {
        GameManager.Instance.CheckUnlock();
        Debug.Log("Time to Prepare");
    }

    public void EndPrepare()
    {
        stopTimer = true;
        canvas = FindObjectOfType<Canvas>();
        if(!GameManager.Instance.killMonsterInADay)
        {
            GameManager.Instance.MoveToEndingScene();
        }
        endPrepareUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/EndPrepareUI"), canvas.transform);
        if (endPrepareUI != null)
        {
            endPrepareUI.GetComponent<EndPrepareUI>().Open();
        }
    }

    public void CloseShop()
    {
        Debug.Log("Time to Close");
        stopTimer = true;
        canvas = FindObjectOfType<Canvas>();
        endDayUI = Instantiate(Resources.Load<GameObject>("Prefabs/UI/EndDayUI"), canvas.transform);
        if (endDayUI != null)
        { 
            endDayUI.GetComponent<EndDayUI>().Open();
        }
    }

    public void Penalty()
    {
        PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() - 50f);
        Debug.Log("isPrepareTimeOver");
    }

    private void UpdateGuestTimes()
    {
        int reputationPenalty = (int)((5 - reputation) * 2);
        if (!GameManager.Instance.unlockMapS)
        {
            GuestEnterTimeStart = 50 + reputationPenalty;
            GuestEnterTimeEnd = 55 + reputationPenalty;
            GuestLeaveTimeStart = 40 + GameManager.Instance.addGuestLeaveTime;
            GuestLeaveTimeEnd = 45 + GameManager.Instance.addGuestLeaveTime;
        }
        else
        {
            GuestEnterTimeStart = 45 + reputationPenalty;
            GuestEnterTimeEnd = 50 + reputationPenalty;
            GuestLeaveTimeStart = 35 + GameManager.Instance.addGuestLeaveTime;
            GuestLeaveTimeEnd = 40 + GameManager.Instance.addGuestLeaveTime;
        }
    }

    public int GetDay()
    {
        return day;
    }

    public void SetDay(int _day)
    {
        // FixMe
        if (_day == 31)
        {
            day = _day;
            GameManager.Instance.killMonsterInADay = true;
            GameManager.Instance.MoveToEndingScene();
            return;
        }
        day = _day;
        if (dayUI != null)
        {
            dayUI.DayTextUpdate(day);
        }
    }

    public float GetReputation()
    {
        return reputation;
    }

    public void SetReputation(float value)
    {
        if (value > 5.0)
        {
            reputation = 5.0f;
            return;
        }
        reputation = value;
        if (value <= 0)
        { 
            reputation = 0;
            //GameManager.Instance.MoveToEndingScene();
        }
    }

    public float GetTime()
    {
        return (timer / oneHour);
    }

}
