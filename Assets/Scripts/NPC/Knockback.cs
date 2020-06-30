using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Knockback : MonoBehaviour
{
    [SerializeField] private float thrust;
    [SerializeField] private float knockTime;
    [SerializeField] private string otherTag;
    //public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        //{
        //    other.GetComponent<Pot>().Smash();
        //}

        if (other.gameObject.CompareTag(otherTag) && other.isTrigger)
        {
            Rigidbody2D hit = other.GetComponentInParent<Rigidbody2D>();

            if (hit != null)
            {
                Vector3 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                //hit.AddForce(difference, ForceMode2D.Impulse);
                hit.DOMove(hit.transform.position + difference, knockTime);

                if (other.gameObject.CompareTag("enemy") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.STAGGER;
                    other.GetComponent<Enemy>().Knock(hit, knockTime);
                }

                if (other.GetComponentInParent<PlayerMovement>().currentState != PlayerState.STAGGER)
                {
                    hit.GetComponent<PlayerMovement>().currentState = PlayerState.STAGGER;
                    other.GetComponentInParent<PlayerMovement>().Knock(knockTime);
                }
            }
        }
    }
}
