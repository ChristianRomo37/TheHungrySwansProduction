using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour, IDamage
{
    [SerializeField] int Hp;

    public void takeDamage(int damage)
    {
        Hp -= damage;


        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
