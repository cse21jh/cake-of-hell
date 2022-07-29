using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float Hp { get; set; }
    public float MaxHp { get; set; }
    public float AttackDamage { get; set; }
    public float AttackRange { get; set; }
    public float Speed { get; set; }

    protected Player player;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected bool isAttacked; // ���� ���ߴ°�. �İ��� ��� �� ���η� �÷��̾� ������� ��ƾ ���� �ɵ�
    protected bool stopMove = false;
    protected bool alreadyDie = false;

    protected GameObject Item;
    protected GameObject dropItem;
    protected List<int> itemCode = new List<int>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Item = Resources.Load<GameObject>("Prefabs/Item/Item");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !alreadyDie)
        {
            PlayerManager.Instance.GetDamage(AttackDamage);
            Debug.Log(PlayerManager.Instance.GetHp());
        }
    }

    public virtual void GetDamage(float damage)
    {
        // Ÿ�� ȿ��, ü�¹� �� ���⼭ ȣ��
        Hp -= damage;
        SoundManager.Instance.PlayEffect("MonsterHit");
        Debug.Log(Hp);
        if(Hp<=0)
        {
            stopMove = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        // ���⼭ ��� ������ ������ �Լ� ȣ��
        StartCoroutine(FadeOut());
        return;
    }
        
    protected virtual void DropItem(int itemCount)
    {      
        Vector3[] eightDirection = new [] {new Vector3(+0.0f, 0.5f, 0.0f), new Vector3(+0.5f, 0.5f, 0.0f), new Vector3(+0.5f, 0.0f, 0.0f), new Vector3(+0.5f, -0.5f, 0.0f), new Vector3(0.0f, -0.5f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(-0.5f, 0.0f, 0.0f), new Vector3(-0.5f, 0.5f, 0.0f)};
        List<int> itemDirection = new List<int>();
        int newNumber = Random.Range(0,8);
        int k = 0;
        Debug.Log(itemCode.Count);
        for (int j = 0; j < itemCode.Count; j++)
        {
            for (int i = 0; i < itemCount;)
            {
                while(itemDirection.Contains(newNumber))
                { 
                    newNumber = Random.Range(0, 8);
                }
                itemDirection.Add(newNumber);
                i++;
            }

            for (int i = 0; i < itemCount; i++)
            {
                Vector3 itemPosition = transform.position + eightDirection[itemDirection[i+(k*itemCount)]];
                dropItem = Instantiate(Item, itemPosition, transform.rotation);
                dropItem.GetComponent<SpriteRenderer>().sprite = Util.GetItem(itemCode[j]).SpriteImage;
                dropItem.GetComponent<DropItem>().SetItemCode(itemCode[j]);
            }
            k++;
        }
    }

    private IEnumerator FadeOut()
    {
        alreadyDie = true;
        this.gameObject.layer = 6;
        for(int i=10;i>=0;i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        DropItem(Random.Range(1, 4));
        Destroy(gameObject);
        yield return null; 
    }
}
