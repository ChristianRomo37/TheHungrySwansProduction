using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    bool playerInRange;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.helicopterPartsRemaining <= 0)
        {
            gameManager.instance.promptLeaveOn();
            interact();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptLeaveOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.left2 = true;
            gameManager.instance.update2ndGameGoal(0);
        }
    }
}
