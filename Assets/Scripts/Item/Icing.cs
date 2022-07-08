using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icing : MonoBehaviour
{
    [SerializeField]
    protected RIcingIndex icing;
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
            SaveManager.Instance.NumberOfRIcing[(int)icing] += 1;
            Destroy(gameObject);
        }
    }
}
