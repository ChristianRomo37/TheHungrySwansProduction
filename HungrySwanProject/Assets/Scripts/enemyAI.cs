using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;



public class enemyAI : MonoBehaviour, IDamage, IPhysics
{
    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] AudioSource audioSource;
    [SerializeField] public Animator anim;  //Anim
    [SerializeField] Collider meleeCol; //melee
    [SerializeField] GameObject Head;
    [SerializeField] ParticleSystem spawnEffect;


    [Header("-----Enemy Stats-----")]
    [SerializeField] int HP;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int viewCone;
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;

    [Header("-----Enemy Weapon-----")]
    [Range(1, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3f)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [SerializeField] int shootAngle;
    [SerializeField] float animTransSpeed; //Anim

    [Header("-----Audio-----")]
    [SerializeField] AudioClip[] audDamage;
    [SerializeField] AudioClip[] audSteps;
    [SerializeField] AudioClip[] audAttack;
    [SerializeField] AudioClip[] audIdle;
    [SerializeField][Range(0, 1)] float audDamageVol;
    [SerializeField][Range(0, 1)] float audStepsVol;
    [SerializeField][Range(0, 1)] float audAttackVol;
    [SerializeField][Range(0, 1)] float audIdleVol;


    Vector3 playerDir;
    float angleToPlayer;
    bool isShooting;
    public bool see;
    Color colorOrg;
    bool playerInRange;
    private int HPOrig;
    Vector3 startingPos;
    bool destinatoinChosen;
    float stoppingDistOrig;
    bool stepIsPlaying = false;
    float speed; //Anim
    bool sink = false;
    float roamPauseTime2;
    int damageGlob;
    Collision headShot;
    bool takeHS;
    bool spanwed = true;
    bool dead;
    public static bool spawning;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        HPOrig = HP;
        colorOrg = model.material.color;
        if (Boss.bossMinion)
        {
            transform.Translate(-Vector3.up * 2f);
            agent.enabled = false;
        }
        //spawnEnemys();
        //gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawning && Boss.bossMinion)
        {
            StartCoroutine(spawnRise());
        }
        else
        {
            spawning = false;
        }

        if (agent.isActiveAndEnabled && !spawning)
        {
            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed); //Anim
            anim.SetFloat("Speed", speed); //Anim
            


            if (Boss.bossMinion)
            {
                zombieSpeak();
                agent.SetDestination(gameManager.instance.player.transform.position);
            }

            if (playerInRange && !canSeePlayer())
           {
               zombieSpeak();
               StartCoroutine(roam());
           }
           else if (agent.destination != gameManager.instance.player.transform.position)
           {
              zombieSpeak();
              StartCoroutine(roam());
           }
        }
        else
        {
            if (sink)
            {
                transform.Translate(-Vector3.up * 2f * Time.deltaTime);
            }
        }
    }

    IEnumerator spawnRise()
    {
        spawnEffect.gameObject.SetActive(true);
        spawnEffect.Play();
        transform.Translate(Vector3.up * 1.5f * Time.deltaTime);

        yield return new WaitForSeconds(2);


        agent.enabled = true;
        spawnEffect.Stop();
        spawnEffect.gameObject.SetActive(false);
        spawning = false;
    }

    IEnumerator roam()
    {
        if (!destinatoinChosen && agent.remainingDistance < 0.05f && agent.isActiveAndEnabled && !Boss.bossMinion)
        {
            destinatoinChosen = true;
            agent.stoppingDistance = 0;
            roamPauseTime2 = Random.Range(0f, 5f);
            yield return new WaitForSeconds(roamPauseTime);
            destinatoinChosen = false;

            Vector3 ranPos = Random.insideUnitSphere * roamDist;
            ranPos += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);

            playSteps();

            //if (!dead)
            agent.SetDestination(hit.position);
        }
    }

    public void takePushBack(Vector3 dir)
    {
        agent.velocity += dir;
    }

    bool canSeePlayer()
    {
        see = true;
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
                //playSteps();

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

    public void facePlayer()
    {
        Quaternion rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        Quaternion rotY = Quaternion.LookRotation(playerDir, Vector3.left);

        headPos.rotation = Quaternion.Lerp(headPos.rotation, rotY, Time.deltaTime * playerFaceSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * playerFaceSpeed);
    }

    IEnumerator shoot()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            isShooting = true;

            anim.SetTrigger("Melee"); //Anim
            audioSource.PlayOneShot(audAttack[Random.Range(0, audAttack.Length)], audAttackVol);

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        }
    }

    public void meleeColOn()
    {
        meleeCol.enabled = true;
    }

    public void meleeColOff()
    {
        meleeCol.enabled = false;
    }


    public void takeDamage(int damage)
    {
        //if (headShot.collider.gameObject == Head)
        //{
        //    damage *= 2;
        //}
        //anim.SetTrigger("Damage");

        if (agent.isActiveAndEnabled)
        {
            HP -= damage;
            Vector3 forceDirection = (Vector3.forward - gameManager.instance.player.transform.position).normalized;
            transform.position += forceDirection * Time.deltaTime * 1.0f;

            audioSource.PlayOneShot(audDamage[Random.Range(0, audDamage.Length)], audDamageVol);
            StartCoroutine(flashColor());

            if (!dead)
                agent.SetDestination(gameManager.instance.player.transform.position);

            playerInRange = true;
        }

        if (HP <= 0)
        {
            if (Boss.minionsAlive == 1)
            {
                StartCoroutine(deadAI());
            }
            else
            {
                StartCoroutine(minionDeath());
            }
        }
    }

    IEnumerator minionDeath()
    {
        Boss.bossShoot = true;

        StartCoroutine(deadAI());

        yield return new WaitForSeconds(2);

        Boss.bossShoot = false;
    }

    IEnumerator deadAI()
    {
        dead = true;
        anim.SetBool("Dead", true);
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        objectSpawn();
        sink = true;
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
        Boss.minionsAlive--;

        StopAllCoroutines();
    }

    public void objectSpawn()
    {
        int rand = Random.Range(0, 2);
        if (rand == 1)
        {
            int rand1 = Random.Range(0, 2);
            Instantiate(gameManager.instance.drops[rand1], transform.position, Quaternion.identity, null);
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
            agent.stoppingDistance = 0;
            see = false;
        }
    }

    //public void spawnEnemys()
    //{
    //    if (this.CompareTag("Normal Zombie"))
    //    {
    //        transform.position = gameManager.instance.NEnemySpawnPos.transform.position;
    //    }
    //    if (this.CompareTag("Spitter Zombie"))
    //    {
    //        transform.position = gameManager.instance.SEnemySpawnPos.transform.position;
    //    }
    //    if (this.CompareTag("Fast Zombie"))
    //    {
    //        transform.position = gameManager.instance.FEnemySpawnPos.transform.position;
    //    }
    //    if (this.CompareTag("Tank Zombie"))
    //    {
    //        transform.position = gameManager.instance.TEnemySpawnPos.transform.position;
    //    }
        
    //    HP = HPOrig;
    //}

    IEnumerator zombieSpeak()
    {
        // audioSource.PlayOneShot(audIdle[Random.Range(0, audIdle.Length)], audIdleVol);
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator playSteps()
    {
        stepIsPlaying = true;

        zombieSpeak();
        // audioSource.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);

        yield return new WaitForSeconds(0.3f);

        stepIsPlaying = false;
    }

    //public IEnumerator fireDame(int damage)
    //{
    //    yield return new WaitForSeconds(damage);
    //}

    //public IEnumerator fireTimer(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //}

    
}
