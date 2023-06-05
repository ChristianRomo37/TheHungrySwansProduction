using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tirePickup : MonoBehaviour
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
            gameManager.instance.tire = true;
            gameManager.instance.hasPart = true;
            gameManager.instance.holdingTire.SetActive(true);
            gameManager.instance.updateGameGoal(-1);
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
