using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Monster
{
    private int countMove = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4007);
        MaxHp = 70;
        Hp = 70;
        Speed = Util.GetPlayerSpeed();
        AttackDamage = 40;
        AttackRange = 7;
        Eyesight = 8;
        Rank = "SS";
        MonsterNumber = 1;
        base.Start();
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
                nextRoutines.Enqueue(NewActionRoutine(MoveTowardPlayer(Speed, 1.0f)));
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
                position = new Vector3(GetObjectPos().x, GetObjectPos().y  + 2 * Speed, GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y  - 2 * Speed, GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    private IEnumerator AttackRoutine()
    {
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position + new Vector3(0, 3, 0), Quaternion.identity);
            Bullet temp = bul.GetComponent<Bullet>();
            temp.host = gameObject;
            temp.dmg = AttackDamage;
            temp.duration = 1.0f;
            StartCoroutine(temp.ShootBullet(GetPlayerPos(), 4));
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4007 };
        return item;
    }
}
