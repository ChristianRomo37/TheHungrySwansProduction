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
        if (playerInRange && gameManager.instance.battery)
        {
            interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            gameManager.instance.promptBatteryOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
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
            gameManager.instance.holdingBattery.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptBatteryOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
