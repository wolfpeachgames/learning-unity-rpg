using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaLog : Log
{
    [Header("Area")]
    public Collider2D boundary;


    public override void CheckDistance()
    {
        if (ShouldChaseInBoundary())
        {
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            ChangeAnim(temp - transform.position);
            myRigidBody.MovePosition(temp);
            ChangeState(EnemyState.WALK);
            anim.SetBool("wakeUp", true);
        }
        else if (ShouldSleepInBoundary())
        {
            anim.SetBool("wakeUp", false);
        }
    }


    private bool ShouldChaseInBoundary()
    {
        return (ShouldChase() && boundary.bounds.Contains(target.transform.position));
    }

    private bool ShouldSleepInBoundary()
    {
        return (ShouldSleep() || !boundary.bounds.Contains(target.transform.position));
    }

}
