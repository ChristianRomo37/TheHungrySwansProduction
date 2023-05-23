using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireDrop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptTireOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptTireOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
