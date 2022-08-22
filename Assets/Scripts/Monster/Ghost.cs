using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    private int countMove = 0;
    
    protected override void Start()
    {
        itemCode.Add(4008);
        MaxHp = 60;
        Hp = 60;
        Speed = Util.GetPlayerSpeed();
        AttackDamage = 40;
        AttackRange = 1;
        Eyesight = 7;
        Rank = "S";
        MonsterNumber = 2;
        base.Start();
        StartCoroutine(Fade());

    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(MovePosition(), 1.0f)));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
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
                position = new Vector3(GetObjectPos().x +  Speed, GetObjectPos().y, GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x -  Speed, GetObjectPos().y, GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    private IEnumerator AttackRoutine()
    {
        Vector3 direction = (GetPlayerPos() - GetObjectPos()).normalized;
        yield return MoveRoutine(direction * Speed, 2.0f);
        yield return WaitRoutine(1.0f);
    }

    private IEnumerator Fade()
    {
        while(!alreadyDie)
        {
            for (int i = 10; i >= 0; i--)
            {
                float f = i / 10.0f;
                Color c = sr.material.color;
                c.a = f;
                sr.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1f);
            for (int i = 0; i <= 10; i++)
            {
                float f = i / 10.0f;
                Color c = sr.material.color;
                c.a = f;
                sr.material.color = c;
                yield return new WaitForSeconds(0.1f);
            }
        }

        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4008 };
        return item;
    }
}
