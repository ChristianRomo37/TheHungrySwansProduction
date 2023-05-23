using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.hasEngine)
        {
            //gameManager.instance.promptEngineOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.promptEngineOff();
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
            gameManager.instance.hasEngine = false;
            gameManager.instance.carPartsInserted++;
        }
    }
}
