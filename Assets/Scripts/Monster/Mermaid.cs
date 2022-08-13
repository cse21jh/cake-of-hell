using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid : Monster
{
    private int countMove = 0;

    protected override void Start()
    {
        itemCode.Add(4009);
        itemCode.Add(4014);
        MaxHp = 20;
        Hp = 20;
        AttackDamage = 0;
        AttackRange = 0;
        Speed = Util.GetPlayerSpeed() / 2;
        Eyesight = 4;
        Rank = "C";
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
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(MoveAwayFromPlayer(Speed * 1.5f)));
            }
        }

        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }

    private Vector3 MovePosition()
    {
        Vector3 position;
        switch (countMove) {
            case 0:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y + (2 * Speed), GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y - (2 * Speed), GetObjectPos().z);
                countMove = 2;
                return position;
            case 2:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y - (2 * Speed), GetObjectPos().z);
                countMove = 3;
                return position;
            case 3:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y + (2 * Speed), GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    protected IEnumerator MoveAwayFromPlayer(float speedMultiplier)     // 플레이어로부터 도망친다
    {
        Vector3 direction = (GetObjectPos() - GetPlayerPos()).normalized;
        transform.position = transform.position + (direction * (speedMultiplier * Time.deltaTime));
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4009, 4014 };
        return item;
    }
}
