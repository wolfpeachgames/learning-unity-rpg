using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GenericDamage : MonoBehaviour
{
    [SerializeField] private float damageAmount;
    [SerializeField] private string otherTag;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            GenericHealth health = other.GetComponent<GenericHealth>();
            if (health)
            {
                health.DecreaseHealth(damageAmount);
            }
        }
    }
}
