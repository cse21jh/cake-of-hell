using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapButton : MonoBehaviour
{
    private MiniMap map;

    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        var mapObj = Instantiate(ResourceLoader.GetPrefab("Prefabs/UI/MiniMap"), canvas.transform);
        map = mapObj.GetComponent<MiniMap>();
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            if(!map.IsActive())
            {
                UiManager.Instance.OpenUI(map);
            }
            else
            {
                UiManager.Instance.CloseUI(map);
            }
        });
    }
}
