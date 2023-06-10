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
        if (playerInRange)
        {
            interact();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.hkey)
        {
            gameManager.instance.promptKeyOn();
            playerInRange = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
            gameManager.instance.promptKeyOff();
            //gameManager.instance.playerScript.updateUI();
        }
    }
}
