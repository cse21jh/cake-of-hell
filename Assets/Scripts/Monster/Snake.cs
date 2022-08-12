using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Monster
{
    private int countMove = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4010);
        itemCode.Add(4015);
        MaxHp = 40;
        Hp = 40;
        Speed = (Util.GetPlayerSpeed() * 5) / 6;
        AttackDamage = 30;
        AttackRange = 6;
        Eyesight = 6;
        Rank = "A";
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(MovePosition(), 2.0f)));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine())); 
                Vector2 direction = (GetPlayerPos() - GetObjectPos()).normalized;
                if((direction.x >= 0) && (direction.y >= 0))
                    countMove = 2;
                else if((direction.x >= 0) && (direction.y < 0))
                    countMove = 1;
                else if((direction.x < 0) && (direction.y >= 0))
                    countMove = 3;
                else
                    countMove = 0;
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(MovePosition(), 1.5f)));  
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1.5f)));
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
                position = new Vector3(GetObjectPos().x  + 2 * Speed, GetObjectPos().y  + 2 * Speed, GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x  - 2 * Speed, GetObjectPos().y  + 2 * Speed, GetObjectPos().z);
                countMove = 2;
                return position;
            case 2:
                position = new Vector3(GetObjectPos().x  - 2 * Speed, GetObjectPos().y  - 2 * Speed, GetObjectPos().z);
                countMove = 3;
                return position;
            case 3:
                position = new Vector3(GetObjectPos().x  + 2 * Speed, GetObjectPos().y  - 2 * Speed, GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    private IEnumerator AttackRoutine()
    {
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position, Quaternion.identity);
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
        List<int> item = new List<int>() { 4010, 4015 };
        return item;
    }
}
