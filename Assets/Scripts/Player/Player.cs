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
    public float Money { get; set; }
    private GameObject hitBox;

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
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
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
        Money = SaveManager.Instance.Money;
        PlayerManager.Instance.player = this;
        hitBox = transform.Find("HitBox").gameObject;
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
}
