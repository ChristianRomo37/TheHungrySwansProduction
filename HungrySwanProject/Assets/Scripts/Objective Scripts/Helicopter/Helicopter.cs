using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{
    bool playerInRange;


    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.helicopterPartsRemaining <= 0)
        {
            playerInRange = true;
            gameManager.instance.promptLeaveOn();
            StartCoroutine(interact());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptLeaveOff();
        }
    }

    IEnumerator interact()
    {
        if (Input.GetButtonDown("Interact"))
        {
            playerInRange = false;
            yield return new WaitForSeconds(3);
            gameManager.instance.activeMenu = gameManager.instance.winMenu;
            gameManager.instance.activeMenu.SetActive(true);
            gameManager.instance.pauseState();
        }
    }
}
