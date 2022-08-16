using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<int, ProcessedItem> ProcessedItemList = new Dictionary<int, ProcessedItem>();
    public Dictionary<int, RawItem> RawItemList = new Dictionary<int, RawItem>();

    public List<int> ItemCodeList = new List<int>();

    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AddProcessedItem(0, "민 초 좋 아", ItemLevel.SS, null, "민 초 좋 아", "민 초 좋 아", 9999, "민 초 좋 아");
        AddRawItem(0, "민 초 좋 아", null, new List<int> { 0 }, new List<float> { 9999f }, new List<float> { 9999.0f });
        AddBases();
        AddIcings();
        AddToppings();
        AddRawItems();
    }
    

    public void AddProcessedItem(int code, string name, ItemLevel level, Sprite spriteimage, string keyword, string flavorText, float price, string flavorWord)
    {
        ProcessedItem processedItem = new ProcessedItem(code, name, level, spriteimage, keyword, flavorText, price, flavorWord);
        ProcessedItemList.Add(code, processedItem);
        ItemCodeList.Add(code);
    }

    public void AddRawItem(int code, string name, Sprite spriteimage, List<int> outputCode, List<float> price, List<float> duration)
    {
        ItemCodeList.Add(code);
        RawItem rawItem = new RawItem(code, name, spriteimage, outputCode, price, duration);
        RawItemList.Add(code, rawItem);
    }

    public ProcessedItem GetProcessedItem(int code)
    {
        return ProcessedItemList[code];
    }

    public RawItem GetRawItem(int code)
    {
        return RawItemList[code];
    }

    public float GetPriceOfProcessedItem(int code)
    {
        return ProcessedItemList[code].Price;
    }

    public float GetPriceOfRawItem(int code, int n)
    {
        return RawItemList[code].Price[n];
    }


    public void AddBases()
    {
        //code / name / level/ sprite / keyword / flavor text / price
        AddProcessedItem(1001, "저주받은 흙", ItemLevel.C, Resources.Load<Sprite>("Sprites/Item/After Process/cursed mud"), "촉촉한 빵", "흙이 부드럽다니... 좀 이상하긴 해.", 10f, "부드러운");
        AddProcessedItem(1002, "진화한 흙", ItemLevel.B, Resources.Load<Sprite>("Sprites/Item/After Process/neo mud"), "쫄깃한 빵", "쫀드기 아닙니다. 구워 먹지 말 것.", 20f, "쫀득한");
        AddProcessedItem(1003, "광택이 나는 레더", ItemLevel.A, null, "질긴 가죽", "오만 번 씹어도 안 끊기는 쫄깃함.", 30f, "쫄깃한");
        AddProcessedItem(1004, "뼈 반죽", ItemLevel.SS, null, "뼈", "수상할 정도로 바삭한 뼈.", 50f, "바삭한");
        AddProcessedItem(1005, "버섯 빵", ItemLevel.B, null, "독버섯", "향기롭게 죽어가는 건가... 살려줘요.....", 20f, "향기로운");
        AddProcessedItem(1006, "붉은 심장", ItemLevel.S, Resources.Load<Sprite>("Sprites/Item/After Process/red heart"), "심장", "물컹거리는 게 거의 버섯인데;;", 45f, "물컹거리는");
        AddProcessedItem(1007, "불타는 심장", ItemLevel.SS, null, "뜨거운 심장", "사람 마음에 불 지르지 마세요.", 55f, "불이 나는");
    }

    public void AddIcings()
    {
        AddProcessedItem(2001, "회오리", ItemLevel.B, Resources.Load<Sprite>("Sprites/Item/After Process/tornado cream"), "날아가는", "와! 회오리! 에어컨 18도보다 시원하다!", 25f, "시원한");
        AddProcessedItem(2002, "무지개빛 가루", ItemLevel.SS, null, "빛나는", "여기서 팡! 저기서 팡! 입 안에서 톡톡 터지는 무지개 스파클링!", 50f, "톡톡 터지는");
        AddProcessedItem(2003, "피냄새 크림", ItemLevel.S, null, "한 맺힌", "이가 시린데... 늙었나.....", 40f, "시린");
        AddProcessedItem(2004, "목소리 크림", ItemLevel.B, null, "아름다운", "헉! 이 풍미는 거의 캐비어...?", 25f, "풍미가 있는");
        AddProcessedItem(2005, "상냥한 목소리 크림", ItemLevel.S, null, "나긋나긋한", "이 따뜻함은 뭐지...? 어머니의 품...?", 45f, "따뜻한");
        AddProcessedItem(2006, "독 크림", ItemLevel.A, Resources.Load<Sprite>("Sprites/Item/After Process/poison cream"), "죽음의", "먹었을 때 아파도 우리 가게 정상 영업합니다.", 35f, "아픈");
        AddProcessedItem(2007, "맹독 크림", ItemLevel.SS, null, "지옥의", "어라. 뒷목이 서늘한 건 기분 탓입니다.", 50f, "서늘한");
        AddProcessedItem(2008, "독버섯 크림", ItemLevel.B, null, "화려한", "느끼하다구요? 여기 라면 국물은 안 파는데.....", 25f, "느끼한");
    }

    public void AddToppings()
    {
        AddProcessedItem(3001, "유리 파편", ItemLevel.A, null, "따가운", "개운하네? 오늘은 양치 안 하고 자야지.", 30f, "개운한");
        AddProcessedItem(3002, "유리 조각", ItemLevel.S, null, "치명적인", "슬러시도 아니고 왜 차가운 거야?", 45f, "차가운");
        AddProcessedItem(3003, "레드 콘", ItemLevel.A, Resources.Load<Sprite>("Sprites/Item/After Process/redcone_item"), "뾰족뾰족한", "앗 따가! 벌에 쏘인 건 아니에요.", 30f, "쏘는");
        AddProcessedItem(3004, "썩은 거미줄", ItemLevel.SS, null, "썩은", "으... 이 퀴퀴한 건 뭐야..... 사람은 못 먹겠지만.....", 50f, "퀴퀴한");
        AddProcessedItem(3005, "비늘 가루", ItemLevel.C, null, "반짝거리는", "생선보다도 비리지만 우리 가게에서 가장 잘나가요.", 10f, "비린");
        AddProcessedItem(3006, "이빨 초콜릿", ItemLevel.A, Resources.Load<Sprite>("Sprites/Item/After Process/tooth_chocolate"), "날카로운", "너무 달달해서 이가 썩을 것 같아...?", 35f, "달달한");
        AddProcessedItem(3007, "악한 영혼의 가루", ItemLevel.S, null, "사탄의", "달콤한 악마는 좀 귀하군요.", 45f, "달콤한");
        AddProcessedItem(3008, "악한 악마의 가루", ItemLevel.SS, null, "지하의", "FOX시군요... 사람을 홀리네...", 55f, "홀리는");
    }


    public void AddRawItems()
    {
        // code / name/ sprite/ processedItemCode/ process price/ process time
        AddRawItem(4001, "진흙", Resources.Load<Sprite>("Sprites/Item/Before Process/Mud"), new List<int> { 1001, 1002 }, new List<float> { 2f, 3f }, new List<float> { 1.0f, 2.0f });
        AddRawItem(4002, "가죽", Resources.Load<Sprite>("Sprites/Item/Before Process/rhino_leather"), new List<int> { 1003 }, new List<float> { 4f }, new List<float> { 2.0f });
        AddRawItem(4003, "거미 뼈", null, new List<int> { 1004 }, new List<float> { 7f }, new List<float> { 3.0f });
        AddRawItem(4004, "독버섯", null, new List<int> { 1005, 2008 }, new List<float> { 3f, 3f }, new List<float> { 2.0f, 2.0f });
        AddRawItem(4005, "악마의 심장", null, new List<int> { 1006, 1007 }, new List<float> { 6f, 7f }, new List<float> { 3.0f, 3.0f });
        AddRawItem(4006, "바람 한 병", null, new List<int> { 2001 }, new List<float> { 3f }, new List<float> { 2.0f });
        AddRawItem(4007, "비늘", null, new List<int> { 2002 }, new List<float> { 7f }, new List<float> { 3.0f });
        AddRawItem(4008, "핏방울", null, new List<int> { 2003 }, new List<float> { 5f }, new List<float> { 3.0f });
        AddRawItem(4009, "목소리", Resources.Load<Sprite>("Sprites/Item/Before Process/mermaid_voice"), new List<int> { 2004, 2005 }, new List<float> { 3f, 4f }, new List<float> { 2.0f, 3.0f });
        AddRawItem(4010, "독", null, new List<int> { 2006, 2007 }, new List<float> { 4f, 6f }, new List<float> { 2.0f, 3.0f });
        AddRawItem(4011, "모래", null, new List<int> { 3001, 3002 }, new List<float> { 4f, 5f }, new List<float> { 2.0f, 3.0f });
        AddRawItem(4012, "뿔 조각", Resources.Load<Sprite>("Sprites/Item/Before Process/rhino_horn"), new List<int> { 3003 }, new List<float> { 4f }, new List<float> { 2.0f });
        AddRawItem(4013, "거미줄", null, new List<int> { 3004 }, new List<float> { 7f }, new List<float> { 3.0f });
        AddRawItem(4014, "인어 비늘", Resources.Load<Sprite>("Sprites/Item/Before Process/mermaid_scale"), new List<int> { 3005 }, new List<float> { 2f }, new List<float> { 1.0f });
        AddRawItem(4015, "이빨", null, new List<int> { 3006 }, new List<float> { 4f }, new List<float> { 2.0f });
        AddRawItem(4016, "악한 영혼", null, new List<int> { 3007, 3008 }, new List<float> { 6f, 7f }, new List<float> { 3.0f, 3.0f });
    }



}
