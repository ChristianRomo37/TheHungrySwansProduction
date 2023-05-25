using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    bool playerInRange;
    //void Start()
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.carPartsRemaining <= 0)
        {
            playerInRange = true;
            gameManager.instance.promptCarOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptCarOff();
        }
    }

    void Update()
    {
        if (playerInRange == true)
        {
            StartCoroutine(leave());
        }
    }

    IEnumerator leave()
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
