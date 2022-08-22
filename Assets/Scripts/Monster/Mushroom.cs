using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Monster
{
    private SpriteRenderer spriteRenderer;
    private bool ishidden = true;
    private Color c;

    protected override void Start()
    {
        itemCode.Add(4004);
        MaxHp = 30;
        Hp = 30;
        Speed = 0;
        AttackDamage = 25;
        AttackRange = 1f;
        Eyesight = 3;
        Rank = "B";
        MonsterNumber = 6;
        base.Start();
        c = sr.material.color;
        c.a = 0;
        sr.material.color = c;
    }

    protected override Queue<IEnumerator> DecideNextRoutine()
    {
        Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

        if (CheckPlayer())
        {
            if (DistToPlayer() > Eyesight)
            {
                if(!ishidden)
                    nextRoutines.Enqueue(NewActionRoutine(FadeOutInMushroom()));
            }
            else
            {
                Color c = sr.material.color;
                c.a = 1;
                sr.material.color = c;
                ishidden = false;
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
        BoxCollider2D boxCollider = monsterHitBox.GetComponent<BoxCollider2D>();
        Destroy(boxCollider);
        monsterHitBox.gameObject.SetActive(true);
        mushroomHitbox.ChangeSize(3);
        monsterHitBox.GetComponent<SpriteRenderer>().sprite = AttackSprite[4];
        monsterHitBox.AddComponent<PolygonCollider2D>();
        yield return new WaitForSeconds(0.1f);
        mushroomHitbox.ChangeSize(1/3f);
        monsterHitBox.gameObject.SetActive(false);
        yield return null;
    }

    private IEnumerator FadeOutInMushroom()
    {
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
        ishidden = true;
    }

    public override List<int> GetItemCode()
    {
        List<int> item = new List<int>() { 4004 };
        return item;
    }
}