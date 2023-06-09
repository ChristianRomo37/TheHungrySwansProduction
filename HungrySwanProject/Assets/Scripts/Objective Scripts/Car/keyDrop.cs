using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyDrop : MonoBehaviour
{
    bool playerInRange;

    void Start()
    {
        
    }

    void Update()
    {
        if (playerInRange && gameManager.instance.key)
        {
            interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.key)
        {
            playerInRange = true;
            gameManager.instance.promptKeyOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptKeyOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.key = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.holdingKey.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptKeyOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
