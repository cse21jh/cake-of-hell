using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Monster
{
    private int countMove = 0;
    private bool attacking = false;
    private SpriteRenderer spriteRenderer;
    
    protected override void Start()
    {
        itemCode.Add(1005);
        MaxHp = 30;
        Hp = 30;
        Speed = 0;
        AttackDamage = 25;
        AttackRange = 1f;
        Eyesight = 3;
        Rank = "B";
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        base.Start();
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
                nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(2f)));
                nextRoutines.Enqueue(NewActionRoutine(AttackRoutine()));
            }
        }
        else nextRoutines.Enqueue(NewActionRoutine(WaitRoutine(1f)));

        return nextRoutines;
    }

    private IEnumerator AttackRoutine()
    {
        MonsterHitBox mushroomHitbox = monsterHitBox.GetComponent<MonsterHitBox>();

        attacking = true;
        monsterHitBox.gameObject.SetActive(true);
        mushroomHitbox.ChangeSize(3);
        yield return new WaitForSeconds(0.1f);
        mushroomHitbox.ChangeSize(1/3f);
        monsterHitBox.gameObject.SetActive(false);
        attacking = false;
        yield return null;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4004 };
        return item;
    }
}