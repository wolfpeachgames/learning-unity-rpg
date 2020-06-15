using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;


    // Start is called before the first frame update
    void Start()
    {
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
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    private bool ShouldChase()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return (distance <= chaseRadius && distance > attackRadius);
    }
}
