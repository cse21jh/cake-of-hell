using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player player;

    public bool canMove = true;

    public Item inputBase = null;
    public Item inputIcing = null;
    public Item inputTopping = null;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
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
    public int MakeCake()
    {
        /*
        CakeIndex cakeIndex = ItemManager.Instance.ReturnCake(inputBase, inputIcing, inputTopping);
        if (cakeIndex != CakeIndex.Null)
        {
            SaveManager.Instance.NumberOfBase[(int)inputBase] -= 1;
            SaveManager.Instance.NumberOfIcing[(int)inputIcing] -= 1;
            SaveManager.Instance.NumberOfTopping[(int)inputTopping] -= 1;

            inputBase = BaseIndex.Null;
            inputIcing = IcingIndex.Null;
            inputTopping = ToppingIndex.Null;
        }
        Debug.Log(cakeIndex.ToString());
        */
        return 1;
    }
    
}
