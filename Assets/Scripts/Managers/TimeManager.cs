using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private int day = -1;

    private float oneHour = 1f;
    private float timer = 0f;

    private bool stopTimer = true;
    private IEnumerator enumerator;

    private bool isOpenTime = false;
    private bool isPrepareTime = true;

    public DayUI dayUI;
    public TimeUI timeUI;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(!stopTimer)
            timer += Time.deltaTime;
    }
    public void StartDay()
    {
        day += 1;
        Debug.Log(day);
        enumerator = StartDayCoroutine();
        timer = 0f;
        stopTimer = false;
        StartCoroutine(StartDayCoroutine());
    }

    public IEnumerator StartDayCoroutine()
    {
        PrepareOpenShop();
        yield return new WaitForSeconds(oneHour * 12f);
        if (!PlayerManager.Instance.GetPlayerInShop() && isPrepareTime)
        {
            Penalty();
            OpenShop();
        }
        else if (PlayerManager.Instance.GetPlayerInShop()  && isPrepareTime)
        {
            OpenShop();
        }

        yield return new WaitForSeconds(oneHour * 12f);
        if (isOpenTime)
        {
            CloseShop();
        }
        yield return null;
        
    }

    public void PrepareOpenShop()
    {
        Debug.Log("Time to Prepare");
        isPrepareTime = true;
        isOpenTime = false;
    }

    public void OpenShop()
    {
        Debug.Log("Time to Open");
        isPrepareTime = false;
        isOpenTime = true;
    }

    public void CloseShop()
    {
        Debug.Log("Time to Close");
        stopTimer = true;
        StopCoroutine(enumerator);
        // Insert Ending
        // Open UI. When Close UI, StartDay.
    }

    public void Penalty()
    {
        GameManager.Instance.LoadScene("JHSampleShop");
        PlayerManager.Instance.SetMoney(PlayerManager.Instance.GetMoney() - 50f);
        Debug.Log("isPrepareTimeOver");
    }



    public int GetDay()
    {
        return day;
    }

    public void SetDay(int _day)
    {
        day = _day;
    }

    public float GetTime()
    {
        return (timer/oneHour);
    }
}
