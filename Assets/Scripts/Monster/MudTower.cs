using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MudTower : Monster
{
    private float angle;
    // Start is called before the first frame update
    protected override void Start()
    {
        itemCode.Add(4001);
        MaxHp = 20;
        Hp = 20;
        Speed = 0;
        AttackDamage = 24;
        AttackRange = 3f;
        Eyesight = 3;
        Rank = "C";
        MonsterNumber = 5;
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(3f)));
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));

        return nextRoutines;
    }

    private IEnumerator AttackRoutine()
    {
        if (CheckPlayer())
        {
            var bul = Instantiate(bullet, transform.position, Quaternion.identity);
            Bullet temp = bul.GetComponent<Bullet>();
            Vector3 monsterPos = GetObjectPos();
            Vector3 playerPos = GetPlayerPos();
            angle = Mathf.Atan2(playerPos.y - monsterPos.y, playerPos.x - monsterPos.x) * Mathf.Rad2Deg;
            bul.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            bul.GetComponent<SpriteRenderer>().sprite = AttackSprite[3];
            BoxCollider2D boxCollider = bul.GetComponent<BoxCollider2D>();
            Destroy(boxCollider);
            bul.AddComponent<PolygonCollider2D>();
            bul.GetComponent<PolygonCollider2D>().isTrigger = true;
            temp.host = gameObject;
            temp.dmg = AttackDamage;
            temp.duration = 2.0f;
            StartCoroutine(temp.ShootBullet(GetPlayerPos()));
            yield return new WaitForSeconds(2.0f);
        }
        yield return null;
    }

    private IEnumerator CanAttack()
    {
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4001 };
        return item;
    }
}