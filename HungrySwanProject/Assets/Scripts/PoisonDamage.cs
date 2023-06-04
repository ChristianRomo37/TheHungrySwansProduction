using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PiosonDamage : MonoBehaviour, IDamage
{
    [SerializeField] int damage;
    [SerializeField] int duration;
    [SerializeField] int damagedHP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < duration; i++)
        {
            fireOn();
            fire.Play();
            takeDamage(damage);
        }
        fire.Stop();
    }

    public void takeDamage(int damage)
    {
        damagedHP -= damage;
    }

    IEnumerator fireOn()
    {
        yield return new WaitForSeconds(1);
    }

}
