using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tireDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer innerTire;
    [SerializeField] MeshRenderer outerTire;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.tire)
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
            gameManager.instance.tire = false;
            innerTire.enabled = true;
            outerTire.enabled = true;
        }
    }
}
