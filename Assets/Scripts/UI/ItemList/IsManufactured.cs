using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsManufactured : MonoBehaviour
{
    [SerializeField] GameObject wantToRemove;
    [SerializeField] GameObject wantToShow;

    public void OnClickExit()
    {
        wantToShow.gameObject.SetActive(true);
        wantToRemove.gameObject.SetActive(false);
    }
}
