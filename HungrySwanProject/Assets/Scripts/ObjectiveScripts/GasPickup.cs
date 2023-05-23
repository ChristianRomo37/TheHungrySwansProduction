using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPickup : MonoBehaviour
{
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    public void interact(bool canInteract)
    {
        gameManager.instance.updateGameGoal(-1);
        gameManager.instance.hasBattery = true;
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
