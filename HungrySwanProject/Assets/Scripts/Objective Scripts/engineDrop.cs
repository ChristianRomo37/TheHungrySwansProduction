using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class engineDrop : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.engine)
        {
            gameManager.instance.promptEngineOn();
            interact();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptEngineOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.engine = false;
        }
    }
}
