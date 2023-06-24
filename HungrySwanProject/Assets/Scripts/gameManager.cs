using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- Scene -----")]
    public Scene context;

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
    public UIElements ui;
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject playerDamageFlash;
    public TextMeshProUGUI carPartsRemainingLabel;
    public TextMeshProUGUI carPartsRemainingText;
    public TextMeshProUGUI helicopterPartsRemainingLabel;
    public TextMeshProUGUI helicopterPartsRemainingText;
    public GameObject ret;
    public Button respawn;

    [Header("----- HUD Stuff-----")]
    public TextMeshProUGUI ePrompt;
    public TextMeshProUGUI aPrompt;
    public TextMeshProUGUI totalMagSize;
    public TextMeshProUGUI bulletsLeft;
    public Image HPBar;
    public TextMeshProUGUI reloadPrompt;
    public TextMeshProUGUI objectivePrompt;
    public GameObject Grenade;
    public TextMeshProUGUI grenadePrompt;

    [Header("-----Objective-----")]
    public GameObject holdingBattery;
    public GameObject holdingEngine;
    public GameObject holdingGas;
    public GameObject holdingKey;
    public GameObject holdingTire;
    public GameObject holdingBlade;
    public GameObject holdingRotor;
    public GameObject holdingSkid;

    [Header("-----Drop-----")]
    public List<GameObject> drops;

    [Header("----- Main Menu -----")]
    public GameObject levelSelect;
    public GameObject settings;
    public GameObject confirmManager;


    public bool isPaused;
    float timeScaleOrig;
    public int carPartsRemaining;
    public int carPartsPlaced;
    public bool hasPart;
    public bool battery;
    public bool engine;
    public bool gas;
    public bool key;
    public bool tire;
    public bool left;
    public int helicopterPartsRemaining;
    public bool blade;
    public bool hgas;
    public bool hkey;
    public bool rotor;
    public bool skid;
    public bool left2;
    public bool restart;
    public int respawnCount;


    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;
       
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player)
        {
            playerScript = player.GetComponent<playerControler>();
        }
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
        //currentWeapon = GameObject.FindGameObjectWithTag("Player Weapon");
        ui = GetComponent<UIElements>();
        Nenemy = GameObject.FindGameObjectWithTag("Normal Zombie");
        Senemy = GameObject.FindGameObjectWithTag("Spitter Zombie");
        Fenemy = GameObject.FindGameObjectWithTag("Fast Zombie");
        Tenemy = GameObject.FindGameObjectWithTag("Tank Zombie");
        //enemyAIscript = Nenemy.GetComponent<enemyAI>();
        NEnemySpawnPos = GameObject.FindGameObjectWithTag("NEnemy Spawn Pos");
        SEnemySpawnPos = GameObject.FindGameObjectWithTag("SEnemy Spawn Pos");
        FEnemySpawnPos = GameObject.FindGameObjectWithTag("FEnemy Spawn Pos");
        TEnemySpawnPos = GameObject.FindGameObjectWithTag("TEnemy Spawn Pos");

        holdingBattery = playerScript.holdingBattery;
        holdingEngine = playerScript.holdingEngine;
        holdingGas = playerScript.holdingGas;
        holdingKey = playerScript.holdingKey;
        holdingTire = playerScript.holdingTire;
        holdingBlade = playerScript.holdingBlade;
        holdingRotor = playerScript.holdingRotor;
        holdingSkid = playerScript.holdingSkid;
        respawnCount = 0;

        instance.ui.updateBulletCounter();

        context = SceneManager.GetActiveScene();
        updateGameGoalText();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug Command
        //Automatically Triggers Win State on pressing * button
        //if (Input.GetKeyDown(KeyCode.KeypadMultiply)) StartCoroutine(youWin());

        string stringname = context.name;
        if (Input.GetButtonDown("Cancel") && activeMenu == null && stringname != ("Main Menu") || Input.GetKeyDown(KeyCode.Tab) && activeMenu == null && stringname != ("Main Menu"))
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            activeMenu.SetActive(isPaused);
            pauseState();
        }

        if (respawnCount == 3)
        {
            respawn.interactable = false;
        }

        //if (HPBar.fillAmount <= 0)
        //{
        //    youLose();
        //}
        //if (playerScript.getMagSize() < 0)
        //{
        //    totalMagSize.text = "0";
        //}
    }

    public void pauseState()
    {
        playerScript.primaryGunPOS.SetActive(true);
        playerScript.secondaryGunPOS.SetActive(false);
        playerScript.isAiming = false;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        ui.hUD.SetActive(false);
        //promptEOff();
    }

    public void unPauseState()
    {
        Time.timeScale = timeScaleOrig;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        ui.hUD.SetActive(true);
        isPaused = false;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void youLose()
    {
        pauseState();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
        carPartsRemainingLabel.SetText("");
        carPartsRemainingText.SetText("");
        helicopterPartsRemainingLabel.SetText("");
        helicopterPartsRemainingText.SetText("");
        
        //promptEOff();
    }

    public void updateGameGoal(int amount)
    {
        carPartsRemaining += amount;
        ui.Level1Missions();
        if (left == true)
        {
            StartCoroutine(youWin());
        }
    }

    public void update2ndGameGoal(int amount)
    {
        helicopterPartsRemaining += amount;
        ui.Level2Missions();
        if (left2 == true)
        {
            StartCoroutine(youWin());
        }
    }

    public IEnumerator youWin()
    {
        yield return new WaitForSeconds(3);
        activeMenu = winMenu;
        activeMenu.SetActive(true);
        //promptEOff();
        pauseState();
    }

    public void updateGameGoalText()
    {
        if (context.name == "Full look Scene")
        {
            carPartsRemainingLabel.gameObject.SetActive(true);
            carPartsRemainingText.gameObject.SetActive(true);
        }
        else if (context.name == "Artic Scene")
        {
            helicopterPartsRemainingLabel.gameObject.SetActive(true);
            helicopterPartsRemainingText.gameObject.SetActive(true);
        }
    }

    public void promptEOn()
    {
        ePrompt.enabled = true;
        ePrompt.text = "Press E To Pick Up Gun";
    }

    public void promptEOnBox()
    {
        aPrompt.enabled = true;
        aPrompt.text = "Press E To Pick Up Ammo";
    }

    public void promptEOff()
    {
        ePrompt.enabled = false;
    }

    public void promptEOffBox()
    {
        aPrompt.enabled = false;
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

    public void promptCarOn()
    {
        objectivePrompt.text = "Press E To Leave";
        objectivePrompt.enabled = true;
    }

    public void promptCarOff()
    {
        objectivePrompt.enabled= false;
    }

    public void promptSkidOn()
    {
        objectivePrompt.text = "Press E To Place Landing Skid";
        objectivePrompt.enabled = true;
    }

    public void promptSkidOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptBladeOn()
    {
        objectivePrompt.text = "Press E To Install Blade";
        objectivePrompt.enabled = true;
    }

    public void promptBladeOff()
    {
        objectivePrompt.enabled = false;
    }

    public void promptRotorOn()
    {
        objectivePrompt.text = "Press E To Install Rotor";
        objectivePrompt.enabled = true;
    }

    public void promptRotorOff()
    {
        objectivePrompt.enabled = false;
    }
}
