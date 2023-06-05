using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour, IDamage
{

    public void takeDamage(int damage)
    {
        Destroy(gameObject);
    }
}
