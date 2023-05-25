using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("-----Player Stuff-----")]
    public GameObject player;
    public playerControler playerScript;
    public GameObject playerSpawnPos;
    //public GameObject currentWeapon;

    [Header("-----Enemy Stuff-----")]
    //public enemyAI enemyAIscript;
    public GameObject Nenemy;
    public GameObject Senemy;
    public GameObject Fenemy;
    public GameObject Tenemy;
    public GameObject NEnemySpawnPos;
    public GameObject SEnemySpawnPos;
    public GameObject FEnemySpawnPos;
    public GameObject TEnemySpawnPos;
    public GameObject NEnemySpawnPosGrave;
    public GameObject SEnemySpawnPosGrave;
    public GameObject FEnemySpawnPosGrave;
    public GameObject TEnemySpawnPosGrave;

    [Header("-----UI Stuff-----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject playerDamageFlash;
    public TextMeshProUGUI carPartsRemainingLabel;
    public TextMeshProUGUI carPartsRemainingText;

    [Header("----- HUD Stuff-----")]
    public TextMeshProUGUI ePrompt;
    public TextMeshProUGUI totalMagSize;
    public TextMeshProUGUI bulletsLeft;
    public Image HPBar;
    public TextMeshProUGUI reloadPrompt;
    public TextMeshProUGUI objectivePrompt;


    public bool isPaused;
    float timeScaleOrig;
    public int carPartsRemaining;
    public int carPartsPlaced;
    public bool battery;
    public bool engine;
    public bool gas;
    public bool key;
    public bool tire;
    public bool left;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
       
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerControler>();
        
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        //currentWeapon = GameObject.FindGameObjectWithTag("Player Weapon");
        
        Nenemy = GameObject.FindGameObjectWithTag("Normal Zombie");
        Senemy = GameObject.FindGameObjectWithTag("Spitter Zombie");
        Fenemy = GameObject.FindGameObjectWithTag("Fast Zombie");
        Tenemy = GameObject.FindGameObjectWithTag("Tank Zombie");
        //enemyAIscript = Nenemy.GetComponent<enemyAI>();
        NEnemySpawnPos = GameObject.FindGameObjectWithTag("NEnemy Spawn Pos");
        SEnemySpawnPos = GameObject.FindGameObjectWithTag("SEnemy Spawn Pos");
        FEnemySpawnPos = GameObject.FindGameObjectWithTag("FEnemy Spawn Pos");
        TEnemySpawnPos = GameObject.FindGameObjectWithTag("TEnemy Spawn Pos");
        NEnemySpawnPosGrave = GameObject.FindGameObjectWithTag("NEnemy Spawn Pos GraveYard");
        SEnemySpawnPosGrave = GameObject.FindGameObjectWithTag("SEnemy Spawn Pos GraveYard");
        FEnemySpawnPosGrave = GameObject.FindGameObjectWithTag("FEnemy Spawn Pos GraveYard");
        TEnemySpawnPosGrave = GameObject.FindGameObjectWithTag("TEnemy Spawn Pos GraveYard");
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
        //promptEOff();
    }

    public void unPauseState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isPaused = false;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void youLose()
    {
        pauseState();
        carPartsRemainingLabel.SetText("");
        carPartsRemainingText.SetText("");
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        //promptEOff();
    }

    public void updateGameGoal(int amount)
    {
        carPartsRemaining += amount;
        carPartsRemainingText.text = carPartsRemaining.ToString();
        if (carPartsRemaining < 1)
        {
            carPartsRemainingLabel.SetText("Fix the Car");
            carPartsRemainingText.SetText("");
        }
        if (left == true)
        {
            StartCoroutine(youWin());
        }
    }

    IEnumerator youWin()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        //promptEOff();
        pauseState();
    }

    public void updateBulletCounter()
    {
        if (instance.playerScript.gunList.Count > 0)
        {
           totalMagSize.text = playerScript.getMagSize().ToString();
           bulletsLeft.text = playerScript.getBulletsRemaining().ToString();
        }
    }

    public void promptEOn()
    {
        ePrompt.enabled = true;
        ePrompt.text = "Press E To Pick Up Gun";
    }

    public void promptEOff()
    {
        ePrompt.enabled = false;
    }

    public void promptReloadOn()
    {
        reloadPrompt.enabled = true;
        reloadPrompt.text = "Reloading!!!";
    }

    public void promptReloadOff()
    {
        reloadPrompt.enabled = false;
    }

    public void promptBatteryOn()
    {
        objectivePrompt.text = "Press E To Install Battery";
        objectivePrompt.enabled = true;
    }

    public void promptBatteryOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptEngineOn()
    {
        objectivePrompt.text = "Press E To Install Engine";
        objectivePrompt.enabled = true;
    }

    public void promptEngineOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptGasOn()
    {
        objectivePrompt.text = "Press E To Insert Gas";
        objectivePrompt.enabled = true;
    }

    public void promptGasOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptKeyOn()
    {
        objectivePrompt.text = "Press E To Insert Key";
        objectivePrompt.enabled = true;
    }

    public void promptKeyOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptTireOn()
    {
        objectivePrompt.text = "Press E To Install Tire";
        objectivePrompt.enabled = true;
    }

    public void promptTireOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptLeaveOn()
    {
        objectivePrompt.text = "Press E To Leave";
        objectivePrompt.enabled = true;
    }

    public void promptLeaveOff()
    {
        objectivePrompt.enabled = false;
    }
}
