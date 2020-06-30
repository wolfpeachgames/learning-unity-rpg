using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;
    public float currentHealth;


    void Start() {
        currentHealth = maxHealth.RuntimeValue;
    }


    void Update() { }


    public virtual void IncreaseHealth(float amount) {
        currentHealth += amount;
        if (currentHealth > maxHealth.RuntimeValue)
        {
            currentHealth = maxHealth.RuntimeValue;
        }
    }


    public virtual void IncreaseHealthToMax() {
        IncreaseHealth(maxHealth.RuntimeValue);
    }


    public virtual void DecreaseHealth(float amount) {
        currentHealth -= amount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
    }


    public virtual void DecreaseHealthToZero() {
        DecreaseHealth(maxHealth.RuntimeValue);
    }
}
