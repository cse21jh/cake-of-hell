using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerManager.Instance.ChangeSpeed(10f);
    }
}
