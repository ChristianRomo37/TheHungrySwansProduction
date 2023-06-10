using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

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
        if (gameManager.instance.playerScript.gunList.Count > 0)
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
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Rigidbody rb = other.GetComponent<Rigidbody>();
    //    IDamage dam = other.GetComponent<IDamage>();

    //    if (dam != null)
    //    {
    //        dam.takeDamage(dmg);
    //        rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        IDamage dam = collision.gameObject.GetComponent<IDamage>();
        //SphereCollider sphere = collision.gameObject.GetComponentInChildren<SphereCollider>();

        if (dam != null)
        {
            dam.takeDamage(dmg);
            rb.AddForce(Vector3.forward * 100, ForceMode.Impulse);
        }
    }
}
