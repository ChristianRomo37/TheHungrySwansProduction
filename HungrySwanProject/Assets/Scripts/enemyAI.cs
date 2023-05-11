using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;

    [Header("-----Enemy Stats-----")]
    [SerializeField] int HP;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;
    [SerializeField] Transform headPos;

    [Header("-----Enemy Weapon-----")]
    [Range(1, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3f)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [SerializeField] int shootAngle; 
    [SerializeField] GameObject bullet;
     

    Vector3 playerDir;
    bool isShooting;
    Color colorOrg;
    bool playerInRange;
    float angleToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        colorOrg = model.material.color;
        //gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && canSeePlayer())
        {
            
        }

    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(new Vector3(playerDir.x, 0, playerDir.z), transform.forward);

        Debug.DrawRay(transform.position, playerDir);
        Debug.Log(angleToPlayer);

        RaycastHit hit;
        if (Physics.Raycast(headPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone)
            {
                    agent.SetDestination(gameManager.instance.player.transform.position);

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    facePlayer();
                }
                if (!isShooting && angleToPlayer <= shootAngle)
                {
                    StartCoroutine(shoot());
                }
                return true;
            }
        }
        return false;
    }

    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    IEnumerator shoot()
    {
        isShooting = true;

        Instantiate(bullet, shootPos.position, transform.rotation);

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    public void takeDamage(int damage)
    {
        HP -= damage;
        StartCoroutine(flashColor());

        agent.SetDestination(gameManager.instance.player.transform.position);

        playerInRange = true;

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrg;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
