using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    private Rigidbody2D myRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.IDLE;
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per 2 frames
    void FixedUpdate()
    {
        CheckDistance();
    }


    void CheckDistance()
    {
        if (ShouldChase())
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRigidBody.MovePosition(temp);
            ChangeState(EnemyState.WALK);
        }
    }

    private bool ShouldChase()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool notBusy = (currentState == EnemyState.IDLE || currentState == EnemyState.WALK) && currentState != EnemyState.STAGGER;
        return (notBusy && distance <= chaseRadius && distance > attackRadius);
    }

    private void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
