using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.hasGas)
        {
            gameManager.instance.promptGasOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptGasOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void itemPlace()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.hasGas = false;
            gameManager.instance.carPartsInserted++;
        }
    }
}
