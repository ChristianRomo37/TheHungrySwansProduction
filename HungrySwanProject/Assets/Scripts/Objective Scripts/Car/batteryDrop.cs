using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryDrop : MonoBehaviour
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
        if (other.CompareTag("Player") && gameManager.instance.battery)
        {
            playerInRange = true;
            gameManager.instance.promptBatteryOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !gameManager.instance.battery)
        {
            playerInRange = false;
            gameManager.instance.promptBatteryOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.battery = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.playerScript.holdingBattery.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptBatteryOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
