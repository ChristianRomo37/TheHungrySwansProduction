using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineDrop : MonoBehaviour
{
    bool playerInRange;

    void Start()
    {

    }

    void Update()
    {
        if (playerInRange)
        {
            interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.engine)
        {
            playerInRange = true;
            gameManager.instance.promptEngineOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !gameManager.instance.engine)
        {
            playerInRange = false;
            gameManager.instance.promptEngineOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.engine = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.playerScript.holdingEngine.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptEngineOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
