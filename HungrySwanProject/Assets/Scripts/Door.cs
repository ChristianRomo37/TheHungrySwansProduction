using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Renderer model;
    [SerializeField] Collider col;
    
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
}
