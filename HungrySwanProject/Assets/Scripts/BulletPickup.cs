using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddBullet;
    [SerializeField] AudioSource sound;

    //void Start()
    //{

    //}

    public void interact(bool canInteract)
    {
        gameManager.instance.playerScript.SetBullets(AddBullet);
        sound.Play();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerScript.gunList.Count > 0)
        {
            interact(true);
        }
    }
}
