using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    public GameObject enemy;
    public playerControler playerScript;
    public enemyAI enemyAIscript;
    public GameObject playerSpawnPos;
    public GameObject enemySpawnPos;

    [Header("-----UI Stuff-----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;

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
        
        enemy = GameObject.FindGameObjectWithTag("Normal Zombie");
        enemy = GameObject.FindGameObjectWithTag("Spitter Zombie");
        enemy = GameObject.FindGameObjectWithTag("Fast Zombie");
        enemy = GameObject.FindGameObjectWithTag("Tank Zombie");
        enemyAIscript = enemy.GetComponent<enemyAI>();
        enemySpawnPos = GameObject.FindGameObjectWithTag("NEnemy Spawn Pos");
        enemySpawnPos = GameObject.FindGameObjectWithTag("SEnemy Spawn Pos");
        enemySpawnPos = GameObject.FindGameObjectWithTag("FEnemy Spawn Pos");
        enemySpawnPos = GameObject.FindGameObjectWithTag("TEnemy Spawn Pos");
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
}
