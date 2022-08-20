using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private int countMove = 0;
    private bool attacking = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4002);
        itemCode.Add(4012);
        MaxHp = 40;
        Hp = 40;
        Speed = Util.GetPlayerSpeed()/2;
        AttackDamage = 40;
        AttackRange = 3.5f;
        Eyesight = 4.5f;
        Rank = "A";
        MonsterNumber = 7;
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if(DistToPlayer()>Eyesight)
            { 
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(MovePosition(), 2.0f)));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
            }
            else
            {
                if (!attacking)
                {
                    if (DistToPlayer() > AttackRange)
                    {
                        nextRoutines.Enqueue(NewActionRoutine(MoveTowardPlayer(Speed)));
                    }
                    else
                    {
                        nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));
                        nextRoutines.Enqueue(NewActionRoutine(AttackRoutine(GetPlayerPos())));
                    }
                }
                else
                {
                    nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));
                }
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }

    private Vector3 MovePosition()
    {
        Vector3 position;
        switch (countMove)
        {
            case 0:
                position = new Vector3(GetObjectPos().x + 2 * Speed, GetObjectPos().y , GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x - 2 * Speed, GetObjectPos().y , GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    private IEnumerator AttackRoutine(Vector3 currentPlayerPosition)
    {
        attacking = true;
        monsterHitBox.gameObject.SetActive(true);
        monsterHitBox.transform.position = (Vector3)transform.position + (currentPlayerPosition - (Vector3)transform.position).normalized * AttackRange;
        yield return new WaitForSeconds(0.1f);
        monsterHitBox.transform.position = (Vector3)(GetObjectPos());
        monsterHitBox.gameObject.SetActive(false);
        attacking = false;
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4002, 4012 };
        return item;
    }
}
