using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour
{
    protected Coroutine CurrentRoutine { get; private set; }
    private Queue<IEnumerator> nextRoutines = new Queue<IEnumerator>();

    public float Hp { get; set; }
    public float MaxHp { get; set; }
    public float AttackDamage { get; set; }
    public float AttackRange { get; set; }
    public float Speed { get; set; }
    public float Eyesight { get; set; }
    public string Rank { get; set; }

    protected Player player;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected bool isAttacked; // ���� ���ߴ°�. �İ��� ��� �� ���η� �÷��̾� ������� ��ƾ ���� �ɵ�
    protected bool stopMove = false;
    protected bool alreadyDie = false;

    protected GameObject Item;
    protected GameObject dropItem;
    protected GameObject monsterHitBox;
    protected GameObject bullet;
    protected List<int> itemCode = new List<int>();

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Item = Resources.Load<GameObject>("Prefabs/Item/Item");
        monsterHitBox = Instantiate(Resources.Load<GameObject>("Prefabs/Monster/MonsterHitBox"), this.transform);
        bullet = Resources.Load<GameObject>("Prefabs/Monster/Bullet");
        monsterHitBox.GetComponent<MonsterHitBox>().damage = AttackDamage;
    }

    protected virtual void Update()
    {
        if (CurrentRoutine == null && !alreadyDie)
        {
            NextRoutine();
        }
    }
    protected void NextRoutine()
    {
        if (nextRoutines.Count <= 0)
        {
            nextRoutines = DecideNextRoutine();
        }
        StartCoroutineUnit(nextRoutines.Dequeue());
    }
    protected abstract Queue<IEnumerator> DecideNextRoutine();
    private void StartCoroutineUnit(IEnumerator coroutine)
    {
        if (CurrentRoutine != null)
        {
            StopCoroutine(CurrentRoutine);
        }
        CurrentRoutine = StartCoroutine(coroutine);
    }
    protected IEnumerator NewActionRoutine(IEnumerator action)
    {
        yield return action;
        CurrentRoutine = null;
    }
    protected IEnumerator MoveRoutine(Vector3 destination, float time) // 지정된 좌표로 움직인다
    {
        yield return MoveRoutine(destination, time, AnimationCurve.Linear(0, 0, 1, 1));
    }
    protected IEnumerator MoveRoutine(Vector3 destination, float time, AnimationCurve curve)
    {
        Vector3 startPosition = transform.position;
        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            transform.position =
                Vector3.Lerp(startPosition, destination, curve.Evaluate(t / time));
            yield return null;
        }
        transform.position = destination;
    }

    protected IEnumerator MoveTowardPlayer(float speedMultiplier, float time = 0)     // 플레이어를 향해 움직인다
    {
        if (time != 0)
        {
            for (float t = 0; t <= time; t += Time.deltaTime)
            {
                Vector2 direction = (GetPlayerPos() - GetObjectPos()).normalized;
                transform.position = Vector3.Lerp(GetObjectPos(), GetPlayerPos(), Time.deltaTime / speedMultiplier);
                yield return null;
            }
        }
        else
        {
            Vector2 direction = (GetPlayerPos() - GetObjectPos()).normalized;
            transform.position = Vector3.Lerp(GetObjectPos(), GetPlayerPos(), Time.deltaTime / speedMultiplier);
            yield return null;
        }
    }


    protected IEnumerator WaitRoutine(float time)
    {
        yield return new WaitForSeconds(time);
    }

    protected Vector3 GetObjectPos()    // 오브젝트 벡터3 반환
    {
        return gameObject.transform.position;
    }
    protected Vector3 GetPlayerPos()    // 플레이어 벡터3 반환; 먼저 살아있는지 확인해야함
    {
        return player.transform.position;
    }
    protected float DistToPlayer()
    {
        return Vector2.Distance(gameObject.transform.position, player.transform.position);
        //return Vector3.Distance(GetObjectPos(), GetPlayerPos());
    }
    protected bool CheckPlayer()
    {
        if (FindObjectOfType<Player>() != null) return true;
        else return false;
    }




    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !alreadyDie)
        {
            PlayerManager.Instance.GetDamage(AttackDamage);
            Debug.Log(PlayerManager.Instance.GetHp());
        }
    }

    public virtual void GetDamage(float damage)
    {
        // Ÿ�� ȿ��, ü�¹� �� ���⼭ ȣ��
        Hp -= damage;
        SoundManager.Instance.PlayEffect("MonsterHit");
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            stopMove = true;
            Die();
        }
    }

    protected virtual void Die()
    {
        // ���⼭ ��� ������ ������ �Լ� ȣ��
        GameManager.Instance.AddKillMonsterCount();
        if(Rank == "SS")
        {
            GameManager.Instance.AddKillSSMonsterCount();
        }
        StartCoroutine(FadeOut());
        return;
    }

    protected virtual void DropItem(int itemCount)
    {
        Vector3[] eightDirection = new[] { new Vector3(+0.0f, 0.5f, 0.0f), new Vector3(+0.5f, 0.5f, 0.0f), new Vector3(+0.5f, 0.0f, 0.0f), new Vector3(+0.5f, -0.5f, 0.0f), new Vector3(0.0f, -0.5f, 0.0f), new Vector3(-0.5f, -0.5f, 0.0f), new Vector3(-0.5f, 0.0f, 0.0f), new Vector3(-0.5f, 0.5f, 0.0f) };
        List<int> itemDirection = new List<int>();
        int newNumber = Random.Range(0, 8);
        int k = 0;
        Debug.Log(itemCode.Count);
        for (int j = 0; j < itemCode.Count; j++)
        {
            for (int i = 0; i < itemCount;)
            {
                while (itemDirection.Contains(newNumber))
                {
                    newNumber = Random.Range(0, 8);
                }
                itemDirection.Add(newNumber);
                i++;
            }

            for (int i = 0; i < itemCount; i++)
            {
                Vector3 itemPosition = transform.position + eightDirection[itemDirection[i + (k * itemCount)]];
                dropItem = Instantiate(Item, itemPosition, transform.rotation);
                dropItem.GetComponent<SpriteRenderer>().sprite = Util.GetItem(itemCode[j]).SpriteImage;
                dropItem.GetComponent<DropItem>().SetItemCode(itemCode[j]);
            }
            k++;
        }
    }

    private IEnumerator FadeOut()
    {
        alreadyDie = true;
        this.gameObject.layer = 6;
        for (int i = 10; i >= 0; i--)
        {
            float f = i / 10.0f;
            Color c = sr.material.color;
            c.a = f;
            sr.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        DropItem(Random.Range(1, 4));
        Destroy(gameObject);
        yield return null;
    }

    public abstract List<int> GetItemCode();
    
}


