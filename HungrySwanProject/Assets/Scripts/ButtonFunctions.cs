using System.Collections;
using System.Collections.Generic;
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
        SceneManager.LoadScene("Main Menu");
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
        else gameManager.instance.levelSelect.SetActive(true);
    }

    public void L1Mob()
    {
        SceneManager.LoadScene("Full look Scene");
    }

    public void L1Boss()
    {
        SceneManager.LoadScene("Boss Scene");
    }

    //Settings
    //Credits
    public void Credits()
    {
        SceneManager.LoadScene("Credit Scene");
    }
}
