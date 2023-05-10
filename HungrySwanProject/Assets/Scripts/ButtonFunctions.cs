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
        // not good code if you decide to make a bigger game since it will take time to reload back to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    //public void respawnPlayer()
    //{
    //    gameManager.instance.unPauseState();
    //    gameManager.instance.playerScript.spawnPlayer();
    //}
}
