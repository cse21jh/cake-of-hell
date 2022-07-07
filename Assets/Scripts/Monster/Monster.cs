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
    protected bool isAttacked; // 공격 당했는가. 후공의 경우 이 여부로 플레이어 따라오는 루틴 만들어도 될듯

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
            GameManager.Instance.GetDamage(AttackDamage);
            Debug.Log(Player.Instance.Hp);
        }
    }

    public virtual void GetDamage(float damage)
    {
        // 타격 효과, 체력바 등 여기서 호출
        Hp -= damage;
        Debug.Log(Hp);
        if(Hp<=0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        // 여기서 드롭 아이템 떨구는 함수 호출
        DropItem();
        Destroy(gameObject);
    }
        
    protected virtual void DropItem()
    {
        dropItem = Instantiate(dropItemPrefab, transform.position, transform.rotation);
    }
}
