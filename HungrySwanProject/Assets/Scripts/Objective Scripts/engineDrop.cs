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
            gameManager.instance.promptEngineOn();
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptEngineOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.engine = false;
            gameManager.instance.hasPart = false;
            gameManager.instance.holdingEngine.SetActive(false);
            gameManager.instance.promptEngineOff();
        }
    }
}
