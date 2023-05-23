using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnginePickup : MonoBehaviour, IInteractable
{
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    public void interact(bool canInteract) 
    {
        gameManager.instance.updateGameGoal(-1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }
}
