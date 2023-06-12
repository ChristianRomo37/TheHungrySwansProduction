using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingAni : MonoBehaviour
{
    Animator anie;

    // Start is called before the first frame update
    void Start()
    {
        anie = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.instance.playerScript.magFull)
        {
            if (Input.GetButtonDown("Reload"))
            {
                //anie.SetBool("Reloading", true);
                if (gameManager.instance.playerScript.sniper)
                {
                    anie.SetBool("Sniper", true);
                    anie.SetBool("Reloading", true);
                }
                else if (gameManager.instance.playerScript.pistol)
                {
                    anie.SetBool("Pistol", true);
                    anie.SetBool("Reloading", true);
                }
                else if (gameManager.instance.playerScript.uzi)
                {
                    anie.SetBool("Uzi", true);
                    anie.SetBool("Reloading", true);
                }
                else if (gameManager.instance.playerScript.rifle)
                {
                    anie.SetBool("Rifle", true);
                    anie.SetBool("Reloading", true);
                }
                else if (gameManager.instance.playerScript.shotgun)
                {
                    anie.SetBool("Shotgun", true);
                    anie.SetBool("Reloading", true);
                }
            }
            else if (Input.GetButtonUp("Reload"))
            {
                anie.SetBool("Reloading", false);
                anie.SetBool("Rifle", false);
                anie.SetBool("Pistol", false);
                anie.SetBool("Shotgun", false);
                anie.SetBool("Uzi", false);
                anie.SetBool("Sniper", false);
            }
        }
    }
}
