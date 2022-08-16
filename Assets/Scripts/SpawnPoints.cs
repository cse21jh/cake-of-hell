using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [SerializeField]
    private string rank;

    private List<Monster> monsterInMap = new List<Monster>();

    private List<GameObject> spawnPoint = new List<GameObject>();

    void Awake()
    {
        switch(rank)
        {
            case "C":
                monsterInMap = GameManager.Instance.monsterInMapC;
                break;
            case "B":
                monsterInMap = GameManager.Instance.monsterInMapB;
                break;
            case "A":
                monsterInMap = GameManager.Instance.monsterInMapA;
                break;
            case "S":
                monsterInMap = GameManager.Instance.monsterInMapS;
                break;
            case "SS":
                monsterInMap = GameManager.Instance.monsterInMapSS;
                break;

        }

        for(int i=0;i<transform.childCount;i++)
        {
            spawnPoint.Add(transform.GetChild(i).gameObject);
            SpriteRenderer sr = spawnPoint[i].GetComponent<SpriteRenderer>();
            Color c = sr.material.color;
            c.a = 0;
            sr.material.color = c;
        }
    }
    
    void Start()
    {
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            Monster monster = Instantiate(monsterInMap[Random.Range(0, monsterInMap.Count)]);
            monster.transform.position = spawnPoint[i].transform.position;
        }
    }
}
