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
        if (other.CompareTag("Player") && gameManager.instance.blade)
        {
            gameManager.instance.promptGasOn();
            playerInRange = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptGasOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.hgas = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.holdingHGas.SetActive(false);
            gameManager.instance.promptGasOff();
            //gameManager.instance.playerScript.updateUI();
        }
    }
}
