using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hgasPickup : MonoBehaviour
{
    void Start()
    {
        gameManager.instance.update2ndGameGoal(1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }

    public void interact(bool canInteract)
    {
        if (!gameManager.instance.hasPart)
        {
            Destroy(gameObject);
            gameManager.instance.hgas = true;
            gameManager.instance.hasPart = true;
            gameManager.instance.playerScript.holdingGas.SetActive(true);
            gameManager.instance.playerScript.updateUI();
        }
    }
}
