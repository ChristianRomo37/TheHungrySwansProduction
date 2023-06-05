using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    public void interact(bool canInteract)
    {
        if (!gameManager.instance.hasPart)
        {
            Destroy(gameObject);
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.battery = true;
            gameManager.instance.hasPart = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }
}
