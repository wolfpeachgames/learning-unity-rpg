using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    [Header("Construction")]
    public Rigidbody2D myRigidBody;
    public Animator anim;
    [Header("Target")]
    public Transform target;
    public Transform homePosition;
    [Header("Stats")]
    public float chaseRadius;
    public float attackRadius;


    // Start is called before the first frame update
    void Start()
    {
        currentState = EnemyState.IDLE;
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        anim.SetBool("wakeUp", true);
    }


    // Update is called once per 2 frames
    void FixedUpdate()
    {
        CheckDistance();
    }


    public virtual void CheckDistance()
    {
        if (ShouldChase())
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            myRigidBody.MovePosition(temp);
            ChangeState(EnemyState.WALK);
            anim.SetBool("wakeUp", true);
        }
        else if (ShouldSleep())
        {
            anim.SetBool("wakeUp", false);
        }
    }


    public bool ShouldChase()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool notBusy = (currentState == EnemyState.IDLE || currentState == EnemyState.WALK) && (currentState != EnemyState.STAGGER);
        return (notBusy && distance <= chaseRadius && distance > attackRadius);
    }

    public bool ShouldSleep()
    {
        return (Vector3.Distance(target.position, transform.position) > chaseRadius);
    }


    public void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                SetAnimFloat(Vector2.right);
            }
            else if (direction.x < 0)
            {
                SetAnimFloat(Vector2.left);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(Vector2.up);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(Vector2.down);
            }
        }
    }


    private void SetAnimFloat(Vector2 setVector)
    {
        anim.SetFloat("moveX", setVector.x);
        anim.SetFloat("moveY", setVector.y);
    }


    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
        }
    }
}
