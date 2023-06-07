using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class playerControler : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource audioSource;
    camerControl cameracon;

    [Header("-----Player Stats-----")]
    [Range(1, 20)][SerializeField] public int HP;
    [Range(1, 10)][SerializeField] float playerSpeed;
    [Range(1, 10)][SerializeField] float sprintMod;
    [Range(1, 10)][SerializeField] float speedTimer;
    [Range(1, 10)][SerializeField] float jumpHeight;
    [Range(1, 10)][SerializeField] float gravityValue;
    [Range(1, 10)][SerializeField] int jumpMax;
    [Range(1, 10)][SerializeField] float sprintTimer;
    [SerializeField] GameObject flashlight;

    [Header("-----Weapon Stats-----")]
    public List<gunStats> gunList = new List<gunStats>();
    [Range(0, 300)][SerializeField] int shootDist;
    [Range(0, 3f)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(0, 10)][SerializeField] int magSize;
    [Range(0, 10)][SerializeField] float reloadTime;
    [Range(0, 10)][SerializeField] int shotsFired;
    [Range(0, 500)][SerializeField] int totalBulletCount;
    [Range(0,500)][SerializeField] int bulletsRemaining;
    [SerializeField] MeshFilter gunModel;
    [SerializeField] MeshRenderer gunMat;
    public GameObject primaryGunPOS;
    public GameObject secondaryGunPOS;
    public GameObject sniperFlashPos;
    public GameObject rifleFlashPos;
    public GameObject pistolFlashPos;
    public GameObject uziFlashPos;
    public GameObject shotgunFlashPos;
    public GameObject ADSsniperFlashPos;
    public bool sniper;
    public bool rifle;
    public bool pistol;
    public bool uzi;
    public bool shotgun;
    public bool isReloading;
    public float spread;
    public float Recoil;

    [Header("----- Gun Locker -----")]
    [SerializeField] GameObject sniperPre;
    [SerializeField] GameObject pistolPre;
    [SerializeField] GameObject RiflePre;
    [SerializeField] GameObject ShotGunPre;
    [SerializeField] GameObject UziPre;

    [Header("-----Audio-----")]
    [SerializeField] AudioClip[] audJump;
    [SerializeField] AudioClip[] audDamage;
    [SerializeField] AudioClip[] audSteps;
    [SerializeField][Range(0, 1)] float audJumpVol;
    [SerializeField][Range(0, 1)] float audDamageVol;
    [SerializeField][Range(0, 1)] float audStepsVol;

    private Vector3 move;
    private Vector3 playerVelocity;

    private bool groundedPlayer;
    public bool isSprinting;
    public bool isShooting;
    public bool isAiming;
    bool stepIsPlaying;
    bool Holdfire;

    private int jumpedTimes;
    public int HPOrig;
    private int bulletsShot;
    private int OrigBullet = 0;
    private float OrigSpeed;
    int selectedGun;

    ReticalSpread ret;
    //int bulletsRemaining;

    //public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;
        OrigSpeed = playerSpeed;
        spawnPlayer();
        cameracon = GetComponentInChildren<camerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //Please leave this here for testing purposes
        //Damages the player if you hit "/"
        if (Input.GetKeyDown(KeyCode.KeypadDivide)) takeDamage(1);

        ret = gameManager.instance.ret.GetComponent<ReticalSpread>();

        FlashLight();

        ADS();

        if (gameManager.instance.activeMenu == null)
        {
            movement();
            changeGun();
            if (gunList.Count > 0)
            {
                OverallFire();

                OverallReload();
            }
        }

        if (gunList.Count == 0)
        {
            gameManager.instance.ui.updateBulletCounter();
        }
        StartCoroutine(sprint());
    }

    void movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer )
        {
            if (!stepIsPlaying && move.normalized.magnitude > 0.5f)
            {
                StartCoroutine(playSteps());
            }
            if (playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                jumpedTimes = 0;
            }
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
            (transform.forward * Input.GetAxis("Vertical"));
        if (Input.GetButtonDown("Horizontal"))
        {
            playerSpeed = OrigSpeed;
            playerSpeed /= 2;
        }
        else if (Input.GetButtonUp("Horizontal"))
        {
            playerSpeed = OrigSpeed;
        }

        if (Input.GetButtonDown("BackStep"))
        {
            playerSpeed = OrigSpeed;
            playerSpeed /= 2;
        }
        else if (Input.GetButtonUp("BackStep"))
        {
            playerSpeed = OrigSpeed;
        }

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && jumpedTimes < jumpMax)
        {
            audioSource.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            jumpedTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //StartCoroutine(resetSpeed());
    }

    IEnumerator sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed *= sprintMod;
            yield return new WaitForSeconds(sprintTimer);
            playerSpeed = OrigSpeed;
            isSprinting = false;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed = OrigSpeed;
        }
    }

    IEnumerator playSteps()
    {
        stepIsPlaying = true;

        audioSource.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);

        if (!isSprinting)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }

        stepIsPlaying = false;
    }

    public void updateUI()
    {
        gameManager.instance.ui.HealthControl();

        gameManager.instance.ui.Level1Missions();
    }

    IEnumerator shoot()
    {
        isShooting = true;
        gunList[selectedGun].bulletsRemaining -= gunList[selectedGun].shotsFired;
        
        audioSource.PlayOneShot(gunList[selectedGun].gunShotAud, gunList[selectedGun].gunShotAudVol);

        for (int i = 0; i < gunList[selectedGun].shotsFired; i++)
            StartCoroutine(flashMuzzel());

        gameManager.instance.ui.updateBulletCounter();

        RaycastHit hit;

        Vector3 middle = new Vector3(0.5f,0.5f,0);

        Ray ray = Camera.main.ViewportPointToRay(middle);

        for (int i = 0; i < gunList[selectedGun].shotsFired; ++i)
        {
            float x;
            float y;
            if (!isAiming)
            {
                x = Random.Range((-ret.curSize / ret.maxSize) / 15, (ret.curSize / ret.maxSize) / 15);
                y = Random.Range((-ret.curSize / ret.maxSize) / 15, (ret.curSize / ret.maxSize) / 15);
            }
            else
            {
                x = Random.Range((-ret.curSize / ret.maxSize) / 15, (ret.curSize / ret.maxSize) / 15);
                y = Random.Range((-ret.curSize / ret.maxSize) / 15, (ret.curSize / ret.maxSize) / 15);
            }

            Vector3 spreaddirect = ray.direction + new Vector3(x, y, 0);
            if (Physics.Raycast(ray.origin, spreaddirect, out hit, shootDist))
            {
                IDamage damageable = hit.collider.GetComponent<IDamage>();

                if (damageable != null)
                {
                    //for (int i = 0; i < gunList[selectedGun].shotsFired; i++)
                    damageable.takeDamage(shootDamage);
                }
                //for (int i = 0; i < gunList[selectedGun].shotsFired; i++)
                Instantiate(gunList[selectedGun].hitEffect, hit.point, gunList[selectedGun].hitEffect.transform.rotation);
            }
        }
        cameracon.ApplyRecoil(gunList[selectedGun].Recoil);
        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    IEnumerator flashMuzzel()
    {
        if (sniper == true)
        {
            if (isAiming)
            {
                ADSsniperFlashPos.SetActive(true);
            }
            else
            {
                sniperFlashPos.SetActive(true);
            }
        }
        else if (rifle  == true)
        {
            rifleFlashPos.SetActive(true);
        }
        else if (pistol == true)
        {
            pistolFlashPos.SetActive(true);
        }
        else if (uzi == true)
        {
            uziFlashPos.SetActive(true);
        }
        else if (shotgun == true)
        {
            shotgunFlashPos.SetActive(true);
        }

        yield return new WaitForSeconds(0.2f);

        sniperFlashPos.SetActive(false);
        ADSsniperFlashPos.SetActive(false);
        rifleFlashPos.SetActive(false);
        pistolFlashPos.SetActive(false);
        uziFlashPos.SetActive(false);
        shotgunFlashPos.SetActive(false);
    }

    public void takeDamage(int amount)
    {
        HP -= amount;

        audioSource.PlayOneShot(audDamage[Random.Range(0, audDamage.Length)], audDamageVol);
        
        updateUI();
        if (HP <= 0)
        {
            gameManager.instance.youLose();
        }
        else StartCoroutine(DamageFlash());
    }

    IEnumerator DamageFlash()
    {
        gameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageFlash.SetActive(false);
    }

    IEnumerator reload()
    {
        isReloading = true;

        gameManager.instance.promptReloadOn();
        yield return new WaitForSeconds(reloadTime);
        gameManager.instance.promptReloadOff();

        audioSource.PlayOneShot(gunList[selectedGun].gunReloadAud, gunList[selectedGun].gunReloadAudVol);

        int bullestToReload = gunList[selectedGun].magSize - gunList[selectedGun].bulletsRemaining;

        if (gunList[selectedGun].totalBulletCount > 0 && gunList[selectedGun].bulletsRemaining < gunList[selectedGun].magSize)
        {
            //Debug.Log("re");
            int reservedAmmo = (int)Mathf.Min(gunList[selectedGun].totalBulletCount, bullestToReload);
            gunList[selectedGun].bulletsRemaining += reservedAmmo;
            gunList[selectedGun].totalBulletCount -= reservedAmmo;
        }

        //Debug.Log("reload");

        //bulletsShot = 0;
        gameManager.instance.ui.updateBulletCounter();

        isReloading = false;
    }

    public int getMagSize()
    {
        return gunList[selectedGun].totalBulletCount;
    }

    public int getBulletsRemaining()
    {
        return gunList[selectedGun].bulletsRemaining;
    }

    public void spawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
        HP = HPOrig;
        totalBulletCount = OrigBullet;
        bulletsRemaining = magSize;
        updateUI();
        gameManager.instance.ui.updateBulletCounter();
    }

    public void gunPickup(gunStats gunStat)
    {
        if (gunList.Count == 2)
        {
            if (sniper == true)
            {
                Instantiate(sniperPre, transform.position, Quaternion.identity);
            }
            else if (rifle == true)
            {
                Instantiate(RiflePre, transform.position, Quaternion.identity);
            }
            else if (pistol == true)
            {
                Instantiate(pistolPre, transform.position, Quaternion.identity);
            }
            else if (uzi == true)
            {
                Instantiate(UziPre, transform.position, Quaternion.identity);
            }
            else if (shotgun == true)
            {
                Instantiate(ShotGunPre, transform.position, Quaternion.identity);
            }
            gunList.RemoveAt(selectedGun);

            gunList.Add(gunStat);
        }
        else
        {
            gunList.Add(gunStat);
        }
        

        shootDamage = gunStat.shootDamage;
        shootDist = gunStat.shootDist;
        shootRate = gunStat.shootRate;
        magSize = gunStat.magSize;
        reloadTime = gunStat.reloadTime;
        shotsFired = gunStat.shotsFired;
        totalBulletCount = gunStat.totalBulletCount;
        sniper = gunStat.sniper;
        rifle = gunStat.rifle;
        pistol = gunStat.pistol;
        uzi = gunStat.uzi;
        shotgun = gunStat.shotgun;
        Holdfire = gunStat.HoldFire;
        spread = gunStat.Spread;
        Recoil = gunStat.Recoil;



        gunModel.mesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
        gunMat.material = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;

        selectedGun = gunList.Count - 1;

        audioSource.PlayOneShot(gunList[selectedGun].gunPickupAud, gunList[selectedGun].gunPickupAudVol);
        gameManager.instance.ui.updateBulletCounter();
    }

    void changeGun()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1 && !isReloading)
        {
            selectedGun++;
            changeGunStats();

            //gameManager.instance.updateBulletCounter();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0 && !isReloading)
        {
            selectedGun--;
            changeGunStats();
            //gameManager.instance.updateBulletCounter();
        }
    }

    void changeGunStats()
    {
        shootDamage = gunList[selectedGun].shootDamage;
        shootDist = gunList[selectedGun].shootDist;
        shootRate = gunList[selectedGun].shootRate;
        magSize = gunList[selectedGun].magSize;
        reloadTime = gunList[selectedGun].reloadTime;
        shotsFired = gunList[selectedGun].shotsFired;
        bulletsRemaining = gunList[selectedGun].bulletsRemaining;
        totalBulletCount = gunList[selectedGun].totalBulletCount;
        sniper = gunList[selectedGun].sniper;
        rifle = gunList[selectedGun].rifle;
        pistol = gunList[selectedGun].pistol;
        uzi = gunList[selectedGun].uzi;
        shotgun = gunList[selectedGun].shotgun;
        Holdfire = gunList[selectedGun].HoldFire;
        spread = gunList[selectedGun].Spread;
        Recoil = gunList[selectedGun].Recoil;

        gunModel.mesh = gunList[selectedGun].model.GetComponent<MeshFilter>().sharedMesh;
        gunMat.material = gunList[selectedGun].model.GetComponent<MeshRenderer>().sharedMaterial;
        
        gameManager.instance.ui.updateBulletCounter();
    }

    public int SetHP(int amount)
    {
        gameManager.instance.HPBar.fillAmount = (float)(HP + amount) / HPOrig;
        return HP += amount;
    }

    public void SetBullets(int amount)
    {
        if (gunList.Count > 0)
        {
            gunList[selectedGun].totalBulletCount += amount;
            gameManager.instance.ui.updateBulletCounter();
        }
    }

    public void SetAmmoCrate(int amount)
    {
        amount = gunList[selectedGun].magSize;
        if (gunList.Count > 0)
        {
            gunList[selectedGun].totalBulletCount += amount;
            gameManager.instance.ui.updateBulletCounter();
        }
    }

    public int GetHP(int health)
    {
        return health = HP;
    }

    public int GetMaxHP(int health)
    {
        return health = HPOrig;
    }

    public void FlashLight()
    {
        if (Input.GetButtonDown("FlashLight"))
        {
            if (flashlight.activeSelf)
            {
                flashlight.SetActive(false);
            }
            else
            {
                flashlight.SetActive(true);
            }
        }
    }

    public void ADS()
    {
        if (sniper)
        {
            if (Input.GetMouseButtonDown(1))
            {
                primaryGunPOS.SetActive(false);
                secondaryGunPOS.SetActive(true);
                isAiming = true;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                primaryGunPOS.SetActive(true);
                secondaryGunPOS.SetActive(false);
                isAiming = false;
            }
        }
    }

    public void OverallFire()
    {
        if (gunList[selectedGun].HoldFire == true && !isShooting && !isReloading)
        {
            if (Input.GetMouseButton(0))
            {
                if (gunList.Count > 0 && gunList[selectedGun].bulletsRemaining == 0 && !isReloading)
                {
                    audioSource.PlayOneShot(gunList[selectedGun].gunNoAmmoAud, gunList[selectedGun].gunNoAmmoAudVol);
                }

                if (gunList.Count > 0 && gunList[selectedGun].bulletsRemaining > 0 && !isReloading)
                {
                    Debug.Log("shooting");
                    StartCoroutine(shoot());
                }
            }
        }
        else if (Input.GetButtonDown("Shoot") && !isShooting && !isReloading)
        {
            //Debug.Log("pressed");

            if (gunList.Count > 0 && gunList[selectedGun].bulletsRemaining == 0 && !isReloading)
            {
                audioSource.PlayOneShot(gunList[selectedGun].gunNoAmmoAud, gunList[selectedGun].gunNoAmmoAudVol);
            }

            if (gunList.Count > 0 && gunList[selectedGun].bulletsRemaining > 0 && !isReloading)
            {
                Debug.Log("shooting");
                StartCoroutine(shoot());
            }
        }
    }

    public void OverallReload()
    {
        if (!isReloading)
        {
            if (gunList.Count > 0 && Input.GetButtonDown("Reload") || gunList.Count > 0 && Input.GetButtonDown("Reload") && gunList[selectedGun].bulletsRemaining != magSize || gunList.Count > 0 && Input.GetButtonDown("Reload") && gunList[selectedGun].totalBulletCount != 0)
            {
                //Debug.Log("re");
                //isReloading = true;
                if (gunList[selectedGun].totalBulletCount != 0)
                    StartCoroutine(reload());
            }
        }
    }

    //public IEnumerator fireDame(int damage)
    //{
    //    HP -= damage;

    //    audioSource.PlayOneShot(audDamage[Random.Range(0, audDamage.Length)], audDamageVol);

    //    updateUI();
    //    if (HP <= 0)
    //    {
    //        gameManager.instance.youLose();
    //    }
    //    else StartCoroutine(DamageFlash());

    //    yield return new WaitForSeconds(2);
    //}

    //public IEnumerator fireTimer(float timer)
    //{
    //    yield return new WaitForSeconds(timer);
    //}
}
