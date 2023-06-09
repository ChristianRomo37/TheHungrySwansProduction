using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hkeyPickup : MonoBehaviour
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
            gameManager.instance.hkey = true;
            gameManager.instance.hasPart = true;
            gameManager.instance.holdingKey.SetActive(true);
            gameManager.instance.playerScript.updateUI();
        }
    }
}
