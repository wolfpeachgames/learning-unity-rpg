using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    void Start()
    {
        powerupSignal.Raise();
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerInventory.coins += 1;
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
