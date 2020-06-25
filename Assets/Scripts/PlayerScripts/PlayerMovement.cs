using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    IDLE, WALK, ATTACK, INTERACT, STAGGER, DEAD
}

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator anim;
    public VectorValue startingPosition;

    [Header("State and Health")]
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    [Header("Inventory")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;

    [Header("Attacking")]
    public Signal playerHitSignal;
    public GameObject projectile;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.WALK;
        anim = GetComponent<Animator>();
        anim.SetFloat("moveX", 0);
        anim.SetFloat("moveY", -1f);
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }


    // Update is called once per frame
    void Update()
    {
        // is the player in an interaction?
        if (currentState == PlayerState.INTERACT)
        {
            return;
        }

        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("attack") && currentState != PlayerState.ATTACK && currentState != PlayerState.STAGGER)
        {
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Attack") && currentState != PlayerState.ATTACK && currentState != PlayerState.STAGGER)
        {
            StartCoroutine(SecondAttackCo());
        }
        else if (currentState == PlayerState.WALK || currentState == PlayerState.IDLE)
        {
            UpdateAnimationAndMove();
        }
    }


    private IEnumerator AttackCo()
    {
        anim.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        anim.SetBool("attacking", false);
        yield return new WaitForSeconds(.23f);
        if (currentState != PlayerState.INTERACT)
        {
            currentState = PlayerState.WALK;
        }
    }


    private IEnumerator SecondAttackCo()
    {
        //animator.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        MakeArrow();
        //animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.23f);
        if (currentState != PlayerState.INTERACT)
        {
            currentState = PlayerState.WALK;
        }
    }


    private void MakeArrow()
    {
        Vector2 temp = new Vector2(anim.GetFloat("moveX"), anim.GetFloat("moveY"));
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
    }


    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(anim.GetFloat("moveY"), anim.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void ReceiveItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.INTERACT)
            {
                currentState = PlayerState.INTERACT;
                anim.SetBool("receiveItem", true);
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                anim.SetBool("receiveItem", false);
                currentState = PlayerState.IDLE;
                receivedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
        }
    }


    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            anim.SetFloat("moveX", change.x);
            anim.SetFloat("moveY", change.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }


    void MoveCharacter()
    {
        change.Normalize();
        // multiplying by Time.deltaTime makes this a very small amount each frame
        Vector2 distanceToMove = transform.position + change * speed * Time.deltaTime;
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
        playerHitSignal.Raise();
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.IDLE;
        }
    }
}
