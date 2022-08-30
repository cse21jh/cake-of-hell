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
    private string[] sentence;
    private Util Util;
    void Start()
    {
        var canvas = GameObject.Find("Canvas");
        BackGround = canvas.transform.Find("BackGround").gameObject.GetComponent<Image>();
        dialog = canvas.transform.Find("DialogUI").GetComponent<DialogUI>();
        dialog.ExecuteAtEnd = GoToMainMenu;
        EndingBackGround = ResourceLoader.GetPackedSprite("Sprites/BackGround/EndingBackGround");
        CheckEnding();
        Ending();
    }

    private void CheckEnding()
    {
        if(GameManager.Instance.dieCount>=GameManager.Instance.maxRevivalCount)
        {
            endingCount = 1;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
        if(!GameManager.Instance.killMonsterInADay)
        {
            endingCount = 2;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
        for(int i = 0; i<GameManager.Instance.killEachMonsterCount.Length;i++)
        {
            if(GameManager.Instance.killEachMonsterCount[i]>=100 && !GameManager.Instance.shownEnding[3] )
            {
                endingCount = 3;
                GameManager.Instance.shownEnding[endingCount] = true;
                return;
            }
    }
        /*if(GameManager.Instance.killMonsterCount >=1000 &&!GameManager.Instance.shownEnding[4])
        {
            endingCount = 4;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }*/
        if (GameManager.Instance.killSSMonsterCount >= 300 && !GameManager.Instance.shownEnding[5])
        {
            endingCount = 5;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
        /*if (TimeManager.Instance.reputation <=0f))
        {
            endingCount = 6;
            return;
        }*/
        /*if ((GameManager.Instance.cantAcceptOrderCount >= 100 && !GameManager.Instance.shownEnding[6])
        {
            endingCount = 6;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }*/
        // 7번은 아직 사냥꾼 존재 X
        /*if(GameManager.Instance.processSSCount>=150 && !GameManager.Instance.shownEnding[8])
        {
            endingCount = 8;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }*/
        /*if (GameManager.Instance.enterBlackHoleCount >= 100 && !GameManager.Instance.shownEnding[9])
        {
            endingCount = 9;
           GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }*/
        if (GameManager.Instance.numberOfSatisfiedCustomer >= 200 && !GameManager.Instance.shownEnding[10])
        {
            endingCount = 10;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
        if (GameManager.Instance.numberOfSatisfiedCustomer >= 100 && !GameManager.Instance.shownEnding[11])
        {
            endingCount = 11;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
        /*if (GameManager.Instance.cantAcceptOrderCount <= 30 && !GameManager.Instance.shownEnding[12])
        {
            endingCount = 12;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }*/
        // 13번은 디자인 업그레이드 아직 X
        // 14번은 각 씬마다 시간 체크나 그런 부분이 힘듦. 일단 X
        if (GameManager.Instance.numberOfSoldCake-GameManager.Instance.numberOfSatisfiedCustomer >= 150 && !GameManager.Instance.shownEnding[15])
        {
            endingCount = 15;
            GameManager.Instance.shownEnding[endingCount] = true;
            return;
        }
    }



    private void Ending()
    {
        SaveManager.Instance.SaveShownEnding();
        switch(endingCount)
        {
            case 0:
                Debug.Log("이거 버그임");
                dialog.SetLongText(new string[] { "축하합니다! 모든 엔딩을 섭렵하셨습니다!... 혹은 버그이거나요.." });
                break;
            case 1:
                BackGround.sprite = EndingBackGround[0];
                dialog.SetLongText(new string[] { "플레이어는 사망하고 플레이어의 케이크를 좋아하던 많은 손님들은 플레이어를 추모하며 장례식을 치러 준다." });
                break;
            case 2:
                BackGround.sprite = EndingBackGround[1];
                sentence = new string[2];
                sentence[0] = "파밍 구역에 마물은 계속 생기지만 플레이어는 이를 파밍하지 못하고 관리하지 않았기 때문에 마물은 플레이어의 가게를 합동해서 공격하게 된다.";
                sentence[1] = "플레이어는 죽지는 않지만 가게를 도망쳐 새로운 곳으로 이전하게 된다.";
                //sentence = Util.LongSentenceToArray("파밍 구역에 마물은 계속 생기지만 플레이어는 이를 파밍하지 못하고 관리하지 않았기 때문에 마물은 플레이어의 가게를 합동해서 공격하게 된다. 플레이어는 죽지는 않지만 가게를 도망쳐 새로운 곳으로 이전하게 된다.");
                dialog.SetLongText(sentence);
                break;
            case 3:
                BackGround.sprite = EndingBackGround[2];
                dialog.SetLongText(new string[] { "몬스터가 멸종위기 직전이어서 몬스터를 사육하면서 제과제빵 일을 계속하다가 몬스터 사육으로 충분히 돈을 벌고 그 일이 적성에 더 잘 맞아서 몬스터 사육사로 직업을 바꾼다." });
                break;
            case 4:
                //BackGround.sprite = EndingBackGround[3];
                sentence = new string[2];
                sentence[0] = "플레이어는 몬스터 슬레이어라는 호칭을 얻고 그 능력을 인정받아 마왕에 의해 마계의 경찰청장으로 임명된다.";
                sentence[1] = "플레이어는 마계 주민들의 안전을 담당하며 제 2의 인생을 시작한다.";
                //sentence = Util.LongSentenceToArray("플레이어는 몬스터 슬레이어라는 호칭을 얻고 그 능력을 인정받아 마왕에 의해 마계의 경찰청장으로 임명된다. 플레이어는 마계 주민들의 안전을 담당하며 제 2의 인생을 시작한다.");
                dialog.SetLongText(sentence);
                break;
            case 5:
                BackGround.sprite = EndingBackGround[3];
                sentence = new string[4];
                sentence[0] = "SS등급 몬스터들은 화가 났다.";
                sentence[1] = "플레이어가 여느 날과 다름 없이 가게를 나서는데 가게 밖 환경이 SS등급 세계의 환경이다.";
                sentence[2] = "어디선가 몬스터들이 마구 나타나서 플레이어에게 달려들고 플레이어는 속수무책으로 죽고 만다.";
                sentence[3] = "몬스터들은 쓰러진 플레이어를 밟고 가게로 들어가 가게를 점령한다.";
                //sentence = Util.LongSentenceToArray("SS등급 몬스터들은 화가 났다. 플레이어가 여느 날과 다름 없이 가게를 나서는데 가게 밖 환경이 SS등급 세계의 환경이다. 어디선가 몬스터들이 마구 나타나서 플레이어에게 달려들고 플레이어는 속수무책으로 죽고 만다. 몬스터들은 쓰러진 플레이어를 밟고 가게로 들어가 가게를 점령한다.");
                dialog.SetLongText(sentence);
                break;
            case 6:
                //BackGround.sprite = EndingBackGround[5];
                sentence = new string[2];
                sentence[0] = "너무 많은 주문을 실패한 플레이어에 마계 주민들은 화가 나 마계의 SNS에서는 플레이어의 가게에 대한 단체 보이콧이 일어난다.";
                sentence[1] = "그 결과 플레이어의 가게는 하루에 손님이 한두 명밖에 오지 않는 신세로 전락하고 결국 플레이어는 적자를 감당하지 못하고 폐업하고 백수 신세가 된다.";
                //sentence = Util.LongSentenceToArray("너무 많은 주문을 실패한 플레이어에 마계 주민들은 화가 나 마계의 SNS에서는 플레이어의 가게에 대한 단체 보이콧이 일어난다. 그 결과 플레이어의 가게는 하루에 손님이 한두 명밖에 오지 않는 신세로 전락하고 결국 플레이어는 적자를 감당하지 못하고 폐업하고 백수 신세가 된다.");
                dialog.SetLongText(sentence);
                break;
            case 7:
                break;
            case 8:
                //BackGround.sprite = EndingBackGround[6];
                sentence = new string[4];
                sentence[0] = "더이상 SS등급 재료를 만족하지 못한 플레이어는 마법사에게 조언을 구한다.";
                sentence[1] = "마법사는 더 좋은 재료를 얻기 위해서는 미지의 세계로 가야 한다며 플레이어를 두고 미지의 세계로 가는 주술을 왼다.";
                sentence[2] = "번쩍하고 눈을 뜨니 플레이어는 마법사가 되어 있고 원래 마법사는 온데간데 없이 사라져 있다.";
                sentence[3] = "플레이어는 동굴 밖으로 나가지 못하고 마법사 일을 하게 된다.";
                //sentence = Util.LongSentenceToArray("더이상 SS등급 재료를 만족하지 못한 플레이어는 마법사에게 조언을 구한다. 마법사는 더 좋은 재료를 얻기 위해서는 미지의 세계로 가야 한다며 플레이어를 두고 미지의 세계로 가는 주술을 왼다. 번쩍하고 눈을 뜨니 플레이어는 마법사가 되어 있고 원래 마법사는 온데간데 없이 사라져 있다. 플레이어는 동굴 밖으로 나가지 못하고 마법사 일을 하게 된다.");
                dialog.SetLongText(sentence);
                break;
            case 9:
                //BackGround.sprite = EndingBackGround[7];
                //dialog.SetLongText(sentence);
                //break;
            case 10:
                BackGround.sprite = EndingBackGround[5];
                sentence = new string[2];
                sentence[0] = "마물을 조합하여 예쁜 마물 케이크로 만드는 것으로 유명해진 플레이어는 마왕의 성으로 들어가 마왕의 전속 제빵사로 임명받게 된다.";
                sentence[1] = "마왕의 성에서 더 퀄리티 높고 많은 재료들과 다양한 방법으로 마왕과 그들의 측근들의 입맛에 맞추어 새로운 일을 시작하게 된다.";
                //sentence = Util.LongSentenceToArray("마물을 조합하여 예쁜 마물 케이크로 만드는 것으로 유명해진 플레이어는 마왕의 성으로 들어가 마왕의 전속 제빵사로 임명받게 된다. 마왕의 성에서 더 퀄리티 높고 많은 재료들과 다양한 방법으로 마왕과 그들의 측근들의 입맛에 맞추어 새로운 일을 시작하게 된다.");
                dialog.SetLongText(sentence);
                break;
            case 11:
                BackGround.sprite = EndingBackGround[6];
                sentence = new string[2];
                sentence[0] = "마물을 조합하여 고객들의 마음을 잘 알아주는 것으로 유명해진 플레이어는 영혼의 세계로 스카웃 제안을 받는다.";
                sentence[1] = "영혼의 세계에서 다양한 영혼을 바탕으로 한 색다른 재료들로 일을 시작하게 된다.";
                //sentence = Util.LongSentenceToArray("마물을 조합하여 고객들의 마음을 잘 알아주는 것으로 유명해진 플레이어는 영혼의 세계로 스카웃 제안을 받는다. 영혼의 세계에서 다양한 영혼을 바탕으로 한 색다른 재료들로 일을 시작하게 된다.");
                dialog.SetLongText(sentence);
                break;
            case 12:
                //BackGround.sprite = EndingBackGround[10];
                sentence = new string[2];
                sentence[0] = "플레이어는 손님들의 주문을 미리 예상해 재료를 준비하는 제빵사로 유명해져 자신의 미래를 봐 달라고 찾아오는 손님이 많아진다.";
                sentence[1] = "손님들의 미래를 봐 주다가 플레이어는 자신도 몰랐던 자신의 예언 능력을 깨닫게 되고 케이크 가게를 접고 마왕까지 찾아올 정도로 유명한 예언자로 새로운 인생을 살게 된다.";
                //sentence = Util.LongSentenceToArray("플레이어는 손님들의 주문을 미리 예상해 재료를 준비하는 제빵사로 유명해져 자신의 미래를 봐 달라고 찾아오는 손님이 많아진다. 손님들의 미래를 봐 주다가 플레이어는 자신도 몰랐던 자신의 예언 능력을 깨닫게 되고 케이크 가게를 접고 마왕까지 찾아올 정도로 유명한 예언자로 새로운 인생을 살게 된다.");
                dialog.SetLongText(sentence);
                break;
            case 13:
                //BackGround.sprite = EndingBackGround[1];
            //string
            case 14:
            //BackGround.sprite = EndingBackGround[1];
            //string
            case 15:
                BackGround.sprite = EndingBackGround[7];
                dialog.SetLongText(new string[] { "마물 제빵사이지만 능력이 없다는 것을 알게 된 다른 마물이 제과제빵을 공부해서 맞은 편에 새로운 디저트 가게를 세우고 그 곳이 유명해져 플레이어의 가게는 망하게 된다." });
                break;
        }
    }

    private void GoToMainMenu()
    {
        GameManager.Instance.LoadScene("MainMenu");
    }
}
