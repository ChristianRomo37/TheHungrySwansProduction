using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    int hp;
    [SerializeField] int timer;
    bool onFire;
    int ticks;
    public int dmg;
    public int interval;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ticks == 0)
        {
            onFire = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        onFire = true;
        ticks = 5;
        IDamage dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            for (int i = 0; i < ticks; i++)
            {
                StartCoroutine(tickDam());
                dam.takeDamage(dmg);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onFire = false;
    }

    IEnumerator tickDam()
    {
        yield return new WaitForSeconds(interval);
    }
}
