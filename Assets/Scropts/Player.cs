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
        //DontDestroyOnLoad(gameObject); ���� �Ȱ� �Ĺ� ���� ĳ���� �и� �� �� ����� �ʿ䰡 ���� ��?
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
