using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Monster
{
    private Vector3 centerPoint;
    private Vector3 newPoint;
    private float degree = 0f;
    private float radius = 1f;
    private float circleSpeed = 200f;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4006);
        MaxHp = 30;
        Hp = 30;
        Speed = (Util.GetPlayerSpeed());
        AttackDamage = 24;
        AttackRange = 4;
        Eyesight = 4;
        Rank = "B";
        centerPoint = transform.position + new Vector3(0, -1, 0);
        transform.localScale = new Vector3(2f, 2f, 0f);
        MonsterNumber = 10;
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                nextRoutines.Enqueue(NewActionRoutine(CircleMoveRoutine()));
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1.5f)));
                Vector3 playerPos = GetPlayerPos();
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine(playerPos)));
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }

    private IEnumerator CircleMoveRoutine()
    {
        degree += Time.deltaTime * circleSpeed;
        if (degree >= 360)
            degree = 0;
        var rad = Mathf.Deg2Rad * (degree);
        var x = radius * Mathf.Sin(rad);
        var y = radius * Mathf.Cos(rad);
        transform.position = centerPoint + new Vector3(x, y);
        yield return null;
    }

    private IEnumerator AttackRoutine(Vector3 playerPos)
    {
        degree = 0;
        transform.localScale = new Vector3(3f, 3f, 0f);
        Vector3 direction = (playerPos - GetObjectPos()).normalized;
        yield return MoveRoutine(GetObjectPos()+direction * Speed*3.0f, 2.0f);
        centerPoint = transform.position + new Vector3(0, -1, 0);
        yield return WaitRoutine(2f);
        transform.localScale = new Vector3(2f, 2f, 0f);
    }
    
    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4006 };
        return item;
    }
}