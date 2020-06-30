using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Geometry")]
    public float speed;
    public Vector2 directionToMove;

    [Header("Self")]
    public Rigidbody2D myRigidbody;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    public void Launch(Vector2 initialVelocity)
    {
        myRigidbody.velocity = initialVelocity * speed;
    }
}
