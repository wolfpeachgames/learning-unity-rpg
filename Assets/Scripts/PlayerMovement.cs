using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE, WALK, ATTACK, INTERACT, STAGGER, DEAD
}

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.WALK;
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1f);
        myRigidbody = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.ATTACK && currentState != PlayerState.STAGGER)
        {
            StartCoroutine(AttackCo());
        }
        else if (currentState == PlayerState.WALK || currentState == PlayerState.IDLE)
        {
            UpdateAnimationAndMove();
        }
    }


    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.23f);
        currentState = PlayerState.WALK;
    }


    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }


    void MoveCharacter()
    {
        change.Normalize();
        Debug.Log(change);
        // multiplying by Time.deltaTime makes this a very small amount each frame
        Vector2 distanceToMove = transform.position + change * speed * Time.deltaTime;
        Debug.Log(distanceToMove);
        myRigidbody.MovePosition(distanceToMove);
    }


    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
            currentState = PlayerState.DEAD;
        }
    }


    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.IDLE;
        }
    }
}
