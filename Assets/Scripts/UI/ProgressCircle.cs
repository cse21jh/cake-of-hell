using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressCircle : MonoBehaviour
{
    private Image circleImage;

    void Start()
    {
        circleImage = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        
    }

    public void SetProgress(float percent)
    {
        circleImage.fillAmount = percent > 100 ? 1 : percent / 100.0f;
    }
}
