using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddHP;
    int HP;
    int OrigHP;
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
            //interact(true);
            HP = gameManager.instance.playerScript.GetHP(HP);
            OrigHP = gameManager.instance.playerScript.GetMaxHP(OrigHP);
            if (HP < OrigHP)
            {
                gameManager.instance.playerScript.SetHP(AddHP);
                sound.Play();
                Destroy(gameObject);
            }
        }
    }
}
