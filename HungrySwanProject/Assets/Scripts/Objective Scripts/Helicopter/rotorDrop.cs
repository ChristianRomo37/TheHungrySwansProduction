using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotorDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer mest;
    [SerializeField] MeshRenderer blade1;
    [SerializeField] MeshRenderer blade2;

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
        if (other.CompareTag("Player") && gameManager.instance.rotor)
        {
            gameManager.instance.promptRotorOn();
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptRotorOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.rotor = false;
            gameManager.instance.hasPart = false;
            mest.enabled = true;
            blade1.enabled = true;
            blade2.enabled = true;
            gameManager.instance.holdingRotor.SetActive(false);
            gameManager.instance.promptRotorOff();
            //gameManager.instance.playerScript.updateUI();
        }
    }
}
