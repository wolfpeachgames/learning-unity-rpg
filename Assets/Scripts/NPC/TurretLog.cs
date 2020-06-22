using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretLog : Log
{
    public GameObject projectile;
    public float fireDelay;
    private float fireDelayRemaining;
    public bool canFire = true;


    private void Update()
    {
        fireDelayRemaining -= Time.deltaTime;
        if (fireDelayRemaining <= 0)
        {
            canFire = true;
            fireDelayRemaining = fireDelay;
        }
    }


    public override void CheckDistance()
    {
        //Debug.Log("Check Distance");
        if (ShouldChase())
        {
            if (canFire)
            {
                Vector3 tempVector = target.transform.position - transform.position;
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(tempVector);
                canFire = false;
                ChangeState(EnemyState.WALK);
                anim.SetBool("wakeUp", true);
            }
        }
        else if (ShouldSleep())
        {
            anim.SetBool("wakeUp", false);
        }
    }
}
