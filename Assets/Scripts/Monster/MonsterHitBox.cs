using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHitBox : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerManager.Instance.GetDamage(damage);
        }
    }

    public void ChangeSize(float size)
    {
        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, transform.localScale.z);
    }
}
