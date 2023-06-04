using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour, IDamage
{
    [SerializeField] int Hp;

    [SerializeField] GameObject explosion;
    [Range(1, 500)] public int explDamage;

    public void takeDamage(int damage)
    {
        Hp -= damage;


        if (Hp <= 0)
        {
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                gameManager.instance.playerScript.SetHP(Hp - 5);
            }
            Destroy(gameObject);
        }

    }
}
