using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonDamage : MonoBehaviour
{
    //[SerializeField] ParticleSystem poisonEffect;
    [SerializeField] int timer;
    int ticks;
    bool poisoned;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ticks == 0)
        {
            poisoned = false;
        }
        //if (poisoned == true)
        //{
        //    poisonEffect.Play(true);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        poisoned = true;
        ticks = 5;
        StartCoroutine(poisonDuration());
    }

    private void OnTriggerExit(Collider other)
    {
        poisoned = false;
    }

    IEnumerator poisonDuration()
    {
        for (int i = 0; i < ticks; i++)
        {
            gameManager.instance.playerScript.SetHP(-1);
            yield return new WaitForSeconds(timer);
        }
    }
}
