using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem poisonEffect;
    [SerializeField] int timer;
    bool poisoned;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        poisoned = true;
        if (other.CompareTag("Player"))
        {
            StartCoroutine(poisonPlayer());
        }
        else if (other.CompareTag("Enemy"))
        {
            StopCoroutine(poisonEnemy());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        poisoned = false;
    }

    IEnumerator poisonPlayer()
    {
        for (int i = 0; i < timer; i++)
        {
            gameManager.instance.playerScript.SetHP(-1);
            yield return new WaitForSeconds(1);
        }
        poisoned = false;
    }

    IEnumerator poisonEnemy()
    {
        for (int i = 0; i < timer; i++)
        {
            gameManager.instance.enemyAIscript.takeDamage(-1);
            yield return new WaitForSeconds(1);
        }
        poisoned = false;
    }
}
