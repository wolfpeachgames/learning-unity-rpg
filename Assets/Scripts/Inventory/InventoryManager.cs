using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Info")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryContentsPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;


    private void OnEnable()
    {
        ClearInventorySlots();
        MakeInventorySlots();
        SetTextAndButton("", false);
    }


    public void SetTextAndButton(string description, bool buttonActive)
    {
        descriptionText.text = description;
        useButton.SetActive(buttonActive);
    }


    private void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.items.Count; i++)
            {
                if (playerInventory.items[i].numberHeld > 0)
                {
                    GameObject blankItemSlot = Instantiate(blankInventorySlot, inventoryContentsPanel.transform.position, Quaternion.identity);
                    blankItemSlot.transform.SetParent(inventoryContentsPanel.transform);
                    InventorySlot newSlot = blankItemSlot.GetComponent<InventorySlot>();
                    if (newSlot)
                    {
                        newSlot.Setup(playerInventory.items[i], this);
                    }
                }

            }
        }
    }


    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false); // initialize description section as blank
    }


    public void SetupDescriptionAndButton(InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = newItem.itemDescription;
        useButton.SetActive(newItem.usable);
    }


    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
            // clear all of the inventory slots
            ClearInventorySlots();
            // refill all slots with new values
            MakeInventorySlots();
            if (currentItem.numberHeld == 0)
            {
                SetTextAndButton("", false);
            }
        }
    }


    private void ClearInventorySlots()
    {
        for (int i = 0; i < inventoryContentsPanel.transform.childCount; i++)
        {
            Destroy(inventoryContentsPanel.transform.GetChild(i).gameObject);
        }
    }
}
