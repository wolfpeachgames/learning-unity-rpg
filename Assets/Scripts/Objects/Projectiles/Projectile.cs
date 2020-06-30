using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Geometry")]
    public float speed;
    public Vector2 directionToMove;

    [Header("Lifecycle")]
    public float lifetime;
    private float lifetimeRemaining;

    [Header("Self")]
    public Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        lifetimeRemaining = lifetime;
    }


    void Update()
    {
        lifetimeRemaining -= Time.deltaTime;
        if (lifetimeRemaining <= 0)
        {
            Destroy(this.gameObject);
        }
    }


    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody.velocity = initialVelocity * speed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
