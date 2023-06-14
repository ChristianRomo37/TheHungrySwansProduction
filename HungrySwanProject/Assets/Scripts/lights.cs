using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lights : MonoBehaviour
{

    public Light _light;

    public float minTime;
    public float maxTime;
    public float timer;

    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer <= 0)
        {
            GetComponent<Light>().enabled = !GetComponent<Light>().enabled;
            timer = Random.Range(minTime, maxTime);
        }

    }
}
