using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    Animator anie;
    [SerializeField] int dmg;
    [SerializeField] BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        anie = GetComponent<Animator>();
        box.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        melee();
    }

    public void melee()
    {
        if (Input.GetButtonDown("Melee"))
        {
            anie.SetBool("Bashing", true);
            box.enabled = true;
        }
        else if (Input.GetButtonUp("Melee"))
        {
            anie.SetBool("Bashing", false);
            box.enabled = false;
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    IDamage damagable = other.GetComponent<IDamage>();
    //    Rigidbody rb = other.GetComponent<Rigidbody>();

    //    if (damagable != null)
    //    {
    //        damagable.takeDamage(dmg);

    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Normal Zombie"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            IDamage dam = collision.gameObject.GetComponent<IDamage>();

            if (dam != null)
            {
                dam.takeDamage(dmg);
                //rb.AddForce();
            }
        }
    }
}
