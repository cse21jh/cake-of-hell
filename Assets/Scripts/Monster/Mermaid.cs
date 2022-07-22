using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid : Monster
{
    private int countMove = 0;
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
        if(!stopMove){
            switch (countMove) {
                case 0:
                    rb.velocity = new Vector2(2 * Speed, 0) ;
                    break;
                case 1:
                    rb.velocity = new Vector2(0, 2 * Speed) ;
                    break;
                case 2:
                    rb.velocity = new Vector2(-2 * Speed, 0) ;
                    break;
                case 3:
                    rb.velocity = new Vector2(0, -2 * Speed) ;
                    break;
            }
        }
            
        else if(stopMove)
            rb.velocity = new Vector2(0, 0) ;
    }

    void Move()
    {
        if (stopMove) {
            stopMove = false;
            countMove++;
        }
        else if (!stopMove)
            stopMove = true;

        if (countMove >= 4)
            countMove = 0;

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
        InvokeRepeating("Move", 1f, 1.5f);
        base.OnCollisionEnter2D(collision);
    }
}
