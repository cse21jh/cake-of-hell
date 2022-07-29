using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    private int itemCode;

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
        if (other.gameObject.tag == "Player")
        {
            Util.AddItem(itemCode);
            SoundManager.Instance.PlayEffect("GetItem");
            Destroy(gameObject);
        }
    }

    public void SetItemCode(int code)
    {
        itemCode = code;
    }
}
