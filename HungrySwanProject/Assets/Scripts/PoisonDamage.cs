using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem poisonEffect;
    [SerializeField] int timer;
    [SerializeField] int damage;
    bool poisoned;
    IDamage dam;

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
        dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            for (int i = 0; i < timer;)
            {
                StartCoroutine(poisonDuration());
                i++;
            }
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
            //StartCoroutine(poisonDuration());
        }
    }

    IEnumerator poisonDuration()
    {
        dam.takeDamage(damage);
        yield return new WaitForSeconds(10f);
    }
}
