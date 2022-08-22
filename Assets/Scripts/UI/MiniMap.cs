using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : BaseUI
{
    private RectTransform smallPlayer;

    void Awake()
    {
        var img = gameObject.transform.GetChild(0).GetComponent<Image>();
        smallPlayer = gameObject.transform.GetChild(1).GetComponent<RectTransform>();
        var mapSprites = ResourceLoader.GetPackedSprite("Sprites/Background/Maps");
        if(GameManager.Instance.unlockMapSS)
        {
            img.sprite = mapSprites[4];
        }
        else if(GameManager.Instance.unlockMapS)
        {
            img.sprite = mapSprites[3];
        }
        else if(GameManager.Instance.unlockMapA)
        {
            img.sprite = mapSprites[0];
        }
        else if(GameManager.Instance.unlockMapB)
        {
            img.sprite = mapSprites[1];
        }
        else 
        {
            img.sprite = mapSprites[2];
        }
        gameObject.SetActive(false);
    }

    private void ChangePlayerPosition() 
    {
        switch(Util.GetNowMap())
        {
            case BattleMapList.MapHome:
                smallPlayer.anchoredPosition = new Vector2(75, 0);
                break;
            case BattleMapList.MapMagi:
                smallPlayer.anchoredPosition = new Vector2(-80, -5);
                break;
            case BattleMapList.MapC:
                smallPlayer.anchoredPosition = new Vector2(-225, 35);
                break;
            case BattleMapList.MapB:
                smallPlayer.anchoredPosition = new Vector2(-10, -100);
                break;
            case BattleMapList.MapA:
                smallPlayer.anchoredPosition = new Vector2(190, -110);
                break;
            case BattleMapList.MapS:
                smallPlayer.anchoredPosition = new Vector2(-160, -105);
                break;
            case BattleMapList.MapSS:
                smallPlayer.anchoredPosition = new Vector2(190, 90);
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
