using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddHP;
    int HP = 0;
    int OrigHP = 0;
    [SerializeField] AudioSource sound;

    //void Start()
    //{

    //}

    public void interact(bool canInteract)
    {
        gameManager.instance.playerScript.GetHP(HP);
        gameManager.instance.playerScript.GetMaxHP(OrigHP);
        if (HP < OrigHP)
        {
            gameManager.instance.playerScript.SetHP(AddHP);
            sound.Play();
            Destroy(gameObject);
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
