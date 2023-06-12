using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElements : MonoBehaviour
{
    [SerializeField] Image bossHPBar;

    Boss bossScript;
    public void Level1Missions()
    {
        if (gameManager.instance.key && gameManager.instance.hasPart)
        {
            gameManager.instance.carPartsRemainingLabel.SetText("Put the Key in the car");
            gameManager.instance.carPartsRemainingText.SetText("");
        }
        else if (gameManager.instance.battery && gameManager.instance.hasPart)
        {
            gameManager.instance.carPartsRemainingLabel.SetText("Put the Battery in the car");
            gameManager.instance.carPartsRemainingText.SetText("");
        }
        else if (gameManager.instance.engine && gameManager.instance.hasPart)
        {
            gameManager.instance.carPartsRemainingLabel.SetText("Put the Engine in the car");
            gameManager.instance.carPartsRemainingText.SetText("");
        }
        else if (gameManager.instance.tire && gameManager.instance.hasPart)
        {
            gameManager.instance.carPartsRemainingLabel.SetText("Put the Tire on the car");
            gameManager.instance.carPartsRemainingText.SetText("");
        }
        else if (gameManager.instance.gas && gameManager.instance.hasPart)
        {
            gameManager.instance.carPartsRemainingLabel.SetText("Put Gas in the car");
            gameManager.instance.carPartsRemainingText.SetText("");
        }
        else
        {
            if (gameManager.instance.carPartsRemaining > 0)
            {
                gameManager.instance.carPartsRemainingLabel.SetText("Fix the Car");
                gameManager.instance.carPartsRemainingText.SetText(gameManager.instance.carPartsRemaining.ToString());
            }
            else if (gameManager.instance.carPartsRemaining < 1)
            {
                gameManager.instance.carPartsRemainingLabel.SetText("Fix the Car");

            }
        }

    }

    public void BossHealth()
    {
        bossHPBar.fillAmount = (float)bossScript.HP / bossScript.HPOrig;
    }
    
    public void HealthControl()
    {
        gameManager.instance.HPBar.fillAmount = (float)gameManager.instance.playerScript.HP / gameManager.instance.playerScript.HPOrig;
    }

    public void updateBulletCounter()
    {

        if (gameManager.instance.playerScript && gameManager.instance.playerScript.gunList.Count > 0)
        {
            gameManager.instance.totalMagSize.text = gameManager.instance.playerScript.getMagSize().ToString();
            gameManager.instance.bulletsLeft.text = gameManager.instance.playerScript.getBulletsRemaining().ToString();
        }
    }
}
