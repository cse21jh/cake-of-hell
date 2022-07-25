using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    //private float maxHp = 100f;
    public float MaxHp { get; set; }

    //private float hp = 100f;
    public float Hp { get; set; }

    //private float speed = 5f;
    public float Speed { get; set; }

    public float AttackDamage { get; set; } 

    public float AttackRange { get; set; }

    public float Money { get; set; }

    public Dictionary<int, int> NumberOfBase { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfIcing { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfTopping { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfRaw { get; set; } = new Dictionary<int, int>();

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