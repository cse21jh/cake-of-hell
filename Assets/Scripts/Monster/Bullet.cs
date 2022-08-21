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
        transform.localScale = new Vector3(transform.localScale.x*size, transform.localScale.y*size, transform.localScale.z);
    }

    public IEnumerator ShootBullet(Vector3 playerPos, int size = 1)
    {
        AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
        Vector3 startPosition = transform.position;
        for (float t = 0; t <= duration-0.1f; t += Time.deltaTime)
        {
            transform.position =
                Vector3.Lerp(startPosition, playerPos, curve.Evaluate(t / duration));
            yield return null;
        }
        transform.position = playerPos;
        ChangeSize(size);
        if(size!=1)
        {
            PolygonCollider2D polygonCollider = gameObject.GetComponent<PolygonCollider2D>();
            Destroy(polygonCollider);
            gameObject.AddComponent<PolygonCollider2D>();
            gameObject.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
        
        yield return null;
    }

}
