using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    IDLE, WALK, ATTACK, STAGGER
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState currentState;
    public bool hasHome;
    public Vector2 homePosition;

    [Header("Enemy Stats")]
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;

    [Header("Death Effects")]
    public GameObject deathEffect;
    private float DEATH_EFFECT_DELAY = 1f;
    public Signal deathSignal;


    private void Awake()
    {
        health = maxHealth.initialValue;
        homePosition = transform.position;
    }


    private void OnEnable()
    {
        if (hasHome)
        {
            transform.position = homePosition;
        }
    }


    private void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            DeathEffect();
            deathSignal.Raise();
            this.gameObject.SetActive(false);
        }
    }


    private void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, DEATH_EFFECT_DELAY);
        }
    }


    public void Knock(Rigidbody2D myRigidBody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidBody, knockTime));
        TakeDamage(damage);
    }


    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = EnemyState.IDLE;
        }
    }
}
