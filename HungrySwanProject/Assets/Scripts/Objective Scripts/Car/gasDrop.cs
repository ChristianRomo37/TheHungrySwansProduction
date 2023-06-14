using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasDrop : MonoBehaviour
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
        if (other.CompareTag("Player") && gameManager.instance.gas)
        {
            playerInRange = true;
            gameManager.instance.promptGasOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !gameManager.instance.gas)
        {
            playerInRange = false;
            gameManager.instance.promptGasOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.gas = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.playerScript.holdingGas.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptGasOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
