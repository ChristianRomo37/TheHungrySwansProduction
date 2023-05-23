using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour, IDamage
{
    [SerializeField] int Hp;

    [Header("-----Audio-----")]
    [SerializeField][Range(0, 1)] float audExplosion;
    [SerializeField] AudioClip[] explosiionAud;
    [SerializeField] AudioSource audioSource;

    void Start()
    {

    }
    void Update()
    {

    }

    public void takeDamage(int damage)
    {
        Hp -= damage;


        if (Hp <= 0)
        {
            audioSource.PlayOneShot(explosiionAud[Random.Range(0, explosiionAud.Length)], audExplosion);
            Destroy(gameObject);
        }

    }
}
