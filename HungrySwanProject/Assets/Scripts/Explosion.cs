using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour, IDamage
{
    [SerializeField] int Hp;
    [SerializeField] int dmg;
    [SerializeField] float radius;

    [SerializeField] GameObject explosion;

    bool dead = false;

    //[SerializeField] LayerMask mask;

    public void takeDamage(int damage)
    {
        Hp -= damage;

        if (Hp <= 0 && dead == false)
        {
            dead = true;
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                ExplosionDamage(transform.position, radius);
            }
            Destroy(gameObject);
        }

    }
    public void ExplosionDamage(Vector3 center, float radius)
    {
        int maxColliders = 10;
        Collider[] hitColliders = new Collider[maxColliders];
        int numColliders = Physics.OverlapSphereNonAlloc(center, radius, hitColliders);
        for (int i = 0; i < numColliders; i++)
        {
            if (hitColliders[i] != null)
            {
                IDamage damageable = hitColliders[i].gameObject.GetComponent<IDamage>();
                if (damageable != null)
                {
                    damageable.takeDamage(dmg);
                }
            }
        }
    }


    //public void OnTriggerEnter(Collider other)
    //{
    //    IDamage damage = other.GetComponent<IDamage>();

    //    if (damage != null)
    //    {
    //        damage.takeDamage(dmg);
    //    }
    //}

}
