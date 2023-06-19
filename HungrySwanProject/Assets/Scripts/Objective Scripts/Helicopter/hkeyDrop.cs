using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hkeyDrop : MonoBehaviour
{
    bool playerInRange;


    void Start()
    {

    }

    void Update()
    {
        if (playerInRange && gameManager.instance.hkey)
        {
            interact();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
            gameManager.instance.hkey = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.holdingKey.SetActive(false);
            gameManager.instance.update2ndGameGoal(-1);
            gameManager.instance.promptKeyOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
