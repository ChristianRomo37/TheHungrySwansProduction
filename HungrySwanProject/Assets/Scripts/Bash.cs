using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bash : MonoBehaviour
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
        if (Input.GetButtonDown("Melee"))
        {
            anie.SetBool("Bashing", true);
        }
        else if (Input.GetButtonUp("Melee"))
        {
            anie.SetBool("Bashing", false);
        }
    }
}
