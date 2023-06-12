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
        IDamage dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            while (timer > 0)
            {
                dam.takeDamage(damage);
                StartCoroutine(poisonDuration());
                timer--;
            }
        }

    }

    IEnumerator poisonDuration()
    {
        yield return new WaitForSeconds(5);
    }
}
