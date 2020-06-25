using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicPowerup : Powerup
{
    [Header("Magic PowerUp")]
    public float amountOfMagic;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInventory.ReceiveMagic(amountOfMagic);
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
