using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] Collider col;
    [SerializeField] int Hp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeDoorState(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            changeDoorState(true);
        }
    }

    void changeDoorState(bool state)
    {
        model.enabled = state;
        col.enabled = state;
    }
    public void takeDamage(int damage)
    {
        Hp -= damage;


        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
