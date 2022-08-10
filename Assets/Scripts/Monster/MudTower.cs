using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTower : Monster
{
    private int countMove = 0;
    private bool attacking = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4001);
        MaxHp = 20;
        Hp = 20;
        Speed = 0;
        AttackDamage = 24;
        AttackRange = 1f;
        Eyesight = 5;
        Rank = "C";
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if(DistToPlayer()>Eyesight)
            { 
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
            }
            else
            {
                if (!attacking)
                {
                    nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
                }
                else
                {
                    nextRoutines.Enqueue(NewActionRoutine(CanAttack()));
                }
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));

        return nextRoutines;
    }

    private IEnumerator AttackRoutine()
    {
        attacking = true;
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet temp = bul.GetComponent<Bullet>();
            temp.host = gameObject;
            temp.dmg = AttackDamage;
            temp.duration = 2.0f;
            StartCoroutine(temp.ShootBullet(GetPlayerPos(), 2));
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    private IEnumerator CanAttack()
    {
        attacking = false;
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4001 };
        return item;
    }
}