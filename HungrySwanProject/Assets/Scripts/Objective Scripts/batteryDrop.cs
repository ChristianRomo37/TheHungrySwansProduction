using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteryDrop : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.battery)
        {
            gameManager.instance.promptBatteryOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptBatteryOff();
        }
    }

    void pickUp()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.battery = false;
        }
    }
}
