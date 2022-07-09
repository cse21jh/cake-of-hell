using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;

    public bool canMove = true;

    public BaseIndex inputBase = BaseIndex.Null;
    public IcingIndex inputIcing = IcingIndex.Null;
    public ToppingIndex inputTopping = ToppingIndex.Null;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ChangeSpeed(float value)
    {
        player.Speed += value;
        SaveManager.Instance.Speed += value;
    }

    public void ChangeHp(float value)
    {
        player.Hp += value;
        SaveManager.Instance.Hp += value;
    }

    public void ChangeMaxHp(float value)
    {
        player.MaxHp += value;
        SaveManager.Instance.MaxHp += value;
    }

    public void GetDamage(float value)
    {
        // 만약 피격 효과 넣는다면 여기에 삽입?
        player.Hp -= value;
        SaveManager.Instance.Hp += value;
        if(player.Hp<=0)
        {
            Debug.Log("Die");
            // 체력 0 되었을 때에 대한 내용 여기에 구현
        }
    }

    public void MakeCake()
    {
        CakeIndex cakeIndex = ItemManager.Instance.ReturnCake(inputBase, inputIcing, inputTopping);
        if(cakeIndex != CakeIndex.Null)
        {
            SaveManager.Instance.NumberOfBase[(int)inputBase] -= 1;
            SaveManager.Instance.NumberOfIcing[(int)inputIcing] -= 1;
            SaveManager.Instance.NumberOfTopping[(int)inputTopping] -= 1;

            inputBase = BaseIndex.Null;
            inputIcing = IcingIndex.Null;
            inputTopping = ToppingIndex.Null;
        }
        Debug.Log(cakeIndex.ToString());
    }
}
