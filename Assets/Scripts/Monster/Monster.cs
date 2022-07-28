using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    protected Coroutine CurrentRoutine { get; private set; }
    private Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

    public float Hp { get; set; }
    public float MaxHp { get; set; }
    public float AttackDamage { get; set; }
    public float AttackRange { get; set; }
    public float Speed { get; set; }

    protected Player player;
    protected Rigidbody2D rb;
    protected bool isAttacked; // ���� ���ߴ°�. �İ��� ��� �� ���η� �÷��̾� ������� ��ƾ ���� �ɵ�
    protected bool stopMove = false;

    protected GameObject dropItem;
    [SerializeField]
    protected GameObject dropItemPrefab;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
        DropItem(Random.Range(1,4));
        return;
    }
        
    protected virtual void DropItem(int itemCount)
    {      
        Vector3[] eightDirection = new [] {new Vector3(+0.0f, 0.5f, 0.0f), new Vector3(+0.5f, 0.5f, 0.0f), new Vector3(+0.5f, 0.0f, 0.0f), new Vector3(+0.5f, -0.5f, 0.0f), new Vector3(0.0f, -0.5f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(-0.5f, 0.0f, 0.0f), new Vector3(-0.5f, 0.5f, 0.0f)};
        List<int> itemDirection = new List<int>();
        int newNumber = Random.Range(0,8);
        for(int i = 0; i < itemCount;){
            if(itemDirection.Contains(newNumber))
                newNumber = Random.Range(0,8);
            else
            {
                itemDirection.Add(newNumber);
                i++;
            }
        }
        for(int i = 0; i < itemCount; i++) {
            Vector3 itemPosition = transform.position + eightDirection[i];
            dropItem = Instantiate(dropItemPrefab, itemPosition, transform.rotation);
        }
        
    }

    private IEnumerator FadeOut()
    {
        // work here
        Destroy(gameObject);
        yield return null; 
    }
}
