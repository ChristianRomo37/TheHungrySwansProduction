using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StoneThrow : MonoBehaviour
{
    [SerializeField] float throwForce;
    [SerializeField] float countdown;
    [SerializeField] float interval;

    public GameObject stonePre;
    public enemyAI enemy;
    public static int throwDist;

    // Start is called before the first frame update
    void Start()
    {
        countdown = interval;
        //enemy = GetComponentInChildren<enemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!enemyAI.dead)
        //{
            countdown -= Time.deltaTime;
            if (countdown < 0)
            {
                ThrowStone();
                //enemy.anim.SetBool("Throw", false);
                countdown = interval;
            }
        //}
    }

    void ThrowStone()
    {
        if (enemy.see && enemy.agent.remainingDistance > 8)
        {
            enemy.anim.SetTrigger("Throw");
            GameObject stone = Instantiate(stonePre, transform.position, transform.rotation);
            Rigidbody rb = stone.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
        }
    }
}
