using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;

    public void SetMaxHealth(int amount)
    {
        healthBar.maxValue = amount;
        healthBar.value = amount;
    }

    public void SetHealth(int amount)
    {
        healthBar.value = amount;
    }
}
