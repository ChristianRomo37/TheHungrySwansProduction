using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] float stunTime;
    [SerializeField] int runAwayDist;

    [Header("-----Enemy Weapon-----")]
    [Range(1, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3f)][SerializeField] float shootRate;
    [SerializeField] int shootAngle;
    [SerializeField] GameObject specialbullet;
    [SerializeField] GameObject normBullet;

    [Header("-----Necromancy-----")]
    [SerializeField] GameObject[] ObjectToSpawn;
    [SerializeField] Transform SpawnRange;
    [SerializeField] int MaxMinions;
    [SerializeField] float timeBetweenSpawns;

    bool playerInRange = false;
    Color colorOrg;
    private int HPOrig;
    Vector3 playerDir;
    float angleToPlayer;
    bool isShooting;
    float stoppingDistOrig;
    int minionsSpawned;
    static public int minionsAlive;
    bool stunned;
    int damageGlob;
    float speedOrig;
    static public bool bossShoot;
    static public bool doubleMinions;
    static public bool dead;
    static public bool specialShoot;

    // Start is called before the first frame update
    void Start()
    {
        colorOrg = model.material.color;
        speedOrig = agent.speed;
        HPOrig = HP;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled && !stunned)
        {
            canSeePlayer(); 
            
            if (doubleMinions)
            {
                MaxMinions = MaxMinions * 2;
            }

            if (minionsSpawned < MaxMinions)
            {
                StartCoroutine(spawnMinions());
                bossShoot = false;
                doubleMinions = false;
            }

            StartCoroutine(stun());

            if (minionsAlive == 0)
            {
                minionsSpawned = minionsAlive;
                doubleMinions = true;
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
                if (agent.remainingDistance > agent.stoppingDistance)
                {
                    agent.SetDestination(gameManager.instance.player.transform.position);
                }
                else if (agent.remainingDistance < agent.stoppingDistance - 10)
                {
                    agent.SetDestination(transform.position - (playerDir * runAwayDist));
                }

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    facePlayer();
                }
                if (!isShooting && angleToPlayer <= shootAngle)
                {
                    StartCoroutine(shootNorm()); 
                    StartCoroutine(shootSpecial());
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

    IEnumerator shootSpecial()
    {
        if (bossShoot)
        {
            isShooting = true;

            Instantiate(specialbullet, shootPos.position, transform.rotation);

            yield return new WaitForSeconds(5);

            isShooting = false;
            specialShoot = false;
        }
    }

    IEnumerator shootNorm()
    {
        if (!specialShoot)
        {
            isShooting = true;

            Instantiate(normBullet, shootPos.position, transform.rotation);

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        }
    }

    public void takeDamage(int damage)
    {
        if (stunned)
        {
            damageGlob = damage;
            HP -= damage;

            StartCoroutine(flashColor());
        }

        agent.SetDestination(gameManager.instance.player.transform.position);
        playerInRange = true;

        if (HP <= 0)
        {
            StartCoroutine(deadAI());
        }
    }
    
    IEnumerator stun()
    {
        if (minionsAlive == 0)
        {
            stunned = true;
            agent.speed = 0;

            yield return new WaitForSeconds(stunTime);

            agent.speed = speedOrig;
            stunned = false;
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
        dead = true;
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        gameManager.instance.activeMenu = gameManager.instance.winMenu;
        gameManager.instance.activeMenu.SetActive(true);
        gameManager.instance.pauseState();
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
