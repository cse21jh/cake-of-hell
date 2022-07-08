using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    public float MaxHp { get; set; }
    public float Hp { get; set; }
    public float Speed { get; set; }
    public float AttackDamage { get; set; }
    public float AttackRange { get; set; }

    protected float coolTime = 1.0f;
    protected float curCoolTime = 0;

    public bool openItemList = false;
    public bool alreadyOpenItemList = false; // 사냥꾼 등을 통해 열렸을 때에는 i눌러도 템창 안 열리도록

    private Rigidbody2D rb;
    private Canvas canvas;
    //private SpriteRenderer spriteRenderer;
    //private Animator anim;

    [SerializeField]
    private GameObject itemListPrefab;

    private GameObject itemList;

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        openItemList = false;
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
        if(Input.GetKeyDown(KeyCode.I)&& !alreadyOpenItemList)
        {
            if (!openItemList)
            {
                
                OpenItemList();
            }
            else if(openItemList)
            { 
                CloseItemList();
            }
        }
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

    public void OpenItemList()
    {
        openItemList = true;
        itemList = Instantiate(itemListPrefab, canvas.transform);
    }

    public void CloseItemList()
    {
        openItemList = false;
        Destroy(itemList);
    }
}
