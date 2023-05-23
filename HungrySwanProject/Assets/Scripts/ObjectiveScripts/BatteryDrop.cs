using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player") && gameManager.instance.hasBattery && !gameManager.instance.hasEngine)
       {
            //gameManager.instance.promptBatteryOn();
       }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.promptBatteryOff();
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
            gameManager.instance.hasBattery = false;
            gameManager.instance.carPartsInserted++;
        }
    }
}
