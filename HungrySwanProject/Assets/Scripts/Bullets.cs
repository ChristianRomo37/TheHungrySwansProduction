using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [SerializeField] public int damage;
    [SerializeField] int speed;
    [SerializeField] int timer;

    [SerializeField] Rigidbody rb;

    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        direction = (player.position - transform.position).normalized;

        Destroy(gameObject, timer);
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamage damageable = other.GetComponent<IDamage>();
        //
        if (damageable != null)
        {
            if (other.CompareTag("Head"))
            {
                damageable.takeDamage(damage * 2);
            }
            else
            {
                damageable.takeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
