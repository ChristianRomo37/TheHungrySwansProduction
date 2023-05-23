using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddSpeed;
    [SerializeField] int Timer;

    bool hasMoreSpeed;

    void Start()
    {
        hasMoreSpeed = false;
    }

    public void interact(bool canInteract)
    {
        gameManager.instance.playerScript.SetSpeed(AddSpeed);
        Destroy(gameObject);
        hasMoreSpeed = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }
}
