using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrow : MonoBehaviour
{
    public float throwForce;
    [SerializeField] float countdown;
    [SerializeField]float interval;

    public GameObject stonePre;

    // Start is called before the first frame update
    void Start()
    {
        countdown = interval;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown < 0)
        {
            ThrowStone();
            countdown = interval;
        }
    }

    void ThrowStone()
    {
        GameObject stone = Instantiate(stonePre, transform.position, transform.rotation);
        Rigidbody rb = stone.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
