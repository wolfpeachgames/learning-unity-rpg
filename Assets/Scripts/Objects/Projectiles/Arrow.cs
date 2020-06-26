using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Magic Cost")]
    public float magicCost;
    [Header("Physics")]
    public float speed;
    public Rigidbody2D myRigidbody;
    [Header("Lifecycle")]
    public float lifetime;
    private float lifetimeRemaining;


    void Start()
    {
        lifetimeRemaining = lifetime;
    }


    private void Update()
    {
        lifetimeRemaining -= Time.deltaTime;
        if (lifetimeRemaining <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void Setup(Vector2 velocity, Vector3 direction)
    {
        myRigidbody.velocity = velocity.normalized * speed;
        transform.rotation = Quaternion.Euler(direction);
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            Destroy(this.gameObject);
        }
    }
}
