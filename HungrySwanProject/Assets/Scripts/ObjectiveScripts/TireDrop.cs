using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer outerTire;
    [SerializeField] MeshRenderer innerTire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.hasTire)
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

    void itemPlace()
    {
        if(Input.GetButtonDown("Interact"))
        {
            gameManager.instance.hasTire = false;
            innerTire.enabled = true;
            outerTire.enabled = true;
            gameManager.instance.carPartsInserted++;
        }
    }
}
