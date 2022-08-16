using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Devil : Monster
{
    private Vector3 centerPoint; 
    private Vector3 newPoint;
    private float runningTime = 0f;
    private float degree = 0f;
    private float radius = 3f;
    private float circleSpeed = Util.GetPlayerSpeed() * 20f;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4005);
        itemCode.Add(4016);
        MaxHp = 60;
        Hp = 60;
        Speed = (Util.GetPlayerSpeed() * 3) / 4;
        AttackDamage = 40;
        AttackRange = 5;
        Eyesight = 5;
        Rank = "S";
        centerPoint = transform.position + new Vector3(0, -3, 0);
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
        if(degree >= 360)
            degree = 0;
        var rad = Mathf.Deg2Rad * (degree);
        var x = radius * Mathf.Sin(rad);
        var y = radius * Mathf.Cos(rad);
        transform.position = centerPoint + new Vector3(x, y);
        yield return null;
    }

    private IEnumerator AttackRoutine()
    {
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet temp = bul.GetComponent<Bullet>();
            temp.host = gameObject;
            temp.dmg = AttackDamage;
            temp.duration = 2.0f;
            StartCoroutine(temp.ShootBullet(GetPlayerPos(), 4));
            yield return new WaitForSeconds(2.0f);
            yield return MoveTowardPlayer(Speed, 2f);
            centerPoint = transform.position + new Vector3(0, -3, 0);
            degree = 0;

        }
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4005, 4016 };
        return item;
    }
}