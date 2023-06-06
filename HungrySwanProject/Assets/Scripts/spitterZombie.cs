using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class spitterZombie : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] AudioSource audioSource;
    [SerializeField] Animator anim; //animation
    [SerializeField] Transform shootPos;
    [SerializeField] Transform headPos;
    [SerializeField] Collider headShot;

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
    [SerializeField] GameObject bullet;
    [SerializeField] float animTransSpeed; //animation

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
    Color colorOrg;
    bool playerInRange;
    private int HPOrig;
    Vector3 startingPos;
    bool destinatoinChosen;
    float stoppingDistOrig;
    bool stepIsPlaying;
    float speed; //animation
    int damageGlob;
    bool sink;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        HPOrig = HP;
        colorOrg = model.material.color;
        //spawnEnemys();
        //gameManager.instance.updateGameGoal(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            speed = Mathf.Lerp(speed, agent.velocity.normalized.magnitude, Time.deltaTime * animTransSpeed); //for animation ~ Colyn
            anim.SetFloat("Speed", speed); //animation

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

    IEnumerator roam()
    {
        if (!destinatoinChosen && agent.remainingDistance < 0.05f && agent.isActiveAndEnabled)
        {
            destinatoinChosen = true;
            agent.stoppingDistance = 0;
            yield return new WaitForSeconds(roamPauseTime);
            destinatoinChosen = false;

            Vector3 ranPos = Random.insideUnitSphere * roamDist;
            ranPos += startingPos;

            NavMeshHit hit;
            NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);

            playSteps();

            agent.SetDestination(hit.position);
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

            anim.SetTrigger("Shoot"); //animation
            audioSource.PlayOneShot(audAttack[Random.Range(0, audAttack.Length)], audAttackVol);

            Instantiate(bullet, shootPos.position, transform.rotation);

            yield return new WaitForSeconds(shootRate);

            isShooting = false;
        }
    }

    public void takeDamage(int damage)
    {
        HP -= damage;

        audioSource.PlayOneShot(audDamage[Random.Range(0, audDamage.Length)], audDamageVol);
        StartCoroutine(flashColor());

        agent.SetDestination(gameManager.instance.player.transform.position);

        playerInRange = true;

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

    public void takeHeadshot()
    {
        if (headShot)
        {
            int damage = damageGlob * 2;
            takeDamage(damage);
        }
    }

    IEnumerator deadAI()
    {
        Boss.minionsAlive--;
        anim.SetBool("Dead", true); //animation
        agent.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        sink = true;
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
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
}
