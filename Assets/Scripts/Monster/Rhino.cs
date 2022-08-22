using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rhino : Monster
{
    private int countMove = 0;
    private bool attacking = false;
    private float angle;

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
        bul.SetActive(true);
        Bullet temp = bul.GetComponent<Bullet>();
        Vector3 monsterPos = GetObjectPos();
        Vector3 playerPos = GetPlayerPos();
        angle = Mathf.Atan2(playerPos.y - monsterPos.y, playerPos.x - monsterPos.x) * Mathf.Rad2Deg;
        bul.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        bul.GetComponent<SpriteRenderer>().sprite = AttackSprite[5];
        BoxCollider2D boxCollider = bul.GetComponent<BoxCollider2D>();
        Destroy(boxCollider);
        bul.AddComponent<PolygonCollider2D>();
        bul.GetComponent<PolygonCollider2D>().isTrigger = true;
        temp.host = gameObject;
        temp.dmg = AttackDamage;
        temp.duration = 1.0f;
        StartCoroutine(temp.ShootBullet(GetPlayerPos()));
        yield return new WaitForSeconds(2f);
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4002, 4012 };
        return item;
    }
}
