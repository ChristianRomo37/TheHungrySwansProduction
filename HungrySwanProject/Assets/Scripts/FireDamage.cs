using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    int hp;
    [SerializeField] float timer;
    bool onFire = false;
    int ticks;
    public int dmg;
    public int interval;
    int fireDMG = 1;

    enemyAI enemy;

    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //if (ticks == 0)
        //{
        //    onFire = false;
        //}
        timer += Time.deltaTime;


        if (onFire)
        {
            StartCoroutine(TakeFireDMG());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        onFire = true;
        if (onFire)
        {
            StartCoroutine(TakeFireDMG());
        }

        //if (other.gameObject.CompareTag("Normal Zombie"))
        //{
        //    StartCoroutine(TakeFireDMGEn());
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        onFire = false;
        //enemy.onfire = false;
    }

    IEnumerator TakeFireDMG()
    {
        for (int i = 0; i < timer; i++)
        {
            if (timer >= 1)
            {
                timer = 0f;
                gameManager.instance.playerScript.HP -= fireDMG;
                gameManager.instance.playerScript.updateUI();

                if (gameManager.instance.playerScript.HP == 0)
                {
                    gameManager.instance.pauseState();
                    gameManager.instance.activeMenu = gameManager.instance.loseMenu;
                    gameManager.instance.activeMenu.SetActive(true);
                }
            }
        }
        yield return new WaitForSeconds(3);
    }

    //IEnumerator TakeFireDMGEn()
    //{
    //    enemy = GetComponent<enemyAI>();
    //    enemy.onfire = true;
    //    for (int i = 0;i < timer;i++)
    //    {
    //        if(timer >= 1)
    //        {
    //            timer = 0f;
    //            enemy.HP -= fireDMG;
    //            if (enemy.HP == 0)
    //            {
    //                StartCoroutine(enemy.deadAI());
    //            }
    //        }
    //    }
    //    yield return new WaitForSeconds (3);
    //}
}
