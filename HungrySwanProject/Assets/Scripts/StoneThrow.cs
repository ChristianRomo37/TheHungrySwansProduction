using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrow : MonoBehaviour
{
    [SerializeField] float throwForce;
    [SerializeField] float countdown;
    [SerializeField] float interval;

    public GameObject stonePre;
    public enemyAI enemy;

    // Start is called before the first frame update
    void Start()
    {
        countdown = interval;
        //enemy = GetComponentInChildren<enemyAI>();
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
        if (enemy.see)
        {
            //enemy.facePlayer();
            //enemy.anim.SetTrigger("Melee");
            GameObject stone = Instantiate(stonePre, transform.position, transform.rotation);
            Rigidbody rb = stone.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
