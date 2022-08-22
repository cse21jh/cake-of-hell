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
    public int MonsterNumber { get; set; }

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

    protected Sprite rSprite;
    protected Sprite lSprite;

    protected Sprite[] AttackSprite = new Sprite[9];
    protected int lookLeft = 1;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        AttackSprite = ResourceLoader.GetPackedSprite("Sprites/Mob/mob attack effect");
        player = FindObjectOfType<Player>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        Item = ResourceLoader.GetPrefab("Prefabs/Item/Item");
        monsterHitBox = Instantiate(ResourceLoader.GetPrefab("Prefabs/Monster/MonsterHitBox"), this.transform);
        bullet = ResourceLoader.GetPrefab("Prefabs/Monster/Bullet");
        monsterHitBox.GetComponent<MonsterHitBox>().damage = AttackDamage;
        lSprite = ResourceLoader.GetPackedSprite("Sprites/Mob/mobs")[MonsterNumber * 2];
        rSprite = ResourceLoader.GetPackedSprite("Sprites/Mob/mobs")[MonsterNumber * 2+1];
        sr.sprite = lSprite;
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
    
    protected IEnumerator MoveRoutine(Vector3 destination, float time)
    {
        CheckSprite(destination);
        AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
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
        CheckSprite(GetPlayerPos());
        for (float t = 0; t <= time; t += Time.deltaTime)
        {
            Vector3 direction = (GetPlayerPos() - GetObjectPos()).normalized;
            transform.position = transform.position + (direction * (speedMultiplier * Time.deltaTime));
            yield return null;
        }
        yield return null;
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

    protected void CheckSprite(Vector3 destination)
    {
        if (destination.x > GetObjectPos().x && transform.localScale.x>=0)
        {
            Vector3 scale = transform.localScale;

            scale.x = -scale.x;
            
            transform.localScale = scale;
            lookLeft = -1;
        }
        else if(destination.x < GetObjectPos().x && transform.localScale.x <= 0)
        {
            Vector3 scale = transform.localScale;

            scale.x = -scale.x;

            transform.localScale = scale;
            lookLeft = 1;
        }
    }



    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !alreadyDie)
        {
            PlayerManager.Instance.GetDamage(AttackDamage);
            Debug.Log(PlayerManager.Instance.GetHp());
        }
        if (collision.gameObject.tag == "Wall")
        {
            NextRoutine();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && !alreadyDie)
        {
            PlayerManager.Instance.GetDamage(AttackDamage);
            Debug.Log(PlayerManager.Instance.GetHp());
        }

        if (other.gameObject.tag == "Wall")
        {
            NextRoutine();
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
        GameManager.Instance.killEachMonsterCount[MonsterNumber]++;
        if(GameManager.Instance.killEachMonsterCount[MonsterNumber]>=100)
        {
            GameManager.Instance.MoveToEndingScene();
        }
        GameManager.Instance.AddKillMonsterCount();
        if (GameManager.Instance.killMonsterCount >= 1000)
        {
            //GameManager.Instance.MoveToEndingScene();
        }
        GameManager.Instance.killMonsterInADay = true;
        if (Rank == "SS")
        {
            GameManager.Instance.AddKillSSMonsterCount();
            if (GameManager.Instance.killSSMonsterCount >= 3000)
            {
                GameManager.Instance.MoveToEndingScene();
            }
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


