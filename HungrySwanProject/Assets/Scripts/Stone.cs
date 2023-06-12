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
            StartCoroutine(Dust());
            countdown = interval;
        }
    }

    IEnumerator Dust()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        yield return new WaitForSeconds(decay);
        Destroy(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        IDamage damage = other.GetComponent<IDamage>();
        if (damage != null && other.CompareTag("Player"))
        {
            damage.takeDamage(2);
        }
    }
}
