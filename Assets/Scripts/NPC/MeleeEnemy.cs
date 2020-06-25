using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Log
{
    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}


    //public override void CheckDistance()
    //{
    //    if (Vector3.Distance(target.position,
    //                        transform.position) <= chaseRadius
    //         && Vector3.Distance(target.position,
    //                           transform.position) > attackRadius)
    //    {
    //        if (currentState == EnemyState.IDLE || currentState == EnemyState.WALK
    //            && currentState != EnemyState.STAGGER)
    //        {
    //            Vector3 temp = Vector3.MoveTowards(transform.position,
    //                                                     target.position,
    //                                                     moveSpeed * Time.deltaTime);
    //            ChangeAnim(temp - transform.position);
    //            myRigidBody.MovePosition(temp);
    //            ChangeState(EnemyState.WALK);
    //        }
    //    }
    //    else if (Vector3.Distance(target.position,
    //                transform.position) <= chaseRadius
    //                && Vector3.Distance(target.position,
    //                transform.position) <= attackRadius)
    //    {
    //        if (currentState == EnemyState.WALK
    //            && currentState != EnemyState.STAGGER)
    //        {
    //            StartCoroutine(AttackCo());
    //        }
    //    }
    //}


    public override void CheckDistance()
    {
        if (NotBusy())
        {
            if (ShouldChase())
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
                ChangeAnim(temp - transform.position);
                myRigidBody.MovePosition(temp);
                ChangeState(EnemyState.WALK);
                anim.SetBool("moving", true);
            }
            else if (ShouldAttack())
            {
                anim.SetBool("moving", false);
                StartCoroutine(AttackCo());
            }
            else
            {
                ChangeState(EnemyState.IDLE);
                anim.SetBool("moving", false);
            }
        }
        
    }


    private bool ShouldAttack()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        bool notBusy = NotBusy();
        return (notBusy && distance <= chaseRadius && distance <= attackRadius);
    }


    private bool NotBusy()
    {
        return currentState != EnemyState.STAGGER && currentState != EnemyState.ATTACK;
    }


    public IEnumerator AttackCo()
    {
        Debug.Log("ATTACk");
        currentState = EnemyState.ATTACK;
        anim.SetBool("attacking", true);
        yield return new WaitForSeconds(1f);
        currentState = EnemyState.IDLE;
        anim.SetBool("attacking", false);
        Debug.Log("not attack");
    }
}
