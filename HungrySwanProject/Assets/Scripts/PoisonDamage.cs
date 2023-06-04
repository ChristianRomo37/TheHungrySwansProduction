using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoisonDamage : MonoBehaviour, IDamage
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
            poisoned();
            takeDamage(damage);
        }
    }

    public void takeDamage(int damage)
    {
        damagedHP -= damage;
    }

    IEnumerator poisoned()
    {
        yield return new WaitForSeconds(1);
    }

}
