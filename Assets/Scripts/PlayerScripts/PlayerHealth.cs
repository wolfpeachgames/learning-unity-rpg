﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : GenericHealth
{
    [SerializeField] private Signal healthSignal;


    private void OnEnable()
    {
        healthSignal.Raise();
    }


    public override void DecreaseHealth(float amount)
    {
        base.DecreaseHealth(amount);
        maxHealth.RuntimeValue = currentHealth;
        healthSignal.Raise();
    }
}
