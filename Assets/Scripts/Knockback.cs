using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                //StartCoroutine(KnockCo(hit));

                if (other.gameObject.CompareTag("enemy") && other.isTrigger) {
                    hit.GetComponent<Enemy>().currentState = EnemyState.STAGGER;
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                }

                if (other.gameObject.CompareTag("Player"))
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.STAGGER;
                    other.GetComponent<PlayerMovement>().Knock(knockTime);
                }
            }
        }
    }

    //private IEnumerator KnockCo(Rigidbody2D enemy)
    //{
    //    if (enemy != null)
    //    {
    //        yield return new WaitForSeconds(knockTime);
    //        enemy.velocity = Vector2.zero;
    //        enemy.GetComponent<Enemy>().currentState = EnemyState.IDLE;
    //    }
    //}
}
