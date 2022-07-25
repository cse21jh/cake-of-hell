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

    protected Rigidbody2D rb;
    protected bool isAttacked; // ���� ���ߴ°�. �İ��� ��� �� ���η� �÷��̾� ������� ��ƾ ���� �ɵ�

    protected GameObject dropItem;
    [SerializeField]
    protected GameObject dropItemPrefab;

    // Start is called before the first frame update
    protected virtual void Start()
    {
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
        Debug.Log(Hp);
        if(Hp<=0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // ���⼭ ��� ������ ������ �Լ� ȣ��
        DropItem(Random.Range(1,4));
        Destroy(gameObject);
    }
        
    protected virtual void DropItem(int itemCount)
    {   
        Vector3 itemPosition = transform.position + new Vector3(+0.5f, 0.0f, 0.0f);
        for(int i = itemCount; i > 0; i--) {
            dropItem = Instantiate(dropItemPrefab, itemPosition, transform.rotation);
            itemPosition = itemPosition - new Vector3(-0.5f, 0.0f, 0.0f);
        }
        
    }
}
