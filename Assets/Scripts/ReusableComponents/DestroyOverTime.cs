using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [Header("Lifecycle")]
    [SerializeField] private float lifetime;


    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
