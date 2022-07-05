using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public static Player Instance;

    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }


    private Rigidbody2D rb;
    //private SpriteRenderer spriteRenderer;
    //private Animator anim;
    
    void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        Speed = SaveManager.Instance.Speed;
        Hp = SaveManager.Instance.Hp;
        MaxHp = SaveManager.Instance.MaxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(dx * Speed, dy * Speed);
    }
}
