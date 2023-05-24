using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasDrop : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.gas)
        {
            gameManager.instance.promptGasOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptGasOff();
        }
    }

    void pickUp()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.gas = false;
        }
    }
}
