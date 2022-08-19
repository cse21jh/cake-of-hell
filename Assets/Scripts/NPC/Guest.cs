using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GuestMode
{
    Come,
    Wait,
    Leave
};

public class Guest : MonoBehaviour
{
    public GuestMode Status { get; set; }
    public Counter OrderedCounter { get; set; } = null;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
