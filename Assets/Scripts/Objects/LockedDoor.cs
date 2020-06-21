using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    KEY, ENEMY, BUTTON
}


public class LockedDoor : Interactable
{
    [Header("Door variables")]
    public DoorType thisDoorType;
    public bool open = false;
    public Inventory playerInventory;
    public SpriteRenderer doorSprite;
    public BoxCollider2D physicsCollider;


    private void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (playerInRange && thisDoorType == DoorType.KEY)
            {
                // does the player have a key?
                if (playerInventory.numberOfKeys > 0)
                {
                    // remove a player key
                    playerInventory.numberOfKeys--;
                    // if so, call open
                    Open();
                }
            }
        }
    }


    public void Open()
    {
        // turn off the door's sprite renderer
        doorSprite.enabled = false;
        // set open to true
        open = true;
        // turn off the door's box collider
        physicsCollider.enabled = false;
    }


    public void Close()
    {

    }

    public override void Activate()
    {
        Open();
    }
}
