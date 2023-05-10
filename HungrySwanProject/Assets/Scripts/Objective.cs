using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : MonoBehaviour, IInteractable
{
    void Start()
    {
        gameManager.instance.updateGameGoal(1);
    }

    void Update()
    {
        
    }

    public void  interact(bool canInteract) 
    {
        gameManager.instance.updateGameGoal(1);
    }
}
