using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer innerTire;
    [SerializeField] MeshRenderer outerTire;
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
        if (other.CompareTag("Player") && gameManager.instance.tire)
        {
            playerInRange = true;
            gameManager.instance.promptTireOn();            
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || !gameManager.instance.tire)
        {
            playerInRange = false;
            gameManager.instance.promptTireOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.tire = false;
            gameManager.instance.hasPart = false;
            innerTire.enabled = true;
            outerTire.enabled = true;
            gameManager.instance.playerScript.holdingTire.SetActive(false);
            gameManager.instance.carPartsPlaced++;
            gameManager.instance.updateGameGoal(-1);
            gameManager.instance.promptTireOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
