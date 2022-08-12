using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToForest : MonoBehaviour
{
    [SerializeField] private string nextScene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if(TimeManager.Instance.isPrepareTime)
            { 
                GameManager.Instance.LoadScene(nextScene);
            }
        }
    }
}