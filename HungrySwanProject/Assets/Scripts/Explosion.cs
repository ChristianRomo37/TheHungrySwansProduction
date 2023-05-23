using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explosion : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;


    [Header("-----Audio-----")]
    [SerializeField] AudioClip[] audExplosion;
    [SerializeField][Range(0, 1)] float audExplosionVol;


    public void explosion()
    {
        audioSource.PlayOneShot(audExplosion[Random.Range(0, audExplosion.Length)], audExplosionVol);
    }

}
