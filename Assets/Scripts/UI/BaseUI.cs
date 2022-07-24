using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }
    
    public abstract void Open();
    public abstract void Close();
}
