using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddBullet;

    void Start()
    {

    }

    public void interact(bool canInteract)
    {
        gameManager.instance.playerScript.SetBullets(AddBullet);
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
