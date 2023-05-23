using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.carPartsRemaining <= 0 && gameManager.instance.carPartsInserted == 5)
        {
            //gameManager.instance.promptCarOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.promptCarOff();
        }
    }

    void Update()
    {
        
    }

    void leave()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.left = true;
            gameManager.instance.updateGameGoal(0);
        }
    }
}
