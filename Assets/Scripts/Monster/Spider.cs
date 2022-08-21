using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Monster
{
    private int countMove = 0;
    private bool attacking = false;
    private float angle;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4013);
        MaxHp = 70;
        Hp = 70;
        Speed = Util.GetPlayerSpeed()/2;
        AttackDamage = 60;
        AttackRange = 10;
        Eyesight = 10;
        Rank = "SS";
        MonsterNumber = 9;
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
                if (!attacking)
                {
                    nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
                }
                else
                {
                    nextRoutines.Enqueue(NewActionRoutine(MoveTowardPlayer(Speed,3.0f)));
                    nextRoutines.Enqueue(NewActionRoutine(CanAttack()));
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
                position = new Vector3(GetObjectPos().x + 3 * Speed, GetObjectPos().y , GetObjectPos().z);
                countMove = 1;
                return position;
            case 1:
                position = new Vector3(GetObjectPos().x - 3 * Speed, GetObjectPos().y , GetObjectPos().z);
                countMove = 0;
                return position;
        }
        return GetObjectPos();
    }

    private IEnumerator AttackRoutine()
    {
        attacking = true;
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet temp = bul.GetComponent<Bullet>();
            Vector3 monsterPos = GetObjectPos();
            Vector3 playerPos = GetPlayerPos();
            angle = Mathf.Atan2(playerPos.y - monsterPos.y, playerPos.x - monsterPos.x) * Mathf.Rad2Deg;
            bul.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            bul.GetComponent<SpriteRenderer>().sprite = AttackSprite[5];
            BoxCollider2D boxCollider= bul.GetComponent<BoxCollider2D>();
            Destroy(boxCollider);
            bul.AddComponent<PolygonCollider2D>();
            bul.GetComponent<PolygonCollider2D>().isTrigger = true;
            temp.host = gameObject;
            temp.dmg = AttackDamage;
            temp.duration = 2.0f;
            StartCoroutine(temp.ShootBullet(GetPlayerPos(), 3));
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    private IEnumerator CanAttack()
    {
        attacking = false;
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4013 };
        return item;
    }
}
