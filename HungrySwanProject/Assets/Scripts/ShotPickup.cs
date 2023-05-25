using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddSpeed;
    [SerializeField] public float Timer;

    //void Start()
    //{

    //}

    public void interact(bool canInteract)
    {
        gameManager.instance.playerScript.SetSpeed(AddSpeed);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interact(true);
        }
    }
}
