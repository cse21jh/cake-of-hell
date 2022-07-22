using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<int, ProcessedItem> ProcessedItemList = new Dictionary<int, ProcessedItem>();
    public Dictionary<int, RawItem>RawItemList = new Dictionary<int, RawItem>();

    public List<int> ItemCodeList = new List<int>();



    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AddProcessedItem(0, "민 초 좋 아", ItemLevel.SS, null, "민 초 좋 아", "민 초 좋 아", 9999);
        AddRawItem(0, "민 초 좋 아", null, new List<int> { 0 }, new List<int> { 9999 }, new List<float> { 9999.0f });
        AddBases();
        AddToppings();
        AddIcings();
        AddRawItems();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


   public void AddBases()
    {
        //code / name / level/ sprite / keyword / flavor text / price
        AddProcessedItem(10001, "저주받은 흙", ItemLevel.C, Resources.Load<Sprite>("Sprites/Item/Mud"), "촉촉한 빵", "흙이 부드럽다니... 좀 이상하긴 해.", 10);
        AddProcessedItem(10002, "진화한 흙", ItemLevel.B, Resources.Load<Sprite>("Sprites/Item/Mud"), "쫄깃한 빵", "쫀드기 아닙니다. 구워 먹지 말 것.", 20);
        AddProcessedItem(10003, "광택이 나는 레더", ItemLevel.A, null, "질긴 가죽", "오만 번 씹어도 안 끊기는 쫄깃함.", 30);
        AddProcessedItem(10004, "뼈 반죽", ItemLevel.SS, null, "뼈", "수상할 정도로 바삭한 뼈.", 50);
        AddProcessedItem(10005, "버섯 빵", ItemLevel.B, null, "독버섯", "향기롭게 죽어가는 건가... 살려줘요.....", 20);
        AddProcessedItem(10006, "붉은 심장", ItemLevel.S, null, "심장", "물컹거리는 게 거의 버섯인데;;", 45);
        AddProcessedItem(10007, "불타는 심장", ItemLevel.SS, null, "뜨거운 심장", "사람 마음에 불 지르지 마세요.", 55);
    }

    public void AddToppings()
    {
        AddProcessedItem(20001, "유리 파편", ItemLevel.A, null, "따가운", "개운하네? 오늘은 양치 안 하고 자야지.", 30);
        AddProcessedItem(20002, "유리 조각", ItemLevel.S, null, "치명적인", "", 45);    // edit needed
        AddProcessedItem(20003, "레드 콘", ItemLevel.A, null, "뾰족뾰족한", "앗 따가! 벌에 쏘인 건 아니에요.", 30);
        AddProcessedItem(20004, "썩은 거미줄", ItemLevel.SS, null, "썩은", "으... 이 퀴퀴한 건 뭐야..... 사람은 못 먹겠지만.....", 50);
        AddProcessedItem(20005, "비늘 가루", ItemLevel.C, null, "반짝거리는", "생선보다도 비리지만 우리 가게에서 가장 잘나가요.", 10);
        AddProcessedItem(20006, "이빨 초콜릿", ItemLevel.A, null, "날카로운", "너무 달달해서 이가 썩을 것 같아...?", 35);
        AddProcessedItem(20007, "악한 영혼의 가루", ItemLevel.S, null, "사탄의", "달콤한 악마는 좀 귀하군요.", 45);
        AddProcessedItem(20008, "악한 악마의 가루", ItemLevel.SS, null, "지하의", "FOX시군요... 사람을 홀리네...", 55);
    }

    public void AddIcings()
    {
        AddProcessedItem(40001, "회오리", ItemLevel.B, null, "날아가는", "와! 회오리! 에어컨 18도보다 시원하다!", 25);
        AddProcessedItem(40002, "무지개빛 가루", ItemLevel.SS, null, "빛나는", "여기서 팡! 저기서 팡! 입 안에서 톡톡 터지는 무지개 스파클링!", 50);
        AddProcessedItem(40003, "피냄새 크림", ItemLevel.S, null, "한 맺힌", "이가 시린데... 늙었나.....", 40);
        AddProcessedItem(40004, "목소리 크림", ItemLevel.B, null, "아름다운", "헉! 이 풍미는 거의 캐비어...?", 25);
        AddProcessedItem(40005, "상냥한 목소리 크림", ItemLevel.S, null, "나긋나긋한", "이 따뜻함은 뭐지...? 어머니의 품...?", 45);
        AddProcessedItem(40006, "독 크림", ItemLevel.A, null, "죽음의", "먹었을 때 아파도 우리 가게 정상 영업합니다.", 35);
        AddProcessedItem(40007, "맹독 크림", ItemLevel.SS, null, "지옥의", "어라. 뒷목이 서늘한 건 기분 탓입니다.", 50);
        AddProcessedItem(40008, "독버섯 크림", ItemLevel.B, null, "화려한", "느끼하다구요? 여기 라면 국물은 안 파는데.....", 25);
    }

    public void AddRawItems()
    {
        // code / name/ sprite/ processedItemCode/ process price/ process time
        AddRawItem(81001, "진흙", Resources.Load<Sprite>("Sprites/Item/Mud"), new List<int> { 10001, 10002 }, new List<int> { 2, 3 }, new List<float> { 1.0f, 2.0f }  );
        AddRawItem(81002, "가죽", null, new List<int> { 10003 }, new List<int> { 4 }, new List<float> { 2.0f });
        AddRawItem(81003, "거미 뼈", null, new List<int> { 10004 }, new List<int> { 7 }, new List<float> { 3.0f });
        AddRawItem(81004, "독버섯", null, new List<int> { 10005 }, new List<int> { 3 }, new List<float> { 2.0f });
        AddRawItem(81005, "악마의 심장", null, new List<int> { 10006, 10007 }, new List<int> { 6, 7 }, new List<float> { 3.0f, 3.0f });
        AddRawItem(84006, "바람 한 병", null, new List<int> { 40001 }, new List<int> { 3 }, new List<float> { 2.0f });
        AddRawItem(84007, "비늘", null, new List<int> { 40002 }, new List<int> { 7 }, new List<float> { 3.0f });
        AddRawItem(84008, "핏방울", null, new List<int> { 40003 }, new List<int> { 5 }, new List<float> { 3.0f });
        AddRawItem(84009, "목소리", null, new List<int> { 40004, 40005 }, new List<int> { 3, 4 }, new List<float> { 2.0f, 3.0f });
        AddRawItem(84010, "독", null, new List<int> { 40006, 40007 }, new List<int> { 4, 6 }, new List<float> { 2.0f, 3.0f });
        AddRawItem(84011, "독버섯", null, new List<int> { 40008 }, new List<int> { 3 }, new List<float> { 2.0f });
        AddRawItem(82012, "모래", null, new List<int> { 20001, 20002 }, new List<int> { 4, 5 }, new List<float> { 2.0f, 3.0f });
        AddRawItem(82013, "뿔 조각", null, new List<int> { 20003 }, new List<int> { 4 }, new List<float> { 2.0f });
        AddRawItem(82014, "거미줄", null, new List<int> { 20004 }, new List<int> { 7 }, new List<float> { 3.0f });
        AddRawItem(82015, "인어 비늘", null, new List<int> { 20005 }, new List<int> { 2 }, new List<float> { 1.0f });
        AddRawItem(82016, "이빨", null, new List<int> { 20006 }, new List<int> { 4 }, new List<float> { 2.0f });
        AddRawItem(82017, "악한 영혼", null, new List<int> { 20007, 20008 }, new List<int> { 6, 7 }, new List<float> { 3.0f, 3.0f });
    }



    public void AddProcessedItem(int code, string name, ItemLevel level, Sprite spriteimage, string keyword, string flavorText, int price)
    {
        ProcessedItem processedItem = new ProcessedItem(code, name, level, spriteimage, keyword, flavorText, price);
        ProcessedItemList.Add(code, processedItem);
        ItemCodeList.Add(code);
        int itemType = (code / 10000);
        if (itemType == 1)
        {
            SaveManager.Instance.NumberOfBase.Add(code,0);
        }
        else if (itemType == 2)
        { 
            SaveManager.Instance.NumberOfTopping.Add(code, 0);
        }
        else if (itemType == 4)
        { 
            SaveManager.Instance.NumberOfIcing.Add(code, 0);
        }

    }

    public void AddRawItem(int code, string name, Sprite spriteimage, List<int> outputCode, List<int> price, List<float> duration)
    {
        ItemCodeList.Add(code);
        RawItem rawItem = new RawItem(code, name, spriteimage, outputCode, price, duration);
        RawItemList.Add(code, rawItem);
        SaveManager.Instance.NumberOfRaw.Add(code, 0);
    }


    //Fix me. 
    public string ReturnCake(int baseCode, int toppinCode, int icingcode)
    {
        string a = "민초케이크";
        return a;
    }

    public ProcessedItem GetProcessedItem(int code)
    {
        return ProcessedItemList[code];
    }

    public RawItem GetRawItem(int code)
    {
        return RawItemList[code];
    }

    public int GetOrder(int code)
    {
        return code % 10;
    }

    public int GetPriceOfProcessedItem(int code)
    {
        return ProcessedItemList[code].Price;
    }

    public int GetPriceOfRawItem(int code, int n)
    {
        return RawItemList[code].Price[n];
    }


}
