using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void resume()
    {
        gameManager.instance.unPauseState();
        //gameManager.instance.isPaused = !gameManager.instance.isPaused;
    }

    public void restart()
    {
        gameManager.instance.unPauseState();
        gameManager.instance.ui.updateBulletCounter();
        // not good code if you decide to make a bigger game since it will take time to reload back to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void EndGame()
    {
        gameManager.instance.unPauseState();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
    public void respawnPlayer()
    {
        gameManager.instance.unPauseState();
        gameManager.instance.playerScript.HP = gameManager.instance.playerScript.HPOrig;
        gameManager.instance.playerScript.updateUI();
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.playerScript.spawnPlayer();
        //gameManager.instance.enemyAIscript.spawnEnemys();

    }

    public void promptE()
    {
        
    }

    //Main Menu Buttons
    public void NewGame()
    {
        SceneManager.LoadScene("Full look Scene");
    }

    //Level Select
    public void LSelect()
    {
        if (gameManager.instance.levelSelect.activeSelf) gameManager.instance.levelSelect.SetActive(false);
        else if (gameManager.instance.settings.activeSelf)
        {
            gameManager.instance.settings.SetActive(false);
            gameManager.instance.levelSelect.SetActive(true);
        }
        else gameManager.instance.levelSelect.SetActive(true);
    }

    public void L1Mob()
    {
        SceneManager.LoadScene("Full look Scene", LoadSceneMode.Single);
    }

    public void L1Boss()
    {
        SceneManager.LoadScene("Boss Scene", LoadSceneMode.Single);
    }

    //Settings
    public void SSelect()
    {
        if (gameManager.instance.settings.activeSelf) gameManager.instance.settings.SetActive(false);
        else if (gameManager.instance.levelSelect && gameManager.instance.levelSelect.activeSelf)
        {
            gameManager.instance.levelSelect.SetActive(false);
            gameManager.instance.settings.SetActive(true);
        } 
        else gameManager.instance.settings.SetActive(true);
    }
    //Credits
    public void Credits()
    {
        SceneManager.LoadScene("Credit Scene", LoadSceneMode.Single);
    }
}
