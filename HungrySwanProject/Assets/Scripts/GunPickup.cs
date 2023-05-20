using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class GunPickup : MonoBehaviour
{
    [SerializeField] gunStats gun;
    //[SerializeField] int pickUpTimer;
    //[SerializeField] Transform bodyPos;
    MeshFilter model;
    MeshRenderer mat;
    bool playerInRange;
    Vector3 playerDir;
    float angleToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        model = gun.model.GetComponent<MeshFilter>();
        mat = gun.model.GetComponent<MeshRenderer>();
        gun.bulletsRemaining = 0;
        gun.totalBulletCount = gun.OrigtotalBulletCount;
    }

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
            gameManager.instance.promptEOn();
            //Destroy(gameObject);
        }
    }

    //bool playerCanSee()
    //{
    //    playerDir = gameManager.instance.player.transform.position - bodyPos.position;
    //    angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

    //    Debug.DrawRay(transform.position, playerDir);
    //    Debug.Log(angleToPlayer);

    //    RaycastHit hit;
    //    if (Physics.Raycast(gameManager.instance.player.transform.position, transform.forward, out hit))
    //    {
    //        if (hit.collider.CompareTag("Ground Weapon"))
    //        {
    //            //agent.SetDestination(gameManager.instance.player.transform.position);
    //            //gameManager.instance.promptEOn();
    //            return true;
    //        }
    //    }
    //    return false;
    //}

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            gameManager.instance.promptEOff();
        }
    }

    void pickUp()
    {
        if (playerInRange)
        {
             //Debug.Log("on");
             //gameManager.instance.promptEOn();
             if (Input.GetButtonDown("Interact"))
             {
                 //yield return new WaitForSeconds(pickUpTimer);
                 gameManager.instance.playerScript.gunPickup(gun);
                 playerInRange = false;
                 gameManager.instance.promptEOff();
                 Destroy(gameObject);
             }
        }
        
    }
}
