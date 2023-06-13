using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem playerOnFire;
    [SerializeField] int timer;
    [SerializeField] int damage;
    [SerializeField] float interval;
    int ticks;
    bool onFire;
    IDamage dam;

    // Start is called before the first frame update
    void Start()
    {
        ticks = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        onFire = true;
        dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            if (other.CompareTag("Player"))
            {
                playerOnFire.gameObject.SetActive(true);
            }
            StartCoroutine(TakeFireDMG());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onFire = false;
    }

    IEnumerator TakeFireDMG()
    {
        while (ticks <= timer)
        {
            dam.takeDamage(damage);
            yield return new WaitForSeconds(interval);
            ticks++;
        }
        playerOnFire.gameObject.SetActive(false);
    }
}
