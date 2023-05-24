using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class melee : MonoBehaviour
{
    [SerializeField] int damage;


    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();

        if (damageable != null)
        {
            damageable.takeDamage(damage);
        }

    }
}
