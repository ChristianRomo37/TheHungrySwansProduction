using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

public class playerControler : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource audioSource;

    [Header("-----Player Stats-----")]
    [Range(1, 20)][SerializeField] int HP;
    [Range(1, 10)][SerializeField] float playerSpeed;
    [Range(1, 10)][SerializeField] float sprintMod;
    [Range(1, 10)][SerializeField] float jumpHeight;
    [Range(1, 10)][SerializeField] float gravityValue;
    [Range(1, 10)][SerializeField] int jumpMax;

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

    [Header("-----Audio-----")]
    [SerializeField] AudioClip[] audJump;
    [SerializeField] AudioClip[] audDamage;
    [SerializeField] AudioClip[] audSteps;
    [SerializeField][Range(0, 1)] float audJumpVol;
    [SerializeField][Range(0, 1)] float audDamageVol;
    [SerializeField][Range(0, 1)] float audStepsVol;

    private int jumpedTimes;
    private Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isSprinting;
    private bool isShooting;
    private int HPOrig;
    private int bulletsShot;
    private bool isReloading;
    private int OrigBullet;
    int selectedGun;
    //int bulletsRemaining;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;

        //OrigBullet = totalBulletCount;
        healthBar.SetMaxHealth(HPOrig);
        //bulletsRemaining = magSize;
        //write an if statement for if you hae a gun

        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Please leave this here for testing purposes
        //Damages the player if you hit "/"
        if (Input.GetKeyDown(KeyCode.KeypadDivide)) takeDamage(1);


        if (gameManager.instance.activeMenu == null)
        {
            movement();
            changeGun();

            if (Input.GetButtonDown("Shoot") && !isShooting)
            {
                Debug.Log("pressed");
                if (gunList.Count > 0 && gunList[selectedGun].bulletsRemaining > 0 && !isReloading)
                {
                    Debug.Log("shooing");
                    StartCoroutine(shoot());
                }
            }

            if (gunList.Count > 0 && Input.GetButtonDown("Reload") && !isReloading || gunList.Count > 0 && Input.GetButtonDown("Reload") && gunList[selectedGun].bulletsRemaining != magSize || gunList.Count > 0 && Input.GetButtonDown("Reload") && gunList[selectedGun].totalBulletCount != 0)
            {
                //Debug.Log("re");
                StartCoroutine(reload());
            }
        }

        if (gunList.Count == 0)
        {
            gameManager.instance.updateBulletCounter();
        }
        sprint();
    }

    void movement()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpedTimes = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
            (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (Input.GetButtonDown("Jump") && jumpedTimes < jumpMax)
        {
            jumpedTimes++;
            playerVelocity.y = jumpHeight;
        }

        playerVelocity.y -= gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed /= sprintMod;
        }
    }

    void updateUI()
    {
        gameManager.instance.HPBar.fillAmount = (float)HP / HPOrig;
       

    }
    IEnumerator shoot()
    {
        isShooting = true;
        gunList[selectedGun].bulletsRemaining -= gunList[selectedGun].shotsFired;
        //bulletsShot++;
        gameManager.instance.updateBulletCounter();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f,0.5f)), out hit, shootDist))
        {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                for (int i = 0; i < shotsFired; i++)
                {
                    damageable.takeDamage(shootDamage);
                }
            }
        }
        //bulletsShot++;
        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    public void takeDamage(int amount)
    {
        HP -= amount;
        updateUI();
        if (HP <= 0)
        {
            healthBar.SetHealth(0);
            gameManager.instance.youLose();
        }
    }

    IEnumerator reload()
    {
        isReloading = true;

        int bullestToReload = gunList[selectedGun].magSize - gunList[selectedGun].bulletsRemaining;

        if (gunList[selectedGun].totalBulletCount > 0 && gunList[selectedGun].bulletsRemaining < gunList[selectedGun].magSize)
        {
            //Debug.Log("re");
            int reservedAmmo = (int)Mathf.Min(gunList[selectedGun].totalBulletCount, bullestToReload);
            gunList[selectedGun].bulletsRemaining += reservedAmmo;
            gunList[selectedGun].totalBulletCount -= reservedAmmo;
        }

        //Debug.Log("reload");
        gameManager.instance.updateBulletCounter();

        //bulletsShot = 0;
        yield return new WaitForSeconds(reloadTime);

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
        gameManager.instance.updateBulletCounter();
    }

    public void gunPickup(gunStats gunStat)
    {
        if (gunList.Count == 2)
        {
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


        gunModel.mesh = gunStat.model.GetComponent<MeshFilter>().sharedMesh;
        gunMat.material = gunStat.model.GetComponent<MeshRenderer>().sharedMaterial;

        selectedGun = gunList.Count - 1;
        
        gameManager.instance.updateBulletCounter();
    }

    void changeGun()
    {
        isReloading = false;
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && selectedGun < gunList.Count - 1)
        {
            selectedGun++;
            changeGunStats();
            //gameManager.instance.updateBulletCounter();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && selectedGun > 0)
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

        gunModel.mesh = gunList[selectedGun].model.GetComponent<MeshFilter>().sharedMesh;
        gunMat.material = gunList[selectedGun].model.GetComponent<MeshRenderer>().sharedMaterial;
        
        gameManager.instance.updateBulletCounter();
    }
}
