using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour, IDamage
{
    [Header("-----Components-----")]
    [SerializeField] CharacterController controller;

    [Header("-----Player Stats-----")]
    [Range(1, 20)][SerializeField] int HP;
    [Range(1, 10)][SerializeField] float playerSpeed;
    [Range(1, 10)][SerializeField] float sprintMod;
    [Range(1, 10)][SerializeField] float jumpHeight;
    [Range(1, 10)][SerializeField] float gravityValue;
    [Range(1, 10)][SerializeField] int jumpMax;

    [Header("-----Weapon Stats-----")]
    [Range(2, 300)][SerializeField] int shootDist;
    [Range(0.1f, 3f)][SerializeField] float shootRate;
    [Range(1, 10)][SerializeField] int shootDamage;
    [Range(1, 10)][SerializeField] int magSize;
    [Range(1, 10)][SerializeField] float reloadTime;
    [Range(1, 10)][SerializeField] int shotsFired;
    [Range(1, 500)][SerializeField] int totalBulletCount;
    //[SerializeField] bool holdFire;

    private int jumpedTimes;
    private Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private bool isSprinting;
    private bool isShooting;
    private int bulletsShot;
    private int bulletsRemaining;
    private bool isReloading;
    private int HPOrig;
    private int OrigBullet;

    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        HPOrig = HP;

        OrigBullet = totalBulletCount;
        healthBar.SetMaxHealth(HPOrig);
        bulletsRemaining = magSize;

        gameManager.instance.updateBulletCounter();

        spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        //Please leave this here for testing purposes
        //Damages the player if you hit "/"
        if (Input.GetKeyDown(KeyCode.KeypadDivide)) takeDamage(5);


        if (gameManager.instance.activeMenu == null)
        {
            movement();
            if (Input.GetButtonDown("Shoot") && !isShooting)
            {
                //Debug.Log("shoot");
                if (bulletsRemaining > 0 && !isReloading)
                {
                    StartCoroutine(shoot());
                }
            }

            if (Input.GetButtonDown("Reload") && !isReloading || Input.GetButtonDown("Reload") && bulletsRemaining != magSize || Input.GetButtonDown("Reload") && totalBulletCount != 0)
            {
                //Debug.Log("re");
                StartCoroutine(reload());
            }
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

    IEnumerator shoot()
    {
        isShooting = true;
        bulletsRemaining--;
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
        
        if (HP <= 0)
        {
            healthBar.SetHealth(0);
            gameManager.instance.youLose();
        }
        else healthBar.SetHealth(HP);
    }

    IEnumerator reload()
    {
        isReloading = true;

        int bullestToReload = magSize - bulletsRemaining;

        if (totalBulletCount > 0 && bulletsRemaining < magSize)
        {
            int reservedAmmo = (int)Mathf.Min(totalBulletCount, bullestToReload);
            bulletsRemaining += reservedAmmo;
            totalBulletCount -= reservedAmmo;
        }
        //Debug.Log("reload");
        gameManager.instance.updateBulletCounter();

        //bulletsShot = 0;
        yield return new WaitForSeconds(reloadTime);

        isReloading = false;
    }


    public int getMagSize()
    {
        return totalBulletCount;
    }

    public int getBulletsRemaining()
    {
        return bulletsRemaining;
    }

    public void spawnPlayer()
    {
        controller.enabled = false;
        transform.position = gameManager.instance.playerSpawnPos.transform.position;
        controller.enabled = true;
        HP = HPOrig;
        totalBulletCount = OrigBullet;
        bulletsRemaining = magSize;
        healthBar.SetHealth(HP);
        gameManager.instance.updateBulletCounter();
    }
}
