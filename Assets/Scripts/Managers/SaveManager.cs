using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    //private float maxHp = 100f;
    public float MaxHp { get; set; } = 100f;

    //private float hp = 100f;
    public float Hp { get; set; } = 100f;

    //private float speed = 5f;
    public float Speed { get; set; } = 5f;

    public float AttackDamage { get; set; } = 10f;

    public float AttackRange { get; set; } = 1.0f;


    public int[] NumberOfIcing = new int[(int)IcingIndex.Number];

    public int[] NumberOfTopping = new int[(int) ToppingIndex.Number];

    public int[] NumberOfBase = new int[(int)BaseIndex.Number];


    public int[] NumberOfRIcing = new int[(int)RIcingIndex.Number];

    public int[] NumberOfRTopping = new int[(int)RToppingIndex.Number];

    public int[] NumberOfRBase = new int[(int)RBaseIndex.Number];

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
