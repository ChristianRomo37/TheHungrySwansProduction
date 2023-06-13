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
    [SerializeField] Transform DMshootPos;
    [SerializeField] Transform VshootPos;
    [SerializeField] Transform headPos;
    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem necroEffect;

    [Header("-----Enemy Stats-----")]
    [SerializeField] public int HP;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;
    [SerializeField] float stunTime;
    [SerializeField] int runAwayDist;
    [SerializeField] float animTransSpeed; //Anim

    [Header("-----Enemy Weapon-----")]
    [Range(1, 300)][SerializeField] int shootDist;
    [Range(0.1f, 10f)] [SerializeField] float shootRate;
    [Range(0.1f, 10f)] [SerializeField] float specialBulletDuration;
    [SerializeField] int shootAngle;
    [SerializeField] GameObject specialbullet;
    [SerializeField] GameObject normBullet;

    [Header("-----Necromancy-----")]
    [SerializeField] GameObject[] ObjectToSpawn;
    [SerializeField] Transform SpawnRange;
    [SerializeField] int MaxMinions;
    [SerializeField] float timeBetweenSpawns;

    public UIElements uI;
    bool playerInRange;
    Color colorOrg;
    public int HPOrig;
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
    static public bool bossMinion;
    float speed; //Anim
    bool summon;
    bool isSpawning;

    // Start is called before the first frame update
    void Start()
    {
        uI = GetComponent<UIElements>();
        colorOrg = model.material.color;
        speedOrig = agent.speed;
        HPOrig = HP;
        dead = false;
        minionsAlive = 0;
        minionsSpawned = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (agent.isActiveAndEnabled && !stunned && !summon && playerInRange)
        {
            
            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed); //Anim
            anim.SetFloat("Speed", speed); //Anim

            canSeePlayer(); 
            
            if (doubleMinions)
            {
                MaxMinions = MaxMinions * 2;
            }

            if (minionsAlive == 0)
                summon = true;

            if (!isSpawning && minionsSpawned < MaxMinions)
            {
                StartCoroutine(spawnMinions());
                bossShoot = false;
                doubleMinions = false;
            }

            if (minionsAlive == 0)
            {
                minionsSpawned = minionsAlive;
                doubleMinions = true;
            }

            StartCoroutine(stun());

        }
    }

    IEnumerator spawnMinions()
    {
        isSpawning = true;
        //StopAllCoroutines();

        if (summon)
        {
            anim.SetTrigger("Summon");
            StartCoroutine(NecroGlow());

        }

        Vector3 spawnPos = GetRandomSpawnPos();
        Instantiate(ObjectToSpawn[Random.Range(0, ObjectToSpawn.Length)], spawnPos, transform.rotation);
        minionsSpawned++;
        minionsAlive++;
        bossMinion = true;
        summon = false;
        enemyAI.spawning = true;
        spitterZombie.spawning = true;

        yield return new WaitForSeconds(timeBetweenSpawns);

        isSpawning = false;

    }

    IEnumerator NecroGlow()
    {
        necroEffect.gameObject.SetActive(true);
        necroEffect.Play();
        

        yield return new WaitForSeconds(3);

        necroEffect.Stop();
        necroEffect.gameObject.SetActive(false);
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
            if (hit.collider.CompareTag("Player") && angleToPlayer <= viewCone && !summon)
            {               

                agent.SetDestination(gameManager.instance.player.transform.position);

                float stoppingDistRA = agent.stoppingDistance - 10;

                if (agent.remainingDistance < stoppingDistRA)
                {
                    StartCoroutine(runAway());
                }

                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    facePlayer();
                }
                if (!isShooting && angleToPlayer <= shootAngle)
                {
                    if (!necroEffect.gameObject.activeSelf)
                    {
                        StartCoroutine(shootNorm());
                        StartCoroutine(shootSpecial());
                    }
                }
                return true;
            }
        }
        return false;
    }

    IEnumerator runAway()
    {
        Vector3 raPosition = transform.position - (playerDir * runAwayDist);

        agent.SetDestination(raPosition);

        float waitTime = Vector3.Distance(transform.position, raPosition) / agent.speed;

        yield return new WaitForSeconds(waitTime);

        facePlayer();
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

            anim.SetTrigger("Throw");

            specialShoot = true;

            yield return new WaitForSeconds(specialBulletDuration);

            isShooting = false;
            specialShoot = false;
        }
    }

    IEnumerator shootNorm()
    {
        if (!specialShoot && !summon)
        {
            isShooting = true;

            anim.SetTrigger("Throw");

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        }
    }

    public void createBullet()
    {
        if (specialShoot)
        {
            DMshootPos.gameObject.SetActive(false);
            VshootPos.gameObject.SetActive(true);

            Instantiate(specialbullet, VshootPos.position, transform.rotation);

            VshootPos.gameObject.SetActive(false);
            DMshootPos.gameObject.SetActive(true);
        }
        else
        {
            Instantiate(normBullet, DMshootPos.position, transform.rotation);
        }
    }

    public void takeDamage(int damage)
    {
        if (stunned)
        {
            damageGlob = damage;
            HP -= damage;
            uI.BossHealth();
            StartCoroutine(flashColor());
        }

        agent.SetDestination(gameManager.instance.player.transform.position);
        playerInRange = true;

        
        
        if (HP <= 0)
        {
            if (stunned)
            {
                StopCoroutine(stun());
                anim.SetBool("Stunned", false);
                StartCoroutine(deadAI());
            }
            else
            {
                StartCoroutine(deadAI());
            }
        }

        
    }
    
    IEnumerator stun()
    {
        if (minionsAlive == 0 && HP != 0)
        {
            stunned = true;
            agent.speed = 0;
            anim.SetBool("Stunned", true);

            yield return new WaitForSeconds(stunTime);

            agent.speed = speedOrig;
            anim.SetBool("Stunned", false);
            stunned = false;
            summon = false;
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
        anim.SetBool("Dead", true);
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
