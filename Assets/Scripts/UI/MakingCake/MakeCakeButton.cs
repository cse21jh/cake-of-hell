using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCakeButton : MonoBehaviour
{
    public void OnClickExit()
    {
        if (GameManager.Instance.inputBase != BaseIndex.Null && GameManager.Instance.inputIcing != IcingIndex.Null && GameManager.Instance.inputTopping != ToppingIndex.Null)
        {
            GameManager.Instance.MakeCake();
        }
    }
}
