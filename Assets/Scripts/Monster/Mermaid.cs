using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mermaid : Monster
{
    private int countMove = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4009);
        itemCode.Add(4014);
        MaxHp = 20;
        Hp = 20;
        Speed = 2;
        AttackDamage = 5;
        Rank = "C";
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if(CheckPlayer())
        {           
                nextRoutines.Enqueue(NewActionRoutine(MoveRoutine(MovePosition(),2.0f)));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
        }

        return nextRoutines;
    }

    private Vector3 MovePosition()
    {
        Vector3 position;
        switch (countMove) {
            case 0:
                position = new Vector3(GetObjectPos().x + 2 * Speed, GetObjectPos().y, GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y + 2 * Speed, GetObjectPos().z);
                countMove = 2;
                return position;
            case 2:
                position = new Vector3(GetObjectPos().x - 2 * Speed, GetObjectPos().y, GetObjectPos().z);
                countMove = 3;
                return position;
            case 3:
                position = new Vector3(GetObjectPos().x, GetObjectPos().y - 2 * Speed, GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }


}
