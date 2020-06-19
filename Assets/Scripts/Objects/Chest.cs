using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    public Item contents;
    public Inventory playerInventory;
    public bool isOpen;
    public Signal receiveItemSignal;
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpen)
            {
                OpenChest();
            }
            else
            {
                ChestAlreadyOpen();
            }
        }
    }


    public void OpenChest()
    {
        // dialog window on
        dialogBox.SetActive(true);
        // dialog text = contents text
        dialogText.text = contents.itemDescription;
        // add contents to inventory
        playerInventory.AddItem(contents);
        playerInventory.currentItem = contents;
        // raise signal to player to animate
        receiveItemSignal.Raise();
        //// raise the context clue
        //contextClueSignal.Raise();
        // set chest to open
        isOpen = true;
        anim.SetBool("opened", true);
    }


    public void ChestAlreadyOpen()
    {
        // dialog off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        receiveItemSignal.Raise();
    }
}
