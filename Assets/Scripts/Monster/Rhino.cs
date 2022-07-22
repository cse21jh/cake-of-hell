using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private int nextMove;
    private bool stopMove = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        MaxHp = 20;
        Hp = 10;
        Speed = 2;
        AttackDamage = 5;

        InvokeRepeating("Move",0f,2.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(!stopMove)
            rb.velocity = new Vector2(0, nextMove * Speed) ;
        else if(stopMove)
            rb.velocity = new Vector2(0, 0) ;
    }

    void Move()
    {
        if (stopMove) {
            stopMove = false;
            nextMove = nextMove == 1 ? -1 : 1;
        }
        else if (!stopMove)
            stopMove = true;

    }

    public override void GetDamage(float damage)
    {
        CancelInvoke("Move");
        rb.velocity = new Vector2(0, 0);
        stopMove = true;
        InvokeRepeating("Move", 1f, 2.0f);
        base.GetDamage(damage);
    }

    protected override void Die()
    {
        Debug.Log("TestMonsterDie");
        base.Die();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        CancelInvoke("Move");
        nextMove = 0;
        InvokeRepeating("Move", 1f, 1.5f);
        base.OnCollisionEnter2D(collision);
    }
}
