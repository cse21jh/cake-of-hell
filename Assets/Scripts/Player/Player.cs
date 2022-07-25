using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MaxHp { get; set; } = 100f;
    public float Hp { get; set; } = 100f;
    public float Speed { get; set; } = 5f;
    public float AttackDamage { get; set; } = 10f;
    public float AttackRange { get; set; } = 1.0f;
    public float Money { get; set; } = 0f;
    
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
        for(int i = 0; i<ItemManager.Instance.ItemCodeList.Count;i++)
        {
            InitializationNumberOfItem(ItemManager.Instance.ItemCodeList[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && curCoolTime <= 0)
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
}
