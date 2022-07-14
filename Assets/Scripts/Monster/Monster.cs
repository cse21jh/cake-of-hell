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
            Debug.Log(SaveManager.Instance.Hp);
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
        for(int itemCount = Random.Range(1,4); itemCount > 0; itemCount--){
            DropItem();
        }
        Destroy(gameObject);
    }
        
    protected virtual void DropItem()
    {
        dropItem = Instantiate(dropItemPrefab, transform.position, transform.rotation);
    }
}
