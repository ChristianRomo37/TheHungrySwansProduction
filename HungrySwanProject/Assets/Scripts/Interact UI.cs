using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractUI : MonoBehaviour
{
    [Header("-----Player Stats-----")]
    [SerializeField] Transform headPos;
    [SerializeField] int viewCone;

    bool playerInRange;
    float angleToPlayer;
    Vector3 playerDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
            {
                ///display prompt
                

                return true;
            }
        }
        return false;
    }
}
