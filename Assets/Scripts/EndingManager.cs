using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    private int endingCount=0;
    void Start()
    {
        CheckEnding();
        Ending();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (GameManager.Instance.cantAcceptOrderCount >= 100)
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
        //엔딩 케이스별 배경, 문구 등 스위치문으로 넣어주고 엔딩 틀기
    }
}
