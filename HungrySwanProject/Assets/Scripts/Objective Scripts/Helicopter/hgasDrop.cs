using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hgasDrop : MonoBehaviour
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
        if (other.CompareTag("Player") && gameManager.instance.hgas)
        {
            playerInRange = true;
            gameManager.instance.promptGasOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !gameManager.instance.hgas)
        {
            playerInRange = false;
            gameManager.instance.promptGasOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.hgas = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.holdingGas.SetActive(false);
            gameManager.instance.update2ndGameGoal(-1);
            gameManager.instance.promptGasOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
