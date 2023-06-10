using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer blade;
    [SerializeField] GameObject ladder;

    bool playerInRange;


    void Start()
    {

    }

    void Update()
    {
        if (playerInRange)
        {
            interact();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.blade)
        {
            gameManager.instance.promptBladeOn();
            playerInRange = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.promptBladeOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.blade = false;
            gameManager.instance.hasPart = false;
            blade.enabled = true;
            Destroy(ladder);
            gameManager.instance.holdingBlade.SetActive(false);
            gameManager.instance.promptBladeOff();
            //gameManager.instance.playerScript.updateUI();
        }
    }
}
