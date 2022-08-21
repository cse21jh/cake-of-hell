using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingManager : MonoBehaviour
{
    private Image BackGround;
    private DialogUI dialog;
    [SerializeField]
    private int endingCount=0;
    private Sprite[] EndingBackGround = new Sprite[15];
    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        BackGround = canvas.transform.Find("BackGround").gameObject.GetComponent<Image>();
        dialog = canvas.transform.Find("DialogUI").GetComponent<DialogUI>();
        EndingBackGround = ResourceLoader.GetPackedSprite("Sprites/BackGround/EndingBackGround");
        CheckEnding();
        Ending();
    }

    private void CheckEnding()
    {
        if(GameManager.Instance.dieCount>=3)
        {
            endingCount = 1;
            return;
        }
        if(!GameManager.Instance.killMonsterInADay)
        {
            endingCount = 2;
            return;
        }
        for(int i = 0; i<GameManager.Instance.killEachMonsterCount.Length;i++)
        {
            if(GameManager.Instance.killEachMonsterCount[i]>=100)
            {
                endingCount = 3;
                return;
            }
        }
        if(GameManager.Instance.killMonsterCount >=1000)
        {
            endingCount = 4;
            return;
        }
        if (GameManager.Instance.killSSMonsterCount >= 300)
        {
            endingCount = 5;
            return;
        }
        if (GameManager.Instance.cantAcceptOrderCount >= 100 || TimeManager.Instance.reputation <=0f)
        {
            endingCount = 6;
            return;
        }
        // 7번은 아직 사냥꾼 존재 X
        if(GameManager.Instance.processSSCount>=150)
        {
            endingCount = 8;
            return;
        }
        if (GameManager.Instance.enterBlackHoleCount >= 100)
        {
            endingCount = 9;
            return;
        }
        if (GameManager.Instance.numberOfSatisfiedCustomer >= 200)
        {
            endingCount = 10;
            return;
        }
        if (GameManager.Instance.numberOfSatisfiedCustomer >= 100)
        {
            endingCount = 11;
            return;
        }
        if (GameManager.Instance.cantAcceptOrderCount <= 30)
        {
            endingCount = 12;
            return;
        }
        // 13번은 디자인 업그레이드 아직 X
        // 14번은 각 씬마다 시간 체크나 그런 부분이 힘듦. 일단 X
        if(GameManager.Instance.numberOfSoldCake-GameManager.Instance.numberOfSatisfiedCustomer >= 150)
        {
            endingCount = 15;
            return;
        }
    }



    private void Ending()
    {
        switch(endingCount)
        {
            case 0:
                Debug.Log("이거 버그임");
                dialog.SetText("이거 버그임");
                break;
            case 1:
                BackGround.sprite = EndingBackGround[0];
                dialog.SetText("플레이어는 사망하고 플레이어의 케이크를 좋아하던 많은 손님들은 플레이어를 추모하며 장례식을 치뤄준다.");
                break;
            case 2:
                BackGround.sprite = EndingBackGround[1];
                break;
            case 3:
                BackGround.sprite = EndingBackGround[2];
                break;
            case 4:
                BackGround.sprite = EndingBackGround[3];
                break;
            case 5:
                BackGround.sprite = EndingBackGround[4];
                break;
            case 6:
                BackGround.sprite = EndingBackGround[5];
                break;
            case 7:
                //BackGround.sprite = EndingBackGround[1];
            //string
            case 8:
                BackGround.sprite = EndingBackGround[6];
                break;
            case 9:
                BackGround.sprite = EndingBackGround[7];
                break;
            case 10:
                BackGround.sprite = EndingBackGround[8];
                break;
            case 11:
                BackGround.sprite = EndingBackGround[9];
                break;
            case 12:
                BackGround.sprite = EndingBackGround[10];
                break;
            case 13:
                //BackGround.sprite = EndingBackGround[1];
            //string
            case 14:
                //BackGround.sprite = EndingBackGround[1];
            //string
            case 15:
                BackGround.sprite = EndingBackGround[11];
                break;
        }
    }
}
