using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp { get; set; } = 120f;
    public float Hp { get; set; } = 120f;
    public float Speed { get; set; } = 4f;
    public float AttackDamage { get; set; } = 10f;
    public float AttackRange { get; set; } = 1.0f;
    public float Money { get; set; } = 0f;

    public bool inShop = true;
    private int nowImage;

    private Sprite[] playerImage = new Sprite[8];

    private GameObject hitBox;

    private float coolTime = 1.0f;
    private float curCoolTime = 0;

    public Dictionary<int, int> NumberOfBase { get; set; } = new Dictionary<int, int>() ;
    public Dictionary<int, int> NumberOfIcing { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfTopping { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfRaw { get; set; } = new Dictionary<int, int>();

    public Cake[] CakeList { get; set; } = new Cake[5];

    private Rigidbody2D rb;
    private Canvas canvas;
    public SpriteRenderer spriteRenderer;
    //private Animator anim;

    [SerializeField]
    public GameObject itemListPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        UiManager.Instance.openItemList = false;
        rb = GetComponent<Rigidbody2D>();
        canvas = FindObjectOfType<Canvas>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        PlayerManager.Instance.player = this;
        hitBox = transform.Find("HitBox").gameObject;
        playerImage = Resources.LoadAll<Sprite>("Sprites/Player/player");
        for (int i = 0; i<ItemManager.Instance.ItemCodeList.Count; i++)
        {
            InitializeNumberOfItem(ItemManager.Instance.ItemCodeList[i]);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && curCoolTime <= 0 && !inShop)
        {
            StartCoroutine(Attack());
        }

        if (curCoolTime > 0)
        {
            curCoolTime = Mathf.Max(curCoolTime - Time.deltaTime, 0);
        }
    }

    void FixedUpdate()
    {
        if(GameManager.Instance.canMove)
        {
            Move();
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    private void Move()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        float dy = Input.GetAxisRaw("Vertical");

        SetPlayerImage(dx, dy);

        rb.velocity = new Vector2(dx * Speed * 1.5f, dy * Speed * 1.5f);
    }

    private IEnumerator Attack()
    {
        curCoolTime = coolTime;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hitBox.transform.position = (Vector2)transform.position + (mousePos - (Vector2)transform.position).normalized * AttackRange;
        hitBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitBox.gameObject.SetActive(false);
    }

    private void InitializeNumberOfItem(int code)
    {
        switch(code / 1000)
        { 
            case 1:
                NumberOfBase.Add(code, 0);
                break;
            case 2:
                NumberOfIcing.Add(code, 0);
                break;
            case 3:
                NumberOfTopping.Add(code, 0);
                break;
            case 4:
                NumberOfRaw.Add(code, 0);
                break;
        }
    }

    private void SetPlayerImage(float dx, float dy)
    {
        int idx = dy < 0 ? 0 : dy > 0 ? 1 : dx < 0 ? 2 : dx > 0 ? 3 : -1;
        if(idx == -1) return;
        if(!inShop) idx += 4;
        nowImage = idx;
        spriteRenderer.sprite = playerImage[nowImage];
    }
}
