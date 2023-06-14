using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gasPickup : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    public void interact(bool canInteract)
    {
        if (!gameManager.instance.hasPart)
        {
            Destroy(gameObject);
            gameManager.instance.gas = true;
            gameManager.instance.hasPart = true;
            gameManager.instance.playerScript.holdingGas.SetActive(true);
            gameManager.instance.playerScript.updateUI();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }
}
