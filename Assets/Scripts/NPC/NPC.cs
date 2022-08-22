using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPC : MonoBehaviour
{
    private bool isInteracting = false;
    private bool canInteract = false;

    void Start()
    {

    }

    void Update()
    {
        if(canInteract && Input.GetKeyDown(KeyCode.G)) 
        {
            if(isInteracting)
            {
                isInteracting = false;
                EndInteract();
            }
            else 
            {
                isInteracting = true;
                StartInteract();
            }
        }
    }

    public abstract void StartInteract();
    public abstract void EndInteract();

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") 
        {
            canInteract = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.tag == "Player") 
        {
            canInteract = false;
        }
    }
}
