using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;

    [Header("-----Enemy Stats-----")]
    [SerializeField] int HP;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;

    [Header("-----Enemy Weapon-----")]
    [Range(1, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3f)][SerializeField] float shootRate;
    [SerializeField] int shootAngle;
    [SerializeField] GameObject bullet;

    [Header("-----Necromancy-----")]
    [SerializeField] GameObject[] ObjectToSpawn;
    [SerializeField] Transform SpawnRange;
    [SerializeField] int MaxMinions;
    [SerializeField] float timeBetweenSpawns;

    bool playerInRange;
    Color colorOrg;
    private int HPOrig;
    Vector3 playerDir;
    float angleToPlayer;
    bool isShooting;
    float stoppingDistOrig;
    int minionsSpawned;
    static public int minionsAlive;

    // Start is called before the first frame update
    void Start()
    {
        colorOrg = model.material.color;
        HPOrig = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            if (minionsAlive == 0)
            {
                minionsSpawned = minionsAlive;
            }

            if (minionsSpawned < MaxMinions)
            {
                StartCoroutine(spawnMinions());
            }
        }
    }

    IEnumerator spawnMinions()
    {
        Vector3 spawnPos = GetRandomSpawnPos();
        Instantiate(ObjectToSpawn[Random.Range(0, ObjectToSpawn.Length)], spawnPos, transform.rotation);
        minionsSpawned++;
        minionsAlive++;

        yield return new WaitForSeconds(timeBetweenSpawns);


    }

    Vector3 GetRandomSpawnPos()
    {
        Vector3 randomSpot = Random.insideUnitSphere * SpawnRange.localScale.x;
        Vector3 spawnPostion = SpawnRange.position + randomSpot;
        spawnPostion.y = 0f;
        return spawnPostion;
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
                agent.stoppingDistance = stoppingDistOrig;
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
        agent.stoppingDistance = 0;
        return false;
    }

    void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    IEnumerator shoot()
    {

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isShooting = true;

            Instantiate(bullet, shootPos.position, transform.rotation);

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        }
    }

    public void takeDamage(int damage)
    {
        HP -= damage;

        StartCoroutine(flashColor());

        agent.SetDestination(gameManager.instance.player.transform.position);

        playerInRange = true;

        if (HP <= 0)
        {
            StartCoroutine(deadAI());
        }
    }

    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrg;
    }

    IEnumerator deadAI()
    {
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        StartCoroutine(gameManager.instance.youWin());
    }
}
