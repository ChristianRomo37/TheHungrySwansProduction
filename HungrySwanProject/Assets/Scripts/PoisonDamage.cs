using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem poisonEffect;
    [SerializeField] int timer;
    [SerializeField] int damage;
    [SerializeField] float interval;
    int ticks;
    bool poisoned;
    IDamage dam;

    void Start()
    {
        ticks = 0;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        poisoned = true;
        dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
        }
    }

    IEnumerator poisonDuration()
    {
        while (ticks <= timer)
        {
            dam.takeDamage(damage);
            yield return new WaitForSeconds(interval);
            ticks++;
        }
    }
}
