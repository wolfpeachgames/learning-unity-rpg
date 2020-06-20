using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public FloatValue playerHealth;
    public float amountToIncrease;
    public FloatValue heartContainers;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerHealth.RuntimeValue += amountToIncrease;
            if (playerHealth.RuntimeValue > heartContainers.RuntimeValue * 2f)
            {
                playerHealth.RuntimeValue = heartContainers.RuntimeValue * 2f;
            }
            powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
