using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoilAni : MonoBehaviour
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
        if (Input.GetButtonDown("Shoot") && gameManager.instance.playerScript.bulletsRemaining > 0)
        {
            //anie.SetBool("Shooting", true);
            if (gameManager.instance.playerScript.sniper)
            {
                anie.SetBool("Sniper", true);
                anie.SetBool("Shooting", true);
                StartCoroutine(wait());
            }
            else if (gameManager.instance.playerScript.pistol)
            {
                anie.SetBool("Pistol", true);
                anie.SetBool("Shooting", true);
            }
            else if (gameManager.instance.playerScript.uzi)
            {
                anie.SetBool("Uzi", true);
                anie.SetBool("Shooting", true);
            }
            else if (gameManager.instance.playerScript.rifle)
            {
                anie.SetBool("Rifle", true);
                anie.SetBool("Shooting", true);
            }
            else if (gameManager.instance.playerScript.shotgun)
            {
                anie.SetBool("Shotgun", true);
                anie.SetBool("Shooting", true);
                StartCoroutine (wait());
            }
        }
        else if (Input.GetButtonUp("Shoot"))
        {
            anie.SetBool("Shooting", false);
            anie.SetBool("Rifle", false);
            anie.SetBool("Pistol", false);
            anie.SetBool("Shotgun", false);
            anie.SetBool("Uzi", false);
            anie.SetBool("Sniper", false);
        }
    }

    IEnumerator wait()
    {
        anie.SetBool("Sniper", false);
        anie.SetBool("Shooting", false);
        yield return new WaitForSeconds(3);
    }
}
