using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField]
    private float speed = 5.0f;

    private Rigidbody2D rb;
    //private SpriteRenderer spriteRenderer;
    //private Animator anim;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(gameObject); 가게 안과 파밍 때의 캐릭터 분리 할 지 고민할 필요가 있을 듯?
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

        rb.velocity = new Vector2(dx * speed, dy * speed);
    }
}
