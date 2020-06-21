using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : Interactable
{
    [Header("Chest Contents")]
    public Item contents;
    public bool isOpen;
    public Signal receiveItemSignal;
    public BoolValue storedOpenState;

    [Header("Player")]
    public Inventory playerInventory;

    [Header("Feedback")]
    public GameObject dialogBox;
    public Text dialogText;
    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(storedOpenState.RuntimeValue);
        isOpen = storedOpenState.RuntimeValue;
        if (isOpen)
        {
            anim.SetBool("opened", true);
        }
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
        storedOpenState.RuntimeValue = true;
        Debug.Log("opened chest");
        Debug.Log(storedOpenState.RuntimeValue);
    }


    public void ChestAlreadyOpen()
    {
        // dialog off
        dialogBox.SetActive(false);
        // raise the signal to the player to stop animating
        receiveItemSignal.Raise();
    }
}
