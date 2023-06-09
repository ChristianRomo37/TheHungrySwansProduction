using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletPickup : MonoBehaviour, IInteractable
{
    [SerializeField] int AddBullet;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip[] clip;
    [SerializeField][Range(0, 1)] float vol;

    //void Start()
    //{

    //}

    public void interact(bool canInteract)
    {
        source.PlayOneShot(clip[Random.Range(0, clip.Length)], vol);
        gameManager.instance.playerScript.SetBullets(AddBullet);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.playerScript.gunList.Count > 0)
        {
            interact(true);
        }
    }
}
