using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topping : MonoBehaviour
{
    [SerializeField]
    protected RToppingIndex topping;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            SaveManager.Instance.NumberOfRTopping[(int)topping] += 1;
            Destroy(gameObject);
        }
    }
}
