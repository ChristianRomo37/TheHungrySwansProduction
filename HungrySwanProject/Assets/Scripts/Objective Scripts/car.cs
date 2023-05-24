using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.carPartsRemaining <= 0)
        {
            gameManager.instance.promptTireOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptTireOff();
        }
    }

    void pickUp()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.left = true;
        }
    }

}
