using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInForest : Player
{
    private GameObject hitBox;

    void Awake()
    {

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        hitBox = transform.Find("HitBox").gameObject;
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetMouseButtonDown(0)&&curCoolTime <=0)
            StartCoroutine(Attack());

        if (curCoolTime > 0)
        {
            curCoolTime = Mathf.Max(curCoolTime - Time.deltaTime, 0);
        }
        base.Update();
    }

    private IEnumerator Attack()
    {
        curCoolTime = coolTime;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        hitBox.transform.position = (Vector2)transform.position + (mousePos - (Vector2)transform.position).normalized * AttackRange;
        hitBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitBox.gameObject.SetActive(false);
    }
}

