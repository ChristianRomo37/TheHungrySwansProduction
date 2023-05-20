using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    [Header("-----Weapon Stats-----")]
    [Range(2, 300)]public int shootDist;
    [Range(0.1f, 3f)]public float shootRate;
    [Range(1, 10)]public int shootDamage;
    [Range(1, 10)]public int magSize;
    [Range(1, 10)]public float reloadTime;
    [Range(1, 10)]public int shotsFired;
    [Range(1, 500)]public int OrigtotalBulletCount;
    [Range(1, 500)]public int totalBulletCount;
    [Range(0, 500)]public int bulletsRemaining;
    public GameObject model;
    public AudioClip gunShotAud;
    [Range(0, 1)] public float gunShotAudVol;
    public GameObject hitEffect;
    public GameObject muzzleFlash;
}
