using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] ParticleSystem playerOnFire;
    [SerializeField] public int timer;
    [SerializeField] int damage;
    [SerializeField] float interval;
    public int ticks;
    //bool onFire;
    IDamage dam;

    // Start is called before the first frame update
    void Start()
    {
        ticks = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.restart == true)
        {
            ticks = 0;
            timer = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //onFire = true;
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
        if (other.CompareTag("Player"))
        {
            playerOnFire.gameObject.SetActive(true);
        }
        //onFire = false;
    }

    IEnumerator TakeFireDMG()
    {
        while (ticks <= timer)
        {
            dam.takeDamage(damage);
            yield return new WaitForSeconds(interval);
            ticks++;
        }
    }
}
