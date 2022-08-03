using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    private float nowTime;
    private Image circleImage;
    public float Time { get; set; }

    void Start()
    {
        circleImage = gameObject.GetComponent<Image>();
        Debug.Log(circleImage);   
    }

    void Update()
    {
        
    }

    public void StartProgress(float time)
    {
        Time = time;
        StartCoroutine(Turn(Time / 100));
    }

    private IEnumerator Turn(float segment)
    {
        while(nowTime <= Time)
        {
            nowTime += segment;
            circleImage.fillAmount = nowTime / Time;
            yield return new WaitForSeconds(segment);
        }

        circleImage.fillAmount = 0;
        nowTime = 0;
    }
}
