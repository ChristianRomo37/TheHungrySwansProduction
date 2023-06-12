using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    //public float delay = 3f;
    //public float radius = 5;
    //public float effectduration;


    public GameObject explosionEffect;

    [SerializeField] float countdown;
    [SerializeField] float interval;
    [SerializeField] float decay;
    [SerializeField] int pushAmount;

    //bool hasExploded;
    // Start is called before the first frame update
    //void Start()
    //{
    //    countdown = interval;
    //}

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            Dust();
            countdown = interval;
        }
    }

    void Dust()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();
        IPhysics physics = other.GetComponent<IPhysics>();

        if (damage != null && other.CompareTag("Player"))
        {
            damage.takeDamage(2);
            Vector3 dir = other.transform.position - transform.position;
            physics.takePushBack(dir * pushAmount);
            countdown = -1;
        }
    }
}
