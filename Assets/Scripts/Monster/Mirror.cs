using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : Monster
{
    float angle;
    protected override void Start()
    {
        itemCode.Add(4011);
        MaxHp = 50;
        Hp = 50;
        Speed = 0;
        AttackDamage = 30;
        AttackRange = 7.5f;
        Eyesight = 7.5f;
        Rank = "A";
        MonsterNumber = 4;
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1.0f)));
            }
            else
            {
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }

    private IEnumerator AttackRoutine()
    {
        if (CheckPlayer())
        {
            monsterHitBox.gameObject.SetActive(true);
            Vector3 monsterPos = GetObjectPos();
            Vector3 playerPos = GetPlayerPos();
            Vector3 direction = (GetPlayerPos() - GetObjectPos()).normalized;
            angle = Mathf.Atan2(playerPos.y - monsterPos.y, playerPos.x - monsterPos.x)*Mathf.Rad2Deg;
            monsterHitBox.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            monsterHitBox.transform.localScale = new Vector3(0.3f, 0.3f, 0);

            for (float t = 0; t <= 1; t += Time.deltaTime)
            {
                monsterHitBox.transform.localScale = new Vector3(0.3f, monsterHitBox.transform.localScale.y+ (4 *Time.deltaTime)/3, 0);
                monsterHitBox.transform.position = monsterHitBox.transform.position + (direction*Time.deltaTime*2);
                yield return null;
            }
            yield return new WaitForSeconds(1.0f);
            monsterHitBox.transform.localScale = new Vector3(1, 1, 0);
            monsterHitBox.transform.position = GetObjectPos();
            monsterHitBox.gameObject.SetActive(false);
            CheckSprite(GetPlayerPos());
            direction = (GetPlayerPos() - GetObjectPos()).normalized;
            transform.position = GetPlayerPos() + 6 * direction;
            yield return new WaitForSeconds(1.0f);
        }
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4011 };
        return item;
    }
}