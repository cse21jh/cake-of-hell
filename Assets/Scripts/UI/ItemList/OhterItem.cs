using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OhterItem : MonoBehaviour
{
    [SerializeField] GameObject[] wantToRemove = new GameObject[2];
    [SerializeField] GameObject wantToShow;

    public void OnClickExit()
    {
        wantToShow.gameObject.SetActive(true);
        wantToRemove[0].gameObject.SetActive(false);
        wantToRemove[1].gameObject.SetActive(false);
    }
}
