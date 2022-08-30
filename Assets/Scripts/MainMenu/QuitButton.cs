using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Quit);
    }

    private void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
