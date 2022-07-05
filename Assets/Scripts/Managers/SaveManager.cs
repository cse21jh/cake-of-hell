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
