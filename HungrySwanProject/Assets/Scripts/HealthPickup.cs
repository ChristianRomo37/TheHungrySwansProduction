using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int AddHP;
    [SerializeField] GameObject audioeffect;
    int HP;
    int OrigHP;
    int counts;

    //void Start()
    //{
    //}

    //public void interact(bool canInteract)
    //{
    //    gameManager.instance.playerScript.GetHP(HP);
    //    gameManager.instance.playerScript.GetMaxHP(OrigHP);
    //    if (HP < OrigHP)
    //    {
    //        gameManager.instance.playerScript.SetHP(AddHP);
    //        audio.PlayOneShot(sound, soundvol);
    //        Destroy(gameObject);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //interact(true);
            HP = gameManager.instance.playerScript.GetHP(HP);
            OrigHP = gameManager.instance.playerScript.GetMaxHP(OrigHP);
            if (HP < OrigHP)
            {
                Instantiate(audioeffect, transform.position, Quaternion.identity);
                gameManager.instance.playerScript.SetHP(AddHP);
                Destroy(gameObject);
            }
        }
    }
}
