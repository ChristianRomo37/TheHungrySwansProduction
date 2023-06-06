using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    int hp;
    [SerializeField] float timer;
    bool onFire = false;
    int ticks;
    public int dmg;
    public int interval;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //if (ticks == 0)
        //{
        //    onFire = false;
        //}
        //timer -= Time.deltaTime;

        
    }

    private void OnTriggerEnter(Collider other)
    {
        onFire = true;
        ticks = 5;

        IDamage dam = other.GetComponent<IDamage>();
        if (dam != null)
        {
            StartCoroutine(wait());
            for (int i = 0; i < ticks; i++)
            {
                dam.takeDamage(dmg);
            }
        }

        //IFire fire = other.GetComponent<IFire>();
        //if (fire != null)
        //{
        //    fire.fireDame(dmg, ticks);
        //    fire.fireTimer(timer);
        //}

        //while (onFire)
        //{
        //    //StartCoroutine(fire.fireDame(dmg));
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        onFire = false;
    }

    IEnumerator wait()
    {
        ticks-=1;
        yield return new WaitForSeconds(timer);
    }
}
