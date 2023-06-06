using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammobox : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    [Header("-----Audio-----")]
    [SerializeField] AudioClip[] Pickup;
    [SerializeField][Range(0, 1)] float PickupVol;

    bool playerInRange;
    public int counter;
    int ammo = 0;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            pickUp();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //gameManager.instance.playerScript.gunPickup(gun);
            playerInRange = true;
            gameManager.instance.promptEOnBox();
            //Destroy(gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptEOffBox();
        }
    }

    void pickUp()
    {
        if (playerInRange)
        {
            //Debug.Log("on");
            gameManager.instance.promptEOnBox();
            if (Input.GetButtonDown("Interact") && counter <= 3)
            {
                counter++;
                gameManager.instance.playerScript.SetAmmoCrate(ammo);
                audioSource.PlayOneShot(Pickup[Random.Range(0, Pickup.Length)], PickupVol);
                gameManager.instance.promptEOffBox();
                if (counter == 3)
                {
                    playerInRange = false;
                    Destroy(gameObject);
                }
            }
        }
    }
}
