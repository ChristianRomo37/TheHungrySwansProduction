using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
    [SerializeField] gunStats gun;
    MeshFilter model;
    MeshRenderer mat;

    // Start is called before the first frame update
    void Start()
    {
        model = gun.model.GetComponent<MeshFilter>();
        mat = gun.model.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.gunPickup(gun);
            Destroy(gameObject);
        }
    }
}
