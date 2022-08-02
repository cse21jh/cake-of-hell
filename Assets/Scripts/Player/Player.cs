using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp { get; set; } = 120f;
    public float Hp { get; set; } = 120f;
    public float Speed { get; set; } = 5f;
    public float AttackDamage { get; set; } = 10f;
    public float AttackRange { get; set; } = 1.0f;
    public float Money { get; set; } = 0f;

    public bool inShop = true;
    private int nowImage;

    private Sprite[] playerImage = new Sprite[8];

    private GameObject hitBox;

    protected float coolTime = 1.0f;
    protected float curCoolTime = 0;

    public Dictionary<int, int> NumberOfBase { get; set; } = new Dictionary<int, int>() ;
    public Dictionary<int, int> NumberOfIcing { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfTopping { get; set; } = new Dictionary<int, int>();
    public Dictionary<int, int> NumberOfRaw { get; set; } = new Dictionary<int, int>();

    public Cake[] CakeList { get; set; } = new Cake[5];

    private Rigidbody2D rb;
    private Canvas canvas;
    private SpriteRenderer spriteRenderer;
    //private Animator anim;

    [SerializeField]
    public GameObject itemListPrefab;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
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
        for (int i = 0; i<ItemManager.Instance.ItemCodeList.Count;i++)
        {
            InitializationNumberOfItem(ItemManager.Instance.ItemCodeList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && curCoolTime <= 0 &&!inShop)
            StartCoroutine(Attack());

        if (curCoolTime > 0)
        {
            curCoolTime = Mathf.Max(curCoolTime - Time.deltaTime, 0);
        }
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

        spriteRenderer.sprite = PlayerImage(dx, dy);

        rb.velocity = new Vector2(dx * Speed, dy * Speed);
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

    private void InitializationNumberOfItem(int code)
    {
        switch(code/1000)
        { 
            case 1 :
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

    private Sprite PlayerImage(float dx, float dy)
    {
        if(inShop)
        {
            if (dy == -1.0f)
            {
                nowImage = 0;
                return playerImage[0];
            }
            else if (dy == 1.0f)
            {
                nowImage = 1;
                return playerImage[1];
            }
            else if (dx == -1.0f)
            {
                nowImage = 2;
                return playerImage[2];
            }
            else if (dx == 1.0f)
            {
                nowImage = 3;
                return playerImage[3];
            }
            else
            {
                if(nowImage>3)
                {
                    nowImage = nowImage - 4;
                    return playerImage[nowImage];
                }
                else
                    return spriteRenderer.sprite;
            }
        }
        else
        {
            if (dy == -1.0f)
            {
                nowImage = 4;
                return playerImage[4];
            }
            else if (dy == 1.0f)
            {
                nowImage = 5;
                return playerImage[5];
            }
            else if (dx == -1.0f)
            {
                nowImage = 6;
                return playerImage[6];
            }
            else if (dx == 1.0f)
            {
                nowImage = 7;
                return playerImage[7];
            }
            else
            {
                if (nowImage < 4)
                {
                    nowImage = nowImage + 4;
                    return playerImage[nowImage];
                }
                else
                    return spriteRenderer.sprite;
            }
        }
    }
}
