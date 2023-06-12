using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
{
    Animator anie;
    [SerializeField] int dmg;
    [SerializeField] BoxCollider box;
    [SerializeField] int pushAmount;

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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    IPhysics physics = collision.gameObject.GetComponent<IPhysics>();
    //    IDamage dam = collision.gameObject.GetComponent<IDamage>();

    //    if (dam != null)
    //    {
    //        dam.takeDamage(dmg);

    //    }
    //    if (physics != null)
    //    {
    //        Vector3 dir = collision.gameObject.transform.position - transform.position;
    //        physics.takePushBack(dir * pushAmount);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        IPhysics physics = other.GetComponent<IPhysics>();
        IDamage dam = other.GetComponent<IDamage>();

        if (dam != null)
        {
            dam.takeDamage(dmg);

        }
        if (physics != null)
        {
            Vector3 dir = other.transform.position - transform.position;
            physics.takePushBack(dir * pushAmount);
        }
    }
}
