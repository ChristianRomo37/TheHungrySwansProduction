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
        if (Input.GetButtonDown("Reload"))
        {
            anie.SetBool("Reloading", true);
        }
        else if (Input.GetButtonUp("Reload"))
        {
            anie.SetBool("Reloading", false);
        }
    }
}
