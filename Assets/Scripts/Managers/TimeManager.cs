using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private int day = 0;

    private float oneHour = 30f;
    private float timer = 0f;

    private bool stopTimer = true;

    public int GuestEnterTimeStart { get; private set; }
    public int GuestEnterTimeEnd { get; private set; }
    public int GuestLeaveTimeStart { get; private set; }
    public int GuestLeaveTimeEnd { get; private set; }

    public bool isPrepareTime;
    public bool isOpenTime;
    public bool endPrepare = false;

    public DayUI dayUI;
    public HuntTimeUI huntTimeUI;
    public CookTimeUI cookTimeUI;
    public GameObject endDayUI;
    public GameObject endPrepareUI;

    private Canvas canvas;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!stopTimer)
        { 
            timer += Time.deltaTime;
            if(huntTimeUI != null && cookTimeUI != null)
            {
                huntTimeUI.TimeBarUpdate(timer);
                cookTimeUI.TimeBarUpdate(timer);
            }
        }
    }

    public void StartDay()
    {
        PlayerManager.Instance.SetHp(PlayerManager.Instance.GetMaxHp());
        SetDay(day + 1);
        Debug.Log(day);
        timer = 0f;
        isPrepareTime = true;
        isOpenTime = false;
        stopTimer = false;
        UpdateGuestTimes();
        StartCoroutine(StartDayCoroutine());
    }

    public void OpenShop()
    {
        Debug.Log("Time to Open");
        timer = 12.0f * oneHour;
        stopTimer = false;
        isPrepareTime = false;
        isOpenTime = true;
        StartCoroutine(OpenShopCoroutine());
    }

    public IEnumerator StartDayCoroutine()
    {
        PrepareOpenShop();

        for(int i=0; i<oneHour*12*10; i++)
        {
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
            yield return new WaitForSeconds(0.1f);
        }
        CloseShop();
        yield return null;
    }

    public void PrepareOpenShop()
    {
        Debug.Log("Time to Prepare");
    }

    public void EndPrepare()
    {
        stopTimer = true;
        canvas = FindObjectOfType<Canvas>();
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
        //to be fixed
        GuestEnterTimeStart = 20;
        GuestEnterTimeEnd = 25;
        GuestLeaveTimeStart = 15;
        GuestLeaveTimeEnd = 25;
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
            Ending();
        }
        day = _day;
        GameManager.Instance.CheckUnlock();
        if (dayUI != null)
        {
            dayUI.DayTextUpdate(day);
        }
    }

    public float GetTime()
    {
        return (timer / oneHour);
    }

    public void Ending()
    {
        Debug.Log("Ending");
    }
}
