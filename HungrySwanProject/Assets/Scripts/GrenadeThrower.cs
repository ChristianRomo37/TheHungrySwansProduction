using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40f;

    public GameObject grenadePre;

    public int thorws;

    public int thorwsMax;

    //Start is called before the first frame update
    void Start()
    {
        thorwsMax = 2;
        thorws = thorwsMax;
        gameManager.instance.grenadePrompt.text = thorws.ToString() + " / " + thorwsMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Grenade") && thorws > 0)
        {
            ThrowGrenade();
        }
    }

    void ThrowGrenade()
    {
        thorws--;
        GameObject grenade = Instantiate(grenadePre, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        gameManager.instance.grenadePrompt.text = thorws.ToString() + " / " + thorwsMax;
    }
}
