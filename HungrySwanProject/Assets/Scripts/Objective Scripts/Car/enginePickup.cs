using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enginePickup : MonoBehaviour
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
            gameManager.instance.engine = true;
            gameManager.instance.hasPart = true;
            gameManager.instance.playerScript.holdingEngine.SetActive(true);
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
