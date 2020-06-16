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
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        CheckDistance();
    }


    void CheckDistance()
    {
        if (ShouldChase())
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            myRigidBody.MovePosition(temp);
        }
    }

    private bool ShouldChase()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return (distance <= chaseRadius && distance > attackRadius);
    }
}
