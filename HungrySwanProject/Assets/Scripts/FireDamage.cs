using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    int hp;
    [SerializeField] int timer;
    bool onFire;
    int ticks;

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
        if (onFire)
        {
            StartCoroutine(lastingFire());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        onFire = false;
    }

    IEnumerator lastingFire()
    {
        for (int i = 0; i < ticks; i++)
        {
            gameManager.instance.playerScript.SetHP(hp - 1);
            yield return new WaitForSeconds(timer);
        }
    }
}
