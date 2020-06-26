using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : Powerup
{
    public FloatValue heartContainers;
    public FloatValue playerHealth;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // increase number of heart containers
            heartContainers.RuntimeValue++;
            // fill player health
            playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2;
            // raise health signal
            powerupSignal.Raise();
            // destroy
            Destroy(this.gameObject);
        }
    }
}
