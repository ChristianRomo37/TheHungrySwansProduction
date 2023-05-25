using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class gunStats : ScriptableObject
{
    [Header("-----Weapon Stats-----")]
    [Range(2, 300)]public int shootDist;
    [Range(0.1f, 50f)]public float shootRate;
    [Range(1, 500)]public int shootDamage;
    [Range(1, 500)]public int magSize;
    [Range(1, 500)]public float reloadTime;
    [Range(1, 500)]public int shotsFired;
    [Range(1, 500)]public int OrigtotalBulletCount;
    [Range(1, 500)]public int totalBulletCount;
    [Range(0, 500)]public int bulletsRemaining;
    public GameObject model;
    public AudioClip gunShotAud;
    public AudioClip gunPickupAud;
    public AudioClip gunReloadAud;
    public AudioClip gunNoAmmoAud;
    [Range(0, 1)] public float gunShotAudVol;
    [Range(0, 1)] public float gunPickupAudVol;
    [Range(0, 1)] public float gunReloadAudVol;
    [Range(0, 1)] public float gunNoAmmoAudVol;
    public GameObject hitEffect;
    //public GameObject muzzleFlash;
    public bool sniper;
    public bool pistol;
    public bool rifle;
}
