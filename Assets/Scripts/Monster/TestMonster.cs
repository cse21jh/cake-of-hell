using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : Monster
{
    private int nextMove;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        MaxHp = 100;
        Hp = 100;
        Speed = 5;
        AttackDamage = 5;

        InvokeRepeating("Move",0f,0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(nextMove * Speed, 0) ;
    }

    void Move()
    {
        nextMove = nextMove == 1 ? -1 : 1;
    }

    public override void GetDamage(float damage)
    {
        CancelInvoke("Move");
        InvokeRepeating("Move", 1f, 0.5f);
        base.GetDamage(damage);
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        CancelInvoke("Move");
        nextMove = 0;
        InvokeRepeating("Move", 1f, 1.5f);
        base.OnCollisionEnter2D(collision);
    }
}
