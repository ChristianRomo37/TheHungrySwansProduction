using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skidDrop : MonoBehaviour
{
    [SerializeField] MeshRenderer bottom;
    [SerializeField] MeshRenderer leg1;
    [SerializeField] MeshRenderer leg2;
    [SerializeField] GameObject stand;

    bool playerInRange;


    void Start()
    {

    }

    void Update()
    {
        if (playerInRange && gameManager.instance.skid)
        {
            interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.skid)
        {
            playerInRange = true;
            gameManager.instance.promptSkidOn();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptSkidOff();
        }
    }

    void interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            gameManager.instance.skid = false;
            gameManager.instance.hasPart = false;
            bottom.enabled = true;
            leg1.enabled = true;
            leg2.enabled = true;
            Destroy(stand);
            gameManager.instance.holdingSkid.SetActive(false);
            gameManager.instance.update2ndGameGoal(-1);
            gameManager.instance.promptSkidOff();
            gameManager.instance.playerScript.updateUI();
        }
    }
}
