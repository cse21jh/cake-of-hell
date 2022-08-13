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
        Speed = Util.GetPlayerSpeed()*2;
        AttackDamage = 40;
        AttackRange = 1;
        Eyesight = 6;
        Rank = "S";
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
                Vector2 direction = (GetPlayerPos() - GetObjectPos()).normalized;
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(direction * Speed, 2.0f)));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1.0f)));
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !alreadyDie)
        {
            PlayerManager.Instance.GetDamage(AttackDamage);
            Debug.Log(PlayerManager.Instance.GetHp());
        }
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4008 };
        return item;
    }
}
