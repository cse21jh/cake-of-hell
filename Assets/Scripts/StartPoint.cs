using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        GameManager.Instance.startPoint = transform.position;    
    }
}
