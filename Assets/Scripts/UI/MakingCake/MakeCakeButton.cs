using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCakeButton : MonoBehaviour
{
    public void OnClickExit()
    {
        if (GameManager.Instance.inputBase != null && GameManager.Instance.inputIcing != null && GameManager.Instance.inputTopping != null)
        {
            GameManager.Instance.MakeCake();
        }
    }
}
