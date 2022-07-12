using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private Player player;
    private GameObject imageHit;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        imageHit = GameObject.Find("Canvas").transform.Find("ImageHit").gameObject;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void ChangeSpeed(float value)
    {
        player.Speed += value;
        SaveManager.Instance.Speed += value;
    }

    public void ChangeHp(float value)
    {
        player.Hp += value;
        SaveManager.Instance.Hp += value;
    }

    public void ChangeMaxHp(float value)
    {
        player.MaxHp += value;
        SaveManager.Instance.MaxHp += value;
    }

    public IEnumerator DamagedEffect()
    {
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        imageHit.SetActive(false);
    }

    public void GetDamage(float value)
    {
        StartCoroutine("DamagedEffect");
        player.Hp -= value;
        SaveManager.Instance.Hp -= value;
        if (player.Hp <= 0)
        {
            Debug.Log("Die");
            // 체력 0 되었을 때에 대한 내용 여기에 구현
        }
    }

}
