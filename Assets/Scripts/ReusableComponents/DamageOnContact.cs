using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnContact : MonoBehaviour
{
    [SerializeField] private string otherTag;
    [SerializeField] private float damageAmount;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag))
        {
            // first check to see if there is health
            GenericHealth tempHealth = other.gameObject.GetComponent<GenericHealth>();
            if (tempHealth)
            {
                tempHealth.DecreaseHealth(damageAmount);
            }
            Destroy(this.gameObject);
        }
    }
}
