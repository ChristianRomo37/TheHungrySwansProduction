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
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerControler>();
        timeScaleOrig = Time.timeScale;
        updateBulletCounter();
        playerSpawnPos = GameObject.FindGameObjectWithTag("Player Spawn Pos");
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
