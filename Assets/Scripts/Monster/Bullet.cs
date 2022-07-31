using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject host;
    public float dmg;
    public float duration;

    void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.GetDamage(dmg);
            Destroy(gameObject);
        }
    }


    public void ChangeSize(int size)
    {
        transform.localScale = new Vector3(size, size, transform.localScale.z);
    }
}
