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
    private Animator myAnimator;
    public VectorValue startingPosition;

    [Header("State and Health")]
    public PlayerState currentState;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public Signal updatePlayerMagicSignal;

    [Header("Inventory")]
    public Inventory playerInventory;
    public SpriteRenderer receivedItemSprite;
    public Item bow;

    [Header("Attacking")]
    public Signal playerHitSignal;
    public GameObject projectile;

    [Header("Invulnerability Frames")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberOfFlashes;
    public Collider2D myTriggerCollider;
    public SpriteRenderer mySprite;


    void Start()
    {
        currentState = PlayerState.WALK;
        myAnimator = GetComponent<Animator>();
        myAnimator.SetFloat("moveX", 0);
        myAnimator.SetFloat("moveY", -1f);
        myRigidbody = GetComponent<Rigidbody2D>();
        transform.position = startingPosition.initialValue;
    }


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
            if (playerInventory.CheckForItem(bow)) // only allow firing arrows if player has the bow item
            {
                StartCoroutine(SecondAttackCo());
            }
        }
        else if (currentState == PlayerState.WALK || currentState == PlayerState.IDLE)
        {
            UpdateAnimationAndMove();
        }
    }


    private IEnumerator AttackCo()
    {
        myAnimator.SetBool("attacking", true);
        currentState = PlayerState.ATTACK;
        yield return null;
        myAnimator.SetBool("attacking", false);
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
        if (playerInventory.currentMagic > 0)
        {
            // create new arrow
            Vector2 temp = new Vector2(myAnimator.GetFloat("moveX"), myAnimator.GetFloat("moveY"));
            Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();

            if (playerInventory.currentMagic >= arrow.magicCost)
            {
                // consume magic cost of arrow
                playerInventory.ConsumeMagic(arrow.magicCost);
                updatePlayerMagicSignal.Raise();
                // send arrow on its way
                arrow.Setup(temp, ChooseArrowDirection());
            }
            else
            {
                // not enough magic for this arrow, discard
                Destroy(arrow.gameObject);
            }
            
        }
        
    }


    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(myAnimator.GetFloat("moveY"), myAnimator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }


    public void ReceiveItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (currentState != PlayerState.INTERACT)
            {
                currentState = PlayerState.INTERACT;
                myAnimator.SetBool("receiveItem", true);
                receivedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                myAnimator.SetBool("receiveItem", false);
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
            change.x = Mathf.Round(change.x);
            change.y = Mathf.Round(change.y);
            myAnimator.SetFloat("moveX", change.x);
            myAnimator.SetFloat("moveY", change.y);
            myAnimator.SetBool("moving", true);
        }
        else
        {
            myAnimator.SetBool("moving", false);
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
            StartCoroutine(FlashCo());
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.IDLE;
        }
    }


    private IEnumerator FlashCo()
    {
        int processedFlashes = 0;
        myTriggerCollider.enabled = false;
        while (processedFlashes < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            processedFlashes++;
        }
        myTriggerCollider.enabled = true;
    }
}
