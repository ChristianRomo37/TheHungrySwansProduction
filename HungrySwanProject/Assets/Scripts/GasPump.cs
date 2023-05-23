using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasPump : MonoBehaviour, IDamage
{
    [SerializeField] int Hp;

    [SerializeField] GameObject explosion;


    public void takeDamage(int damage)
    {
        Hp -= damage;


        if (Hp <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

    }
}
