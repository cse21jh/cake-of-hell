using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : BaseUI
{
    private RectTransform smallPlayer;

    void Awake()
    {
        smallPlayer = gameObject.transform.GetChild(1).GetComponent<RectTransform>();
        gameObject.SetActive(false);
    }

    private void ChangePlayerPosition() 
    {
        switch(Util.GetNowMap())
        {
            case BattleMapList.MapHome:
                smallPlayer.anchoredPosition = new Vector2(85, 0);
                break;
            case BattleMapList.MapMagi:
                smallPlayer.anchoredPosition = new Vector2(-80, -10);
                break;
            case BattleMapList.MapC:
                smallPlayer.anchoredPosition = new Vector2(-255, 35);
                break;
            case BattleMapList.MapB:
                smallPlayer.anchoredPosition = new Vector2(-10, -120);
                break;
            case BattleMapList.MapA:
                smallPlayer.anchoredPosition = new Vector2(-175, -130);
                break;
            case BattleMapList.MapS:
                smallPlayer.anchoredPosition = new Vector2(230, -125);
                break;
            case BattleMapList.MapSS:
                smallPlayer.anchoredPosition = new Vector2(235, 110);
                break;
            default:
                smallPlayer.anchoredPosition = new Vector2(1000, 1000);
                break;
        }
    }

    public override void Open()
    {
        gameObject.SetActive(true);
        ChangePlayerPosition();
    }

    public override void Close()
    {
        gameObject.SetActive(false);
    }
}
