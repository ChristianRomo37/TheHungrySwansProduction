using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasPickup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    public void interact(bool canInteract)
    {
        gameManager.instance.updateGameGoal(-1);
        gameManager.instance.gas = true;
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
