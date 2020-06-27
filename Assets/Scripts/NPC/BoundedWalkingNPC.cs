using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedWalkingNPC : Interactable
{
    [Header("Movement")]
    public float speed;
    public float moveTime;
    private float moveTimeRemaining;
    public float waitTime;
    private float waitTimeRemaining;
    private bool isMoving;
    private Vector3 directionVector;
    
    [Header("Patrol Area")]
    public Collider2D bounds;

    private Rigidbody2D myRigidbody;
    private Transform myTransform;
    private Animator anim;


    void Start()
    {
        myTransform = GetComponent<Transform>();
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        moveTimeRemaining = moveTime;
        waitTimeRemaining = waitTime;
        ChangeDirection();
    }


    void Update()
    {
        if (isMoving)
        {
            moveTimeRemaining -= Time.deltaTime;
            if (moveTimeRemaining <= 0)
            {
                moveTimeRemaining = moveTime;
                isMoving = false;
                anim.SetBool("moving", false);
            }
            if (!playerInRange)
            {
                Move();
            }
        }
        else
        {
            waitTimeRemaining -= Time.deltaTime;
            if (waitTimeRemaining <= 0)
            {
                waitTimeRemaining = waitTime;
                isMoving = true;
                anim.SetBool("moving", true);
                ChooseDifferentDirection();
            }
        }
    }


    private void ChooseDifferentDirection()
    {
        Vector3 previousDirection = directionVector;
        ChangeDirection();
        int attempts = 0;
        while (previousDirection == directionVector && attempts < 100)
        {
            attempts++;
            ChangeDirection();
        }
    }


    void Move()
    {
        Vector3 targetPosition = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(targetPosition))
        {
            myRigidbody.MovePosition(targetPosition);
        }
        else
        {
            ChangeDirection();
        }
    }


    void ChangeDirection()
    {
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                // walking right
                directionVector = Vector3.right;
                break;
            case 1:
                // walking up
                directionVector = Vector3.up;
                break;
            case 2:
                // walking left
                directionVector = Vector3.left;
                break;
            case 4:
                // walking down
                directionVector = Vector3.down;
                break;
            default:
                break;
        }
        UpdateAnimation();
    }


    void UpdateAnimation()
    {
        anim.SetFloat("moveX", directionVector.x);
        anim.SetFloat("moveY", directionVector.y);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        ChooseDifferentDirection();
    }

}
