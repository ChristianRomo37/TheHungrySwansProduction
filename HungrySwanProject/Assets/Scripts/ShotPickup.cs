using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotPickup : MonoBehaviour
{
    void Start()
    {

    }

    public void interact(bool canInteract)
    {
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
