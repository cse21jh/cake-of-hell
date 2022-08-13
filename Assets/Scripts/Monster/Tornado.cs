using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : Monster
{
    private Vector3 centerPoint;
    private Vector3 newPoint;
    private float runningTime = 0f;
    private float degree = 0f;
    private float radius = 1f;
    private float circleSpeed = 200f;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4006);
        MaxHp = 30;
        Hp = 30;
        Speed = (Util.GetPlayerSpeed() * 2);
        AttackDamage = 24;
        AttackRange = 4;
        Eyesight = 4;
        Rank = "B";
        centerPoint = transform.position + new Vector3(0, -1, 0);
        transform.localScale = new Vector3(2f, 2f, 0f);

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
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
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

    private IEnumerator AttackRoutine()
    {
        transform.localScale = new Vector3(3f, 3f, 0f);
        Vector3 direction = (GetPlayerPos() - GetObjectPos()).normalized;
        yield return MoveRoutine(direction * Speed, 2.0f);
        yield return  WaitRoutine(2f);
        centerPoint = transform.position + new Vector3(0, -1, 0);
        transform.localScale = new Vector3(2f, 2f, 0f);
        degree = 0;
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
        List<int> item = new List<int>() { 4006 };
        return item;
    }
}