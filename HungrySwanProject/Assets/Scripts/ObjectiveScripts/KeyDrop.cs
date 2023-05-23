using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.hasKey)
        {
            gameManager.instance.promptKeyOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptKeyOff();
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
            gameManager.instance.hasKey = false;
            gameManager.instance.carPartsInserted++;
        }
    }
}
