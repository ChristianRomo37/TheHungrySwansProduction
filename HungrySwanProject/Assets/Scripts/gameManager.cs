using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    public playerControler playerScript;
    public GameObject playerSpawnPos;
    //public GameObject currentWeapon;

    [Header("-----Enemy Stuff-----")]
    public enemyAI enemyAIscript;
    public GameObject Nenemy;
    public GameObject Senemy;
    public GameObject Fenemy;
    public GameObject Tenemy;
    public GameObject NEnemySpawnPos;
    public GameObject SEnemySpawnPos;
    public GameObject FEnemySpawnPos;
    public GameObject TEnemySpawnPos;

    [Header("-----UI Stuff-----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public TextMeshProUGUI totalMagSize;
    public TextMeshProUGUI bulletsLeft;
    

    public bool isPaused;
    float timeScaleOrig;
    int carPartsRemaining;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
       
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerControler>();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        //currentWeapon = GameObject.FindGameObjectWithTag("Player Weapon");
        
        //Nenemy = GameObject.FindGameObjectWithTag("Normal Zombie");
        //Senemy = GameObject.FindGameObjectWithTag("Spitter Zombie");
        //Fenemy = GameObject.FindGameObjectWithTag("Fast Zombie");
        //Tenemy = GameObject.FindGameObjectWithTag("Tank Zombie");
        //enemyAIscript = Nenemy.GetComponent<enemyAI>();
        //NEnemySpawnPos = GameObject.FindGameObjectWithTag("NEnemy Spawn Pos");
        //SEnemySpawnPos = GameObject.FindGameObjectWithTag("SEnemy Spawn Pos");
        //FEnemySpawnPos = GameObject.FindGameObjectWithTag("FEnemy Spawn Pos");
        //TEnemySpawnPos = GameObject.FindGameObjectWithTag("TEnemy Spawn Pos");
        updateBulletCounter();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel") && activeMenu == null)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
            pauseState();
        }
        //if (playerScript.getMagSize() < 0)
        //{
        //    totalMagSize.text = "0";
        //}
    }

    public void pauseState()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void unPauseState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = !isPaused;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void youLose()
    {
        pauseState();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }

    public void updateGameGoal(int amount)
    {
        carPartsRemaining += amount;

        if (carPartsRemaining <= 0)
        {
            StartCoroutine(youWin());
        }
    }

    IEnumerator youWin()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        pauseState();
    }

    public void updateBulletCounter()
    {
        totalMagSize.text = playerScript.getMagSize().ToString();
        bulletsLeft.text = playerScript.getBulletsRemaining().ToString();
    }
}
