using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }
    public float AttackDamage { get; set; }
    public float AttackRange { get; set; }

    protected float coolTime = 1.0f;
    protected float curCoolTime = 0;

    
    private Rigidbody2D rb;
    private Canvas canvas;
    //private SpriteRenderer spriteRenderer;
    //private Animator anim;

    [SerializeField]
    public GameObject itemListPrefab;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        UiManager.Instance.openItemList = false;
        rb = GetComponent<Rigidbody2D>();
        canvas = FindObjectOfType<Canvas>();
        //spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        Speed = SaveManager.Instance.Speed;
        Hp = SaveManager.Instance.Hp;
        MaxHp = SaveManager.Instance.MaxHp;
        AttackDamage = SaveManager.Instance.AttackDamage;
        AttackRange = SaveManager.Instance.AttackRange;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.canMove)
            Move();
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Move()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        rb.velocity = new Vector2(dx * Speed, dy * Speed);
    }

}
