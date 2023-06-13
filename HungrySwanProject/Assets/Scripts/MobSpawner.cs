using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MobSpawner : MonoBehaviour, IDamage
{
    [SerializeField] GameObject[] ObjectToSpawn;
    [SerializeField] Transform SpawnRange;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] int MaxEnemies;
    [SerializeField] int HP;
    [SerializeField] Renderer model;

    int enemiesSpawned;
    bool isSpawning;
    Color colorOrg;
    private int HPOrig;
    bool isAlive = false;

    void Start()
    {
        HPOrig = HP;
        colorOrg = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isSpawning && enemiesSpawned < MaxEnemies)
        {
            StartCoroutine(spawn());
        }
    }

    IEnumerator spawn()
    {
        isSpawning = true;

        Vector3 spawnPos = GetRandomSpawnPos();
        Instantiate(ObjectToSpawn[Random.Range(0, ObjectToSpawn.Length)], spawnPos, transform.rotation);
        enemiesSpawned++;
        Boss.bossMinion = false;
        enemyAI.spawning = true;
        spitterZombie.spawning = true;

        yield return new WaitForSeconds(timeBetweenSpawns);

        isSpawning = false;
    }

    Vector3 GetRandomSpawnPos()
    {
        Vector3 randomSpot = Random.insideUnitSphere * SpawnRange.localScale.x;
        Vector3 spawnPostion = SpawnRange.position + randomSpot;
        spawnPostion.y = 0f;
        return spawnPostion;
    }

    public void takeDamage(int damage)
    {
        HP -= damage;

        StartCoroutine(flashColor());

        if (HP <= 0)
        {
            Destroy(gameObject);
            isAlive = false;
        }
    }
    IEnumerator flashColor()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrg;
    }
}
